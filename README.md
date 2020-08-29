



# CatchBall 
<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width">

</head>
<body>
 Android game made by Unity2018 <br>
  https://play.google.com/store/apps/details?id=com.Wallace92.CatchBall  <br>
  Responsie arcade game CatchBall consist of 5 scenes and 22 scripts (over 8000 lines of code). A lot of different assets like vector graphcis created using Inkscape program or music downloaded from freesounds.com. Each script posses some unique feature used in these scenes (loaded or unloaded addtively).
  
<ul>
  <li> <h2> Background and Menu scenes </h2></li>
  <p>
 These two scenes are loaded in the first order preceding by loading progres bar (<b>LoadingScreenScript.cs</b>). The background scene loads movable background images (<b>BackgroundDontDestroy.cs</b>). In the menu scenes all essential buttons  (start game, mute music and show leaderboard) are shown. In this moment the levels are generated or loaded from files using  (<b>swap.cs</b>) script, in the background music starts to play (<b>DontDestroy.cs</b>) and (<b>MusicControl.cs</b>). Leaderboard inicjalization requires own script (<b>LeaderboardInitialization.cs</b>) as well here an sign in to Google Plays Api appears by (<b>PlayGamesScript.cs</b>). Each created level posses unique dictionaries with velocities and times as values and level numbers as key. Music can be muted in free time. 
</p>
  <p>
   <li> <h2> LevelChanger and Level Easy/Mediumn/Hard scenes </h2></li>
  <li>After start game button pressed the menu scenes is unloaded and LevelChanger (<b>LevelChangerController.cs</b>) loaded. The player can choose between three different difficulty levels Easy/Medium/Hard which load appropriate scenes and scripts controllers (<b>LevelEasyController.cs</b>, <b>LevelMediumController.cs</b>, <b>LevelHardController.cs</b>) correspondingly. Each level is a prefab (sprites + text) spawned by this controllers. All prefabs can also move by Touch the screen and move the finger (<b>MoveCanvas.cs</b>). The children of text number of each prefab is a key for appropriate dictionary of levels and will loaded appropraite parameters to each levels before game will start. </li>
  </p>
  
   <p>
   <li> <h2> Game Scene </h2></li>
  <li>The core scene of this app governed by (<b>GameController.cs</b>) script. At the begging some prefabs are instantiated at appropriate positions including stars, balls and text. The start are loaded for each levels in different positions using (<b>StarController.cs</b>) next the dots are instantiated by (<b>DotControler.cs</b>). When one dot posses some random coordinates at the scren, the second one cannot have the same values and the same with the third one therefore while loop allow to avoid these circumstances. After instantate of the balls the are forced to move in some random directions and collide with the wals and to each others (<b>DotMove2.cs</b>). Due to satisfy the game assumtions these velocities are normalized after each collision and the balls posses bouncing materials. Therefore each collision are completely bouncy witohut of energy loss. OnMouseOver function is assosiated with the balls and when the color of the ball is the same as the text at the top of the screen the player will achieve one brighty star. The text at the top can be completely black (<b>ColorTextController_black.cs</b>), colorful (<b>ColorTextController_rgb.cs</b>) and even black and colorful (<b>ColorTextController_rgb_black.cs</b>) which depends of the levels difficulty. In order to make this game more engaging the time of each level is changing and its controller by (<b>Timer.cs</b>) script which is a component of the rect sprite. <br> </li>
  </p>
  
</ul>
The player reaction times (best and average of the rounds) are shown at the screen after each level is passed. The corresponding scores of the leves depends of these times linearly with some build in coefficient. Every scores of all round are saved to appropriate files after each round and submited to the leaderboard if singIn after game start was without errors. Additionally the scores are displayed every round by (<b>CoinTextScript.cs</b>). To satisfy some earning from the app user can gain 500 points for the round by clicking the rewarded button (<b>RewardedAdsButton.cs</b>). In the future I would like to add multiplayer options. Two or more people of differents devices will trying to catch the ball first before the oters users. 
</body>
</html>
