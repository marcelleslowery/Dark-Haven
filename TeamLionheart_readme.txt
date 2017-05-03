Team Lionheart
John Blum - johnwblum@gmail.com - jblum7
Mathew Deeb - 
Sara Jacks - sara.jacks17@yahoo.com - sjacks3
Marcelles Lowery - mlowery30@gatech.edu - mlowery30
Alex Poole - alexpoole5751@gmail.com - apoole32

Requirements Completed:
	o 3D Game Feel Game
		- Each level has a way to succeed (go on to next level/finish by reaching end) or die.
		- Start menu 
		- Able restart game (Backspace key allows the play to restart at anytime)
	o Precursors to Fun Gameplay
		- Goals effectively communicated to the player (Text on HUD)
		- Interesting choices (Each puzzle)
		- Choices have consequences (death if wrong)
		- Choices engage the player with the world (ex. Buttons on the levels for interaction)
		- Balance of game play (Resources vs. Strategy)
		- In game learning (Main area safe zone)
	o Skeletal-Animated 3D Mesh Character Controller with Real-Time Control
		- Mecanim-controlled, Blendtree-enabled, player-controlled character
		- Player has direct, dynamic control of character movement
		- Choice of controls is appropriate and intuitive (magic controls are labeled for ease-of-use)
		- Fluid animation
		- Low latency responsiveness
		- Camera is smooth
		- Auditory feedback on character state and actions
	o 3D World with Physics and Spatial Simulation
		- Unique environment 
		- Appropriate boundaries (walls, death zones, etc.)
		- Interactive scripted objects (pressure plates)
		- Simulated Newtonian Physics rigid body objects (Barrel, coins, pendulum)
		- Animated objects using Mecanim (Treasure chest in main room)
		- State changing or destroyable objects (gargoyle in main room is breakable)
		- Constant spatial simulation throughout
	o Real-time NPC Steering Behaviors/AI
		- Multiple AI States (Patrol, pathfinding)
		- Smooth locomotion
		- Effective AI (use for puzzles)
		- Fluid animation
	o Polish
		- Overall UI is consistent
		- Transitions between scenes are aesthetic
		- Cohesive in style, lighting, color palette (dark)
		- Appeal: No glitches, no getting stuck, stable, etc.

Assets Used:
	o Torch; Purpose: Torches, lighting effects; URL: https://www.assetstore.unity3d.com/en/#!/content/7275
	o Gargoyles; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/27106
	o Chest; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/8952
	o Crate; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/31462
	o Barrel; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/50821
	o Lion; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/34247
	o Fountain; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/75912
	o Coin piles; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/27836
	o Coin textures; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/1810
	o Castle; Purpose: Environment and Textures; URL: https://www.assetstore.unity3d.com/en/#!/content/23699
	o Rocks; Purpose: Aesthetics; URL: https://www.assetstore.unity3d.com/en/#!/content/70208 and https://www.assetstore.unity3d.com/en/#!/content/19856
	o Fonts; Purpose: Game menu aesthetics; URL: http://www.dafont.com/ringbearer.font and http://www.dafont.com/chancery-cursive.font
	o Post Processing Stack, Purpose: , URL:
	o Coin sounds; Purpose: Sound aesthetics; URL: http://www.freesound.org/people/jalastram/sounds/223344/  and  http://www.freesound.org/people/corbanha/sounds/241971/  and  http://www.freesound.org/people/LittleRobotSoundFactory/sounds/270410/

Helpful Steps:
	o Main Room: 
		- Run into gargoyles for breakable objects
		- Play with rolling barrel
		- Run over coins for sound
		- Run into chest to see it open
		- Test your powers on the pendulum
		- Run into the hall opposite the spawn point
	o Levels:
		- Use your wits to run around and test powers
		- Falling into pit kills you
		- Respawn button and magic buttons displayed on HUD
		- AI can be seen using fluid movement

First Scene: "menu.unity"
