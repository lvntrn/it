public class Game
{
    public Gui gui;
    public Hud hud;
    public Input input;
    public PlayerSelection playerSelection;
    public boolean gameRunning, debugMode, skipGui;
    public int gameStatus, globalStatus, highscore, loop;
    public color bgColor;
    public GameWorld gameWorld;

    //Konstruktor
    Game(){
        this.input = new Input();
        this.gameRunning = false;
        this.loop = 0;
        this.gameStatus = 0;
        this.globalStatus = 0;
        this.bgColor = color(0,0,0);
        this.debugMode = true;
        this.playerSelection = new PlayerSelection(3);
        this.skipGui = false; // Toogle Between Menü Development and GameDevelopment

    }

    void update(){

        switch(globalStatus){

        case 0:
            this.gui = new Gui();
            this.gameRunning = true;
        case 1:
            if (this.skipGui == false){
                this.gui.create("startMenu");
                this.gui.createPointer(0);
            }

            if (skipGui == true){
                this.hud = new Hud(new PVector(0,0),0); // set Hud for Spezific Game
                this.hud.createPointer(0);
                this.gameWorld = new GameWorld(0,0); // Set World for Spezific Game
                this.gameWorld.loadStats(); // load Save File
            }
            this.globalStatus = 2;
        break;
        case 2:

             if (this.skipGui == true){
                 this.gameWorld.update();
                 this.hud.update();
             }
             if (this.skipGui == false){
                 this.gui.updateLists();
             }

             if (gameRunning && game.input.getKeyF3()){
                 this.debugMode = !this.debugMode;
             }
             break;
           case 3: //PlayButton
            switch(gameStatus){

                case 0:
                      for(int i = 0; i < this.gui.buttonList.size(); i++){
                          this.gui.buttonList.get(i).setReverse();
                        }
                      this.gui.clearText();
                      this.gui.create("gameSelectionMenu");
                      this.gameStatus = 1;

                case 1:
                          this.gui.updateLists();


                        if (gameRunning && game.input.getKeyF3()){
                            this.debugMode = !this.debugMode;
                        }
                            this.updateLoop(360);
                            this.bgColor = color(this.loop,100,100);

                break;


                //Create Level
                case 2:
                this.gameWorld = new GameWorld(0,0);
                this.gameWorld.setMaxPlayer(this.playerSelection.getMaxPlayer());

                this.gameStatus = 3;
                // RunLevel
                case 3:
                this.gameWorld.update();
                break;
                // Unload GameWorld
                case 4:

                this.gameStatus = 5;
                case 5:

                break;
                //Back to Menü
                case 6:
                 for(int i = 0; i < this.gui.buttonList.size(); i++){
                       this.gui.buttonList.get(i).setReverse();
                    }
                this.gui.clearText();
                this.gui.create("startMenu");
                this.gameStatus = 0;
                this.globalStatus = 2;
                case 7:// Back to Startmenü
                this.gameStatus = 0;

                break;
            }
        break;

        case 5: //OptionsButton
             switch(gameStatus){

                case 0:
                      for(int i = 0; i < this.gui.buttonList.size(); i++){
                          this.gui.buttonList.get(i).setReverse();
                      }
                      this.gui.clearText();
                      this.gui.create("optionsMenu");
                      this.gameStatus = 1;

                case 1:
                          this.gui.updateLists();


                        if (gameRunning && game.input.getKeyF3()){
                            this.debugMode = !this.debugMode;
                        }
                            this.updateLoop(360);
                            this.bgColor = color(this.loop,100,100);

                break;
                case 7:
                for(int i = 0; i < this.gui.buttonList.size(); i++){
                    this.gui.buttonList.get(i).setReverse();
                }
                this.gui.clearText();
                this.gui.create("startMenu");
                this.gameStatus = 0;
                this.globalStatus = 2;
                break;

               }
        break;

        case 6:


        break;

        case 8: //Exit Button

            println("Exit");
            exit();
        break;

        case 10:     //Reset the Game  , for Later Use
                this.hud.clearLists();
                this.gui.clearLists();
                this.globalStatus = 1;

        break;


        }

      }


    //Render Update
    void render(){

        switch(globalStatus){

            case 0:
            case 1:
            break;
            case 2:

            if (this.skipGui == true){
                this.gameWorld.render();
                this.hud.render();
            }
            if (this.skipGui == false){
               bgImages.drawImage(0,0,0);
               bgImages.drawImage(0,0,1);
               this.gui.render();

            }
            if (this.debugMode == true){
                debugMenu();
            }

            break;
            case 3:
                switch(this.gameStatus){


                    case 0:
                    case 1:
                    case 2:
                    // Render Background
                        background(bgColor);
                        this.gui.render();


                    if (this.debugMode == true){
                        debugMenu();
                    }
                    break;
                    case 3:
                        this.gameWorld.render();

                        if (this.debugMode == true){
                            debugMenu();
                        }
                    break;
                }
           case 5:
                switch(this.gameStatus){
                    case 0:
                    case 1:
                    case 2:
                    // Render Background
                        background(bgColor);
                        this.gui.render();

                        if (this.debugMode == true){
                            debugMenu();
                        }
                    break;
                }
           break;
        }

    }

    // Setter & Getter
    boolean getGameRunning(){
        return this.gameRunning;
    } 
    void setGameStatus(int _setGameStatus){
        this.gameStatus = _setGameStatus;
    } 
    void setGlobalStatus(int _setGlobalStatus){
        this.globalStatus = _setGlobalStatus;
    }
    int getGameStatus(){
        return this.gameStatus;
    }
    int getGlobalStatus(){
        return this.globalStatus;
    }

    void updateLoop(int _limit){
        this.loop++;
        if (this.loop >= _limit)
        {
            this.loop = 0;
        }
    }
}
