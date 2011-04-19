using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using CaliburnProto.Infrastructure;

namespace CaliburnProto
{
    /// <summary>
    /// Manages action items.
    /// </summary>
    public class ActionItemManager : IActionItemManager
    {
        public ActionItemManager()
        {
            Items = new BindableCollection<IActionItem>();
        }

        public BindableCollection<IActionItem> Items { get; private set; }

        public IActionItemManager WithScopeOf(object model)
        {
            return new ActionItemManagerWithScope(this, Items, model);
        }

        public IActionItemManager WithParent(string parentName)
        {
            return new ActionItemManagerWithParent(this, Items, parentName);
        }

        public IActionItemManager ShowItem(IActionItem item)
        {
            Items.Add(item);
            return this;
        }

        /// <summary>
        /// A nested item manager for making setting the scope of all added items.
        /// </summary>
        class ActionItemManagerWithScope : IActionItemManager
        {
            private readonly IActionItemManager parentManager;
            private readonly List<IActionItem> scopeItems = new List<IActionItem>();
            private readonly ObservableCollection<IActionItem> parentItems;

            public ActionItemManagerWithScope(IActionItemManager parentManager, ObservableCollection<IActionItem> parentItems, object model)
            {
                this.parentManager = parentManager;
                this.parentItems = parentItems;

                //check if the model implements any activation interfaces
                var activatable = model as IActivate;
                if (activatable != null)
                    activatable.Activated += ScopeActivated;

                var deactivatable = model as IDeactivate;
                if (deactivatable != null)
                    deactivatable.Deactivated += ScopeDeactivated;
            }

            public void ScopeActivated(object source, ActivationEventArgs e)
            {
                foreach (var actionItem in scopeItems)
                    actionItem.Activate();
            }

            public void ScopeDeactivated(object source, DeactivationEventArgs e)
            {
                foreach (var actionItem in scopeItems)
                    actionItem.Deactivate(e.WasClosed);
            }

            public IActionItemManager WithScopeOf(object model)
            {
                //if WithScopeOf is called again on this scope we can just return this (since we are already in the scope).
                return this;
            }

            public IActionItemManager WithParent(string parentName)
            {
                return new ActionItemManagerWithParent(this, parentItems, parentName);
            }

            public IActionItemManager ShowItem(IActionItem item)
            {
                //pass the instance on to the parent manager.
                parentManager.ShowItem(item);
                //also keep a reference for activation
                scopeItems.Add(item);
                return this;
            }
        } //end class ActionItemManagerWithScope


        /// <summary>
        /// A nested item manager for nesting the items. "WithParent" can be called recursively to drill down into the hierarchy.
        /// </summary>
        class ActionItemManagerWithParent : IActionItemManager
        {
            private readonly IActionItem parent;
            private readonly ObservableCollection<IActionItem> parentItems;

            public ActionItemManagerWithParent(IActionItemManager parentManager, ObservableCollection<IActionItem> parentItems, string parentName)
            {
                this.parentItems = parentItems;

                parent = parentItems.FirstOrDefault(item => item.Name == parentName);
                if (parent == null)
                {
                    //if there is no parent item create it and add it to the sibling collection
                    parent = new ActionItem(parentName, null);
                    parentManager.ShowItem(parent);
                }
            }

            public IActionItemManager WithScopeOf(object model)
            {
                return new ActionItemManagerWithScope(this, parentItems, model);
            }

            public IActionItemManager WithParent(string parentName)
            {
                return new ActionItemManagerWithParent(this, parent.Items, parentName);
            }

            public IActionItemManager ShowItem(IActionItem item)
            {
                parent.Items.Add(item);
                return this;
            }
        } //end class ActionItemManagerWithParent


    }//end class ActionItemManager
}
