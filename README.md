[![GitHub release](https://img.shields.io/badge/version-1.0.0-blue)]()

<h1 align="center">
 <b>Ben's Mod Manager · Version 1</b>
</h1>

## Features:
### Local File System:
#### Invoices for each record are housed in a solution-local folder called 'files'. 
* For security purposes, this folder is [.gitignore](https://github.com/BenMoat/BensModManager/blob/master/.gitignore#L12)'d: 
  * If you have cloned the solution or deleted the folder, it will be [automatically created/recreated](https://github.com/BenMoat/BensModManager/blob/master/Controllers/ModsController.cs#L96-#L98) when running the solution and adding a file. 
* When replacing or deleting an invoice from a record, the file system will do the housekeeping:
  * If a record is deleted, [so is the invoice](https://github.com/BenMoat/BensModManager/blob/master/Controllers/ModsController.cs#L91-#L94) that is attached to it. 
  * If a record's invoice needs to be replaced - _because getting things right on the first try isn't my forte_ - the original invoice will be deleted and replaced with the newly added one. 
* In a later release, the file system should also be able to detect if a file is manually deleted and update the affected record accordingly. 
Right now, the iframe to display a nonexistent file embeds the 404 page - [a great 404 page though](https://imgur.com/a/V1lxjfJ). 🏎️☀️



### The ability to embed a _local_ file on the website:
#### By default, a brower's security policy will not allow a third-party site to request to open a local "file:///". 
* To get around this, the 'files' folder is stored in the _wwwroot_ folder. By concatenating the file and extension, prefixed with the "_~/files/_" directory, you can [call the file straight from the site domain](https://github.com/BenMoat/BensModManager/blob/master/Views/Mods/Invoice.cshtml#L60), instead of using the file:/// prefix. 
  * For example:
  This: `file:///C:/Users/JoeBloggs/Documents/Mods/Upload%20Image%20Test.pdf`
  Turns to this: `https://localhost:5001/Files/Upload%20Image%20Test.pdf`

### Asynchronous total price calculation:
#### Just incase looking at the Price column isn't already painful enough 💸
* The [total price](https://github.com/BenMoat/BensModManager/blob/master/Views/Mods/_ViewAll.cshtml#L92) of the modifications is governed by the parameters set for it. 
  * The site will load by default with the full list of modifications. The price will change depending on what you are searching for. For example, filtering the modifications by Performance will automatically show how much money you have spent on performance parts. This also works for the plain text search either independently or in tandem with other search parameters. 
