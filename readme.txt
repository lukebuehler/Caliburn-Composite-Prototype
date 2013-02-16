Note: This project is out of date. 
I'm keeping it here as a reference, for a really good example of a composite application in Caliburn.Micro take a look at Gemini:
https://github.com/tgjones/gemini

===============================================
This is a small prototype to see how a PRISM like composite application could be done only with Caliburn.Micro.

The libraries were installed with NuGet.

What does it do?
 * Shell with different "regions" where modules can insert their content, e.g. menus, toolbars, tab with workspaces.
 * Modules are loaded from a directory (so no reference to modules in the shell).