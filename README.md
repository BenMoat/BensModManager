[![GitHub release](https://img.shields.io/badge/version-0.2.0-blue)](https://github.com/BenMoat/Forza-Colour-Search/releases/tag/v0.2.0)

<h1 align="center">
 <b>Forza Colour Search · Latest Release Notes</b>
</h1>

## End User Changelog
### Full site Dark Mode
  * The site is now fully supported with the dark theme, with a few styling changes to keep it within its theme. 
### Updated Homepage
* The homepage now has two clickable sections to direct you to either the vehicle colour codes or the wheel colour codes. 
  **The wheel colour codes redirect you to the vehicle colour codes whilst the wheel section of the site is being developed.**
### Creating, Updating and Deleting Records
* Individual pages for these actions have been replaced with Popups. 
  * The colour codes for the popup contain a set format that ensures the code you have entered is correct. 
  For example, the system will not allow you to enter a colour code that is greater than 1.00 as that is the maximum value for a colour's parameter. After the numerical code, you can either enter 'L' or 'R'. L: Left click (start of value) R: Right click (before next value)​. 



## Technical Changelog
### Migration to Bootstrap 5
* The site now adopts global Bootstrap 5 styling attributes, including the use of [Modals](https://getbootstrap.com/docs/5.0/components/modal/#modal-components) and Forms for CRUD popups. 
### Navigation changes
* The Homepage now has direct link via a clickable card to the Vehicle view. 
### Pagination 
* [PagedList](https://github.com/troygoode/PagedList#important-this-package-is-no-longer-maintained-please-see-ernado-xxpagedlist-for-a-drop-in-replacement) is no longer maintained so it has been replaced with the supported forked version, [X.PagedList](https://github.com/dncuug/X.PagedList#what-is-this). 
### Replacement of static CRUD pages with jQuery popups. 
* Static pages for _CUD_ services have been removed and replaced with [jQuery Ajax popups](https://github.com/BenMoat/Forza-Colour-Search/blob/master/wwwroot/js/site.js). 
### Regular Expression for colour codes set via a [custom alias](https://github.com/RobinHerbots/Inputmask/wiki/Howto:-Effectively-using-the-data-inputmask-attribute#data-inputmask--aliases-combination) set in Robin Herbot's [InputMask](https://github.com/RobinHerbots/Inputmask#inputmask) plugin. 
* The colour code input mask has [its own alias definition](https://github.com/BenMoat/Forza-Colour-Search/blob/4f4866952d0dbf1ac4059397bfb43af6a93c3468/wwwroot/lib/jquery/dist/jquery.inputmask.js#L942-L947). 
  * This makes the code a lot less noisy as all the alias options are only defined once. The alias can then be called upon by typing ```data-inputmask="'alias': 'colourCode'"``` in the respective HTML file. 
#### Minor Changes
* Updated the site footer with developer's GitHub link.
* Added BrowserLink to the [Startup.cs](https://github.com/BenMoat/Forza-Colour-Search/blob/4f4866952d0dbf1ac4059397bfb43af6a93c3468/Startup.cs#L35). 
* Removed deprecated migrations from early database testing.
* Removed unnecessary Vehicles model. 
