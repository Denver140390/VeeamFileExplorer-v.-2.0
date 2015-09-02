* About program
Framework version: 4.5.2

The program is designed to browse local file system.
UI consists of 2 views: directories tree view and currently-opened-directory-content view (DataGrid).
Selecting a folder in directoies tree fills DataGrid with corresponding data.
Right mouse button click on directories tree opens context menu with "Open in Windows Explorer" item.
Double Left Mouse Button click on row in DataGrid opens corresponding folder or file.

* About code
In this File Explorer I followed MVVM principles of separating business logic (Models) from UI (Views) by connecting them with bridges (ViewModels). Pretty usual.

Instead of creating custom Models, DirectoryInfo and FileInfo classes are used, since they provide all required properties and methods.

Thus DirectoryViewModel and FileViewModel instances have an instance of DirectoryInfo or FileInfo classes.

Views are showing cool stuff and catching user inputs. Code-behind is used only to catch MouseDoubleClick on DataGridRow.
Implemented View-First approach. ViewModel objects are being created in Views.

Directories are loading asyncronously on TreeView item selection or expanding.

* Possible improvements
Add comments in the code.
Interactive DirectoryContentView - open folders and files, copy, paste and other usual Windows Explorer features.
Multiple ContentViews - for comparison.
Navigation Buttons - backward and forward actions.
Asynchronous Loading - load as much info as possible asynchronously (load subfolders aswell to improve UX). Without affecting too much RAM tho.
Add settings and serialize them.
Cool green-styled UI.
A LOT MORE...