PasteE
======

Windows shell extension designed to paste clipboard contents into a new file.

I really have no experience writing programs in a professional environment and I usually only write simple VB.Net applications. This is my first project using GitHub and also my first time tackling something as complicated as Windows shell extensions. 

This extension is using Dave Kerr's SharpShell library as a base, so a big part of the credit goes to him. You can check out SharpShell in his GitHub repo here: https://github.com/dwmkerr/sharpshell. To actually deploy my shell extension, you will need to use SharpShell Server Manager or Server Registration Manager, both available in the repo I mentioned.

I wrote this extension in Visual Studio 2013, and I'm not sure if it will work with older versions. In any case, all of the code I've written is contained in PasteE.vb, all you need to do is start a new VB.Net Class Library project, reference SharpShell.dll and the built-in System.Drawing and System.Windows.Forms, and import PasteE.vb into the project. What else...

I'm aware that most shell extensions are written in C++, but right now I need to learn a bit more about developing shell extensions C++ in order to rewrite whatever I wrote for better compatibility, prettier (in my opinion) interface, and without dependency on SharpShell. I would appreciate any help you can give me in that regard.

Thank you,

/u/corgi92