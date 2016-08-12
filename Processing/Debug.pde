// Simple Debug V0.2
public void debugMenu(){
    int alignLeftSpace = 20;
    int alignUpSpace = 20;
    // Setup
    textAlign(LEFT);
    fill(120, 255, 255);
    textFont(debugfont, 20);
    text("DebugMode = " + game.debugMode +"  GameisRunning = "+ game.gameRunning+ "     toggle DebugMode with F3", alignLeftSpace, alignUpSpace + 20);
    textFont(debugfont, 12);
    text("framesRendered = " + frames +"  GameUpdates = "+ ticks+"  FPS =  "+frameRate, alignLeftSpace, alignUpSpace + 0);
    text("Lag = "+ elapsedFrame +"   DeltaTime =  "+ deltaTime, alignLeftSpace+ 500, alignUpSpace + 0);

    textFont(debugfont, 16);
    // Input Reladed
    text("mouseX = " +mouseX +"   mouseY = " +mouseY, alignLeftSpace, alignUpSpace + 40);
    // Game Engine Related
    text("GlobalStatus = " + game.getGlobalStatus(), alignLeftSpace, alignUpSpace+ 60);
    text("GameStatus = " + game.getGameStatus(), alignLeftSpace, alignUpSpace+ 80);

    //Gui Related
    text("GlobalHitTest = " + game.gui.getGlobalHitTest(), alignLeftSpace, alignUpSpace+ 125);
    textFont(debugfont, 12);
    for (int i = 0 ; i < game.gui.buttonList.size(); i++){
    text("Button "+i+" = " + game.gui.buttonList.get(i).getHitTest(), alignLeftSpace, alignUpSpace+ 140 +(i*12));
    }
     for (int i = 0 ; i < game.gui.buttonList.size(); i++){
    text("pos.x = " + game.gui.buttonList.get(i).pos.x, alignLeftSpace+140, alignUpSpace+ 140 +(i*12));
    }
    for (int i = 0 ; i < game.gui.buttonList.size(); i++){
    text("pos.y = " + game.gui.buttonList.get(i).pos.y, alignLeftSpace +300, alignUpSpace+ 140 +(i*12));
    }
    textFont(debugfont, 16);
    textAlign(RIGHT);
    // Right Side
    // Framework Reladed
    text("AniTestButton = " +menuAniList.get(0).getFrame(), alignLeftSpace + (screenWidth*scaleFaktor-40), alignUpSpace + 0);
    //Kinect_Pointer Related
    text("GamePlayer = " + game.playerSelection.getPlayerStatus(0), alignLeftSpace + (screenWidth*scaleFaktor- 40), alignUpSpace + 20);

    if (game.skipGui == false){
        for (int i = 0 ; i < game.gui.pointerList.size(); i++){
        text("Pointer "+i+" Selected  = " + game.gui.pointerList.get(i).getSelected(), alignLeftSpace +  (screenWidth*scaleFaktor-40), alignUpSpace + 80+ (i*40));
        text("Pointer "+i+" Once = " + game.gui.pointerList.get(i).getSelectedOnce(), alignLeftSpace +  (screenWidth*scaleFaktor-40), alignUpSpace + 120+(i*60));
        }
    }

    if (game.skipGui == true){
    //GameWorld Related

    text("EnemyList = "  + game.gameWorld.enemyList.size(), alignLeftSpace+ (screenWidth*scaleFaktor-40), alignUpSpace+ 160);
    for (int i = 0 ; i < game.gameWorld.playerList.size(); i++){
    text("Pspeed.x "+i+" =  " + game.gameWorld.playerList.get(i).speed.x + "  Pspeed.y "+i+" =  " +
    game.gameWorld.playerList.get(i).speed.y, alignLeftSpace+ (screenWidth*scaleFaktor-40), alignUpSpace+ 180+(i*20));
    }
  }

//Debug


  //menuAniList.get(0).drawAnimation(600,600);
 // menuAniList.get(0).updateLoop(0);



}
