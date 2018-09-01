# MCC_GameJamPGH
Moms Can Code Game Jam Pittsburgh game

# Play
You can [play Antartcicolor](https://simmer.io/@rlahoff/antarcticolor) through on slimmer.io. Your browser will need to support WebGL. If you get an error, search for your browser and enable WebGL to see how to make it work.

# Contributing
You can contribute by creating levels, submitting prefabs, identifying issues, and expanding knowledge base.

In order to contribute, you need to clone the repo and have Unity installed.

* If you do not have Unity, [Download Unity](https://store.unity.com/download?ref=personal)
* This repo uses Large File System, `git clone` will not work.
  - last check, repo was about 282 MB
  - on Mac, `brew install git-lfs` then `git-lfs clone ...`

## Set the game up in Unity
* Run Unity
* At the first screen, select Open.  
* Then select the folder (AntarctiColor or MCC_JamPGH) where you placed the clone.  Wait quite a while Unity gets the project set up (just the first time).

## Create Levels
**It is extremely important to build levels out of prefabs.**

The only items that will not be prefabs-
	* Your background
	* The Canvas
  * The EventSystem (created when you create the Canvas to hold the user interface)
  * BlockPlayer (see Level 4 or SharkFest - invisible colliders you create to make sure the player doesn’t pass through loosely spaced blocks

### Submitting a level
When a level is completed- (make sure there are no console warning or error messages)

In Unity, selected Assets > Export Package.  Select ONLY your level.  Export the package and email it to Renee.

### Submitting a prefab
Got a great idea for a new level element (prefab)?  Tell us about it!  If we decide to use it in the game create and test it in one of your levels.  When you’re happy with it submit it.

In Unity, selected Assets > Export Package.  Select ONLY your prefab(s).  Export the package and email it to Renee.
