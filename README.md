
***CourseDrawer*** 
=================

External tool to draw and edit Courseplay courses directly to the XML File 

CourseDrawer is an application that has been updated and prepared for working with the new version of CoursePlay 5.0 (FS2017, LS17)

Courses are now saved by courseplay in individual files. In order to load the courses you still need to load courseManager.xml which you can find here:
+ Goldcrest Valley Map: %USERPROFILE%\Documents\my games\FarmingSimulator2017\CoursePlay_Courses\Map**01**\courseManager.xml
+ Sosnovska Map: %USERPROFILE%\Documents\my games\FarmingSimulator2017\CoursePlay_Courses\Map**02**\courseManager.xml

In the Install Path of the game, for example **D:\Spiele\Farming Simulator 2017\data\maps**, 
you can find the background images you can load in CourseDrawer for your map. You can load the bmp or the dds file.

**How to Get it?**

See in the Release Section of this Fork for the newest Beta version. If you need a newer version you can Clone it and Compile it yourself in Visual Studio or Commandline  but i can't give you support there

Please be aware you're using a developer version, which may and will contain errors, bugs, mistakes and unfinished code. Chances are you computer will explode when using it. Twice. If you have no idea what "beta", "alpha", or "developer" means and entails, steer clear. The team will not take any responsibility for crop destroyed or savegames deleted. You have been warned.

If you're still ok with this, please remember to post possible issues that you find in the developer version. That's the only way we can find sources of error and fix them. Be as specific as possible.

Download: See Releases

**When is a newer Version coming?**

You can track the Progress in the Project tab of this Repository.

If you are a Developer and want to use the Testfiles in the Repo, please run the command 
" git update-index --skip-worktree TestFiles/ "
after you cloned the repo. You have then your own copy of  the files withot the changes beeing tracked.

Credits
    
    TinyCrocodile

Former Developers

    Pseudex
    PromGames
    Madbros  
    Satis
    horoman
    Original by Pawpouk

Last Beta Version Published: CourseDrawer 5.3.000-beta

Changelog:
5.3.000
+ Selecting Waypoints with arrow buttons.
+ Set Waypoint as Unloadpoint
+ Minimum Zooming set to 10% 
+ Zoomfactor is visible
+ Fixed Selecting area of Waypoints not matching the circle
+ Fixed Bug selecting Waypoints by ID
+ Fixed Bug Speed saved as Angle
+ Fixed Bug Waypoint Position not Updeated when changed by Textboxes

5.2.000
+ New selecting of Mapsize
+ Course selection improved
+ Bug loading new Coursemanager fixed
+ Minor bug fixes

5.1.000
+ new listbox with better reference to course
+ Tooltip for every course with detailed information
+ change waypoint values with [ENTER]
+ colors waypoint input fields
+ radius anchor for selecting waypoints reduced

5.0.000
+ compatibility for CoursePlay 5 files

