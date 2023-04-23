[![GitHub release](https://img.shields.io/badge/version-1.1.0-blue)]()

<h1 align="center">
 <b>Ben's Mod Manager · Version 1.1</b>
</h1>

## Features
### Sorting:
Columns can be asynchronously sorted by the [mod name, price or mod type](https://github.com/BenMoat/BensModManager/blob/master/Controllers/ModsController.cs#L61-#L76):
* Upon load, the table is sorted by the mod name in ascending order. 
  * Selecting a column header will sort it by ascending order at first, clicking it a second time will sort it by descending order and vice versa. 
    ![image](https://user-images.githubusercontent.com/43743754/228944337-a6a9d2d6-8dc4-46f1-85ac-d476f0de0ff6.png)

### Red and green PDF symbols:
* Added Font Awesome PDF symbols to each record to make it easier to tell if a mod as the invoice attached to it or not.
  * A Green PDF symbol means a file is attached to the mod. 
  * A Red PDF symbol  means there is _no_ file attached to the mod. 
    ![image](https://user-images.githubusercontent.com/43743754/228944088-4dd03b61-544d-43b3-8c0c-8b9994dfb698.png)

## Fixes & Improvements
### The total price calculation:
This now remains static at the total price of _all_ mods, not the queried mods. 
* When querying a mod, a second price will be displayed to show the total price of the queried mods _on top_  of the total price of mods. 
    ![image](https://user-images.githubusercontent.com/43743754/228947398-f646db60-609d-4151-9335-3f0239430c79.png)
   _(here is how money I ~wasted~ spent on interior carbon parts)_ 

### Replaced the local array of mod types in the site.js file with an AJAX call to the [controller](https://github.com/BenMoat/BensModManager/blob/master/Controllers/ModsController.cs#L97-L106):
![image](https://user-images.githubusercontent.com/43743754/228950225-2c8a8406-a791-4da1-9ab1-0f55e3673866.png)

### Other fixes & improvements:
* Fixed a TomSelect source mapping issue when trying to launch the web app in Debug mode. 
* The mod notes' height now [animates](https://github.com/BenMoat/BensModManager/blob/master/wwwroot/js/site.js#L60-#L62) to the height of the content inside of it. 
* Globalised the site's font colour and reduced the HTML noise from repetitive class references. 
* Reorganised the site.css file to make it easier to read. 
* Replaced all local libraries with CDNs. 

### [View all changes](https://github.com/BenMoat/BensModManager/pull/1/commits)
### [Version 1.0.0 Release Notes](https://github.com/BenMoat/BensModManager/releases/tag/v1.0.0)
