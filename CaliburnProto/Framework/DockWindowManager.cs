// Credit: BladeWise
// http://caliburnmicro.codeplex.com/discussions/231809
// Adjusted to Avalondock: lukebuehler

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaliburnProto.Infrastructure;
using Caliburn.Micro;
using System.Windows;
using System.ComponentModel;
using AvalonDock;
using System.Windows.Data;
using System.Windows.Media;
using System.ComponentModel.Composition;

namespace CaliburnProto
{
    [Export(typeof(IWindowManager))]
    [Export(typeof(IDockWindowManager))]
    public class DockAwareWindowManager : WindowManager, IDockWindowManager
    {
        Window m_MainWindow;
        DockingManager m_DockingManager;

        //MEF constructor
        protected DockAwareWindowManager()
        { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DockAwareWindowManager"/> class.
        /// </summary>
        /// <param name="mainWindow">The main window or <see langword="null"/> to use the application main window.</param>
        /// <param name="dockingManager">The dock manager or <see langword="null"/> to use the main window dock manager.</param>
        public DockAwareWindowManager(Window mainWindow = null, DockingManager dockingManager = null)
        {
            m_MainWindow = mainWindow;
            m_DockingManager = dockingManager;
        }


        /// <summary>
        /// Shows a docked window.
        /// </summary>
        /// <param name = "viewModel">The view model.</param>
        /// <param name = "context">The context.</param>
        /// <param name = "selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
        /// <param name = "dockSide">The dock side.</param>
        public void ShowDockedWindow(object viewModel,
                                     object context = null,
                                     bool selectWhenShown = true,
                                     DockSide dockSide = DockSide.Left)
        {
            var dockableContent = CreateDockable(viewModel, context);
            dockableContent.Show(GetDockingManager(), GetAnchorStyle(dockSide));
            if (selectWhenShown)
                dockableContent.Activate();
            else
                dockableContent.ToggleAutoHide();
        }


        public void ShowFloatingWindow(object viewModel, object context = null, bool selectWhenShown = true)
        {
            var dockableContent = CreateDockable(viewModel, context);
            dockableContent.ShowAsFloatingWindow(GetDockingManager(), true);
            if (selectWhenShown)
                dockableContent.Activate();
        }

        public void ShowDocumentWindow(object viewModel, object context = null, bool selectWhenShown = true)
        {
            var dockableContent = CreateDockable(viewModel, context);
            dockableContent.ShowAsDocument(GetDockingManager());
            if (selectWhenShown)
                dockableContent.Activate();
        }

        /// <summary>
        ///   Creates the dockable window.
        /// </summary>
        /// <param name = "rootModel">The root model.</param>
        /// <param name = "context">The context.</param>
        /// <param name = "isDocument">If set to <c>true</c>, the created window will be a document window.</param>
        /// <returns>The dockable window.</returns>
        private DockableContent CreateDockable(object rootModel,
                                              object context,
                                              bool isDocument = false)
        {
            var view = EnsureDockableContent(rootModel, ViewLocator.LocateForModel(rootModel, null, context), isDocument);
            ViewModelBinder.Bind(rootModel, view, context);

            var haveDisplayName = rootModel as IHaveDisplayName;
            if (haveDisplayName != null && !ConventionManager.HasBinding(view, DockableContent.TitleProperty))
                view.SetBinding(DockableContent.TitleProperty, "DisplayName");

            new DockableContentConductor(rootModel, view);
            return view;
        }
        /// <summary>
        /// Ensures that a dockable window is created and properly set-up.
        /// </summary>
        /// <param name = "viewModel">The view model.</param>
        /// <param name = "view">The view.</param>
        /// <param name = "isDocument">If set to <c>true</c>, the dockable window is a document window.</param>
        /// <returns>The dockable window.</returns>
        private DockableContent EnsureDockableContent(object viewModel,
                                                    object view,
                                                    bool isDocument = false)
        {
            //const WindowCloseMethod closeMethod = WindowCloseMethod.Detach; //The window is destroyed once closed...
            var dockableContent = view as DockableContent;

            var d = new DocumentContent();
            if (dockableContent == null)
            {
                dockableContent = new DockableContent();
                dockableContent.Content = view;
                dockableContent.IsCloseable = true;
                {
                    
                    //DockingRules = new DockingRules(true, true, true),

                    //dockableContent.IsCloseable = viewModel.CanClose;
                    //Image img = new Image();
                    //img.Source = new BitmapImage(new Uri(@"Resources/Data.png", UriKind.Relative));
                    //dockableContent.Icon = img.Source;
                };
            }
            return dockableContent;
        }

        /// <summary>
        ///   Gets the dock site associated to the window.
        /// </summary>
        /// <param name = "window">The window.</param>
        /// <returns>The retrieved dock site.</returns>
        /// <exception cref = "InvalidOperationException">No dock site could be retrieved.</exception>
        private DockingManager GetDockingManager(Window window = null)
        {
            var dockingManager = m_DockingManager;

            if (dockingManager == null)
            {
                Window parentWindow = GetParentWindow(window);
                if (parentWindow != null)
                    dockingManager = parentWindow.FindChild<DockingManager>();
                //cache the result
                m_DockingManager = dockingManager;
            }

            if (dockingManager == null)
                throw new InvalidOperationException("Unable to retrieve a proper dock site ");

            return dockingManager;
        }

        /// <summary>
        /// Gets the parent window.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>The parent window, or <see langword="null"/> if no parent window was found.</returns>
        private Window GetParentWindow(Window window)
        {
            Window parentWindow = m_MainWindow ?? (Application.Current != null ? Application.Current.MainWindow : null);
            return parentWindow != window ? parentWindow : null;
        }

        private static AnchorStyle GetAnchorStyle(DockSide side)
        {
            switch (side)
            {
                case DockSide.None: return AnchorStyle.None;
                case DockSide.Left: return AnchorStyle.Left;
                case DockSide.Top: return AnchorStyle.Top;
                case DockSide.Right: return AnchorStyle.Right;
                case DockSide.Bottom: return AnchorStyle.Bottom;
                default: return AnchorStyle.Left;
            }
        }

        /// <summary>
        ///   The dockable window conductor, used to allow for interaction between view and view model.
        /// </summary>
        private class DockableContentConductor
        {
            #region Fields
            /// <summary>
            ///   The view.
            /// </summary>
            private readonly ManagedContent m_View;

            /// <summary>
            ///   The view model.
            /// </summary>
            private readonly object m_ViewModel;

            /// <summary>
            ///   The flag used to identify the view as closing.
            /// </summary>
            private bool m_IsClosing;

            /// <summary>
            ///   The flag used to determine if the view requested deactivation.
            /// </summary>
            private bool m_IsDeactivatingFromView;

            /// <summary>
            ///   The flag used to determine if the view model requested deactivation.
            /// </summary>
            private bool m_IsDeactivatingFromViewModel;
            #endregion

            /// <summary>
            ///   Initializes a new instance of the <see cref = "DockableContentConductor" /> class.
            /// </summary>
            /// <param name = "viewModel">The view model.</param>
            /// <param name = "view">The view.</param>
            public DockableContentConductor(object viewModel, ManagedContent view)
            {
                m_ViewModel = viewModel;
                m_View = view;

                var activatable = viewModel as IActivate;
                if (activatable != null)
                    activatable.Activate();

                var deactivatable = viewModel as IDeactivate;
                if (deactivatable != null)
                {
                    view.Closed += OnClosed;
                    deactivatable.Deactivated += OnDeactivated;
                }

                var guard = viewModel as IGuardClose;
                if (guard != null)
                    view.Closing += OnClosing;
            }

            /// <summary>
            ///   Called when the view has been closed.
            /// </summary>
            /// <param name = "sender">The sender.</param>
            /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
            private void OnClosed(object sender, EventArgs e)
            {
                m_View.Closed -= OnClosed;
                m_View.Closing -= OnClosing;

                if (m_IsDeactivatingFromViewModel)
                    return;

                var deactivatable = (IDeactivate)m_ViewModel;

                m_IsDeactivatingFromView = true;
                deactivatable.Deactivate(true);
                m_IsDeactivatingFromView = false;
            }

            /// <summary>
            ///   Called when the view has been deactivated.
            /// </summary>
            /// <param name = "sender">The sender.</param>
            /// <param name = "e">The <see cref = "Caliburn.Micro.DeactivationEventArgs" /> instance containing the event data.</param>
            private void OnDeactivated(object sender, DeactivationEventArgs e)
            {
                ((IDeactivate)m_ViewModel).Deactivated -= OnDeactivated;

                if (!e.WasClosed || m_IsDeactivatingFromView)
                    return;

                m_IsDeactivatingFromViewModel = true;
                m_IsClosing = true;
                m_View.Close();
                m_IsClosing = false;
                m_IsDeactivatingFromViewModel = false;
            }

            /// <summary>
            ///   Called when the view is about to be closed.
            /// </summary>
            /// <param name = "sender">The sender.</param>
            /// <param name = "e">The <see cref = "System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
            private void OnClosing(object sender, CancelEventArgs e)
            {
                var guard = (IGuardClose)m_ViewModel;

                if (m_IsClosing)
                {
                    m_IsClosing = false;
                    return;
                }

                bool runningAsync = false, shouldEnd = false;

                bool async = runningAsync;
                guard.CanClose(canClose =>
                {
                    if (async && canClose)
                    {
                        m_IsClosing = true;
                        m_View.Close();
                    }
                    else
                        e.Cancel = !canClose;

                    shouldEnd = true;
                });

                if (shouldEnd)
                    return;

                runningAsync = e.Cancel = true;
            }
        }
    }
}
