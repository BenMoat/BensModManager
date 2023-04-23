[![GitHub release](https://img.shields.io/badge/version-2.0.0-blue)]()

<h1 align="center">
 <b>Ben's Mod Manager · Version 2.0</b>
</h1>

⚠️ Major version bump due to the how the file system now works and interacts with the iLovePDF API. 

## Features
### Image to PDF Converter and Merger: 
A way to upload and merge multiple PDF or images files into one PDF using the [iLovePDF](https://developer.ilovepdf.com/docs/api-reference#introduction) API.
* Upload one or multiple image files - PNG or JPG. It'll then convert those images to a PDF, and merge those PDFs together. 
  * [Uploading one image](https://github.com/BenMoat/BensModManager/blob/dev/Controllers/ModsController.cs#L189-L201) file will simply convert the file to a PDF, rename the PDF to the image's name, then delete the original image file(s). 
  * [Uploading multiple images](https://github.com/BenMoat/BensModManager/blob/dev/Controllers/ModsController.cs#L205) will follow the same process, however, each file name will be appended with _"-unmerged"_ until the loop of converting images to PDFs is complete. 
* There is logic in place to [ignore this API call](https://github.com/BenMoat/BensModManager/blob/dev/Controllers/ModsController.cs#L190) if the file is already of a PDF format. 
* Here is the file system in action:
![UploadImages](https://user-images.githubusercontent.com/43743754/233851736-fd60f808-4908-4337-b903-9e2c1fb2fb8b.gif)

### Exclude Obsolete Mods option and Search Parameter:
* The table now has an asynchronous slider option to exclude obsolete mods. 
  * It is defaulted to false. 
  * It uses [Session Storage](https://github.com/BenMoat/BensModManager/blob/dev/wwwroot/js/site.js#LL1-#L17) to retain the slider selection when searching. 
![ObsoleteMod](https://user-images.githubusercontent.com/43743754/233852313-84badfd8-5fb6-4d24-9d0f-ef03a1f4d5e5.gif)

## Fixes and Improvements

### Total Price _and_ Mod Count:
* This works in a similar way to calculating the total price of either all mods, or the queried result set of mods. 
![image](https://user-images.githubusercontent.com/43743754/233852000-813e3708-33ac-4107-8c66-e9b3bee7bdc5.png)
* This feature also works asynchronously with the Exclude Obsolete Mods parameter. 

### Search URL Cleanser: 
* Previously, every search parameter would be appended to the URL, regardless of whether that parameter is populated or not. 
Now, it is [just the populated parameter(s)](https://github.com/BenMoat/BensModManager/blob/dev/Views/Mods/_ViewAll.cshtml#L218-L226) that is appended to the URL query. 
* This works in tandem with the Exclude Obsolete Mods parameter. 

### Other fixes & improvements:
* [Dynamically actionable search button](https://github.com/BenMoat/BensModManager/blob/dev/Views/Mods/_ViewAll.cshtml#L190-L208): The search button is set as read-only unless it is populated by either the mod name or mod type. 
* Added a loading animation for whenever a mod is created or updated. 
  * This animation also applies to processing or replacing an invoice. 
* Added a ['Click to Copy'](https://github.com/BenMoat/BensModManager/blob/dev/Views/Mods/Invoice.cshtml#L79-L106) function in the Invoice modal for the filepath of where the PDF is saved. 
* Every Boostrap Modal now has a fade in and out animation. 
* Added support for Bootstrap's new [dark mode](https://getbootstrap.com/docs/5.3/customize/color-modes/#dark-mode). 

### [View all changes](https://github.com/BenMoat/BensModManager/pull/3/commits)
### [Previous Version Release Notes](https://github.com/BenMoat/BensModManager/releases/tag/v1.1.0)
