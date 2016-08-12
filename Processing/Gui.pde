public class Gui
{
    public color activeColor, bgColor, fgColor;
    protected ArrayList<Gui_Button> buttonList;
    protected ArrayList<Kinect_Pointer> pointerList;
    protected ArrayList<FrameText> textList;
    public PVector pos, offset, scale, buttonScale, buttonSpace, alignSpace, spacing, textPos;
    public int highscore, scaleFaktor, fontsize, type;
    public boolean globalHitTest, show;

    Gui(){
        this.buttonList = new ArrayList<Gui_Button>();
        this.pointerList = new ArrayList<Kinect_Pointer>();
        this.textList = new ArrayList<FrameText>();
        this.buttonSpace = new PVector(screenHeight/12+2,0);
        this.scale = new PVector (0,0);
        this.scaleFaktor = 1;
        this.buttonScale = new PVector(300*this.scaleFaktor,100*this.scaleFaktor);
        this.pos = new PVector(screenWidth/2-(this.buttonScale.y/2),screenHeight/2+this.buttonSpace.x);
        this.offset = new PVector(0*this.scaleFaktor, 0*this.scaleFaktor);
        this.activeColor = color(200,90,100);
        this.bgColor = color(200,20,20);
        this.fgColor = color(200,120,50);
        this.fontsize = 64*this.scaleFaktor;
        this.highscore = 0;
        this.textPos = new PVector(24*this.scaleFaktor,64*this.scaleFaktor);
        this.alignSpace = new PVector(10*this.scaleFaktor,10*this.scaleFaktor);
        this.spacing = new PVector(140*this.scaleFaktor,20*this.scaleFaktor);
        this.globalHitTest = false;
    }

    // Create a Simple Menu_Button and add it to this List
    void createButton(PVector _pos, String _buttonName, int _status, String _type, String _transition){
        Gui_Button button = new Gui_Button(_pos, _buttonName, _status, _type, _transition);
        this.buttonList.add(button);
    }
    // Create a Advanced Menu_Button and add it to this List
    void createButton(PVector _pos, PVector _scale, String _buttonName,
    int _fontSize, PVector _text, int _status, String _type, PFont _font, String _transition){
        Gui_Button button = new Gui_Button(_pos, _scale, _buttonName, _fontSize, _text, _status, _type, _font, _transition);
        this.buttonList.add(button);
    }

    // Create a Animations Button with FrameWork
    void createButton(PVector _pos, String _buttonName,int _status, String _type, String _transition, FrameWork _image){
        Gui_Button button = new Gui_Button(_pos, _buttonName ,_status, _type, _transition, _image);
        this.buttonList.add(button);
    }

    void createPointer(int _player){
        Kinect_Pointer pointer = new Kinect_Pointer(_player);
        this.pointerList.add(pointer);
    }

    void createText(PVector _pos,PVector _offset,int _fontSize, String _text,color _textColor, String _type){
        FrameText text = new FrameText(_pos, _offset,_fontSize,_text,_textColor,_type);
        this.textList.add(text);
    }

    // Render Lists and BG
    private void render(){
        switch(this.type){
        case 0:
          
        case 1:
            renderLists();
        break;
        }

    }

    // Create Template
    private void create(String _type){
        // (PVector _pos, String _buttonName, int _status, String _type, String _transition)
        // (PVector _pos, PVector _scale, String _buttonName, int _fontSize, PVector _textAlign, int _status,String _type, PFont _font, String _transition)
        // (PVector _pos, int _status, String _type, String _transition, FrameWork _image)

        if(_type == "startMenu"){
         // this.createButton(new PVector((screenWidth/2)-(this.buttonScale.x/2.5),520), "Play"    ,3, "global", "none", menuAniList.get(0)); // Animated TestButton
            this.createButton(new PVector((screenWidth/2)-(this.buttonScale.x/2),600), "Play",    3,"global","swipeInRandom");
            this.createButton(new PVector((screenWidth/2)-(this.buttonScale.x/2),720), "Options", 5,"global","swipeInRandom");
            this.createButton(new PVector((screenWidth/2)-(this.buttonScale.x/2),840), "Exit",    8,"global","swipeInRandom");
            this.createText(new PVector(600,900),new PVector(0,0),72,"",color(250,190,255),"random");
            this.createText(new PVector(1920-160,1080-10),new PVector(0,0),24,"Version: "+ buildVersion ,color(250,255,255),"plain");
        }
        if(_type == "gameSelectionMenu"){

            this.createButton(new PVector(120,120),                 "Catch!",     0,"gameSelection","swipeSide");
            this.createButton(new PVector(120,this.spacing.x+120),  "Wettrennen!",1,"gameSelection","swipeSide");
            this.createButton(new PVector(120,this.spacing.x*2+120),"Hüpfen!",    2,"gameSelection","swipeSide");
            this.createButton(new PVector(120,this.spacing.x*3+120),"Parkour!",   3,"gameSelection","swipeSide");

            this.createButton(new PVector(1500,120),                 "Player1",0,"playerSelection","swipeSide");
            this.createButton(new PVector(1500,this.spacing.x+120),  "Player2",1,"playerSelection","swipeSide");
            this.createButton(new PVector(1500,this.spacing.x*2+120),"Player3",2,"playerSelection","swipeSide");
            this.createButton(new PVector(1500,this.spacing.x*3+120),"Player4",3,"playerSelection","swipeSide");

            this.createButton(new PVector(300,800), "Lets Play!",   2,"playSelection", "swipeSide");
            this.createButton(new PVector(1320,800), "Back to Menü", 6,"backSelection", "swipeSide");
        }
        if(_type == "optionsMenu"){
            this.createButton(new PVector(120,120),                 "Musik",        2,"gameSelection","swipeSide");
            this.createButton(new PVector(120,this.spacing.x+120),  "Sound",        3,"gameSelection","swipeSide");
            this.createButton(new PVector(120,this.spacing.x*2+120),"Debug Modus",  4,"gameSelection","swipeSide");
            this.createButton(new PVector(120,this.spacing.x*3+120),"Skip Frames",  5,"gameSelection","swipeSide");    
            
            this.createButton(new PVector(1320,800), "Back to Menü", 7,"backSelection", "swipeSide");
        }
    }


    boolean runHitTest(){

		int hitTests = 0;

    	for(int i = 0; i < this.buttonList.size(); i++){
			if( this.buttonList.get(i).getHitTest() == true){
				hitTests++;
			}
		}

		if (hitTests > 0){
			return true;
		}
		else{
			return false;
		}
	}

    // Translate the Menu
    void setOffset(float _offsetX, float _offsetY){
        this.offset.set( _offsetX, _offsetY);
    }

    // Clear all Buttons from List
    void clearButtons(){
        this.buttonList.clear();
    }
    // Clear all Pointers from List
    void clearPointer(){
        this.pointerList.clear();
    }
    void clearText(){
        this.textList.clear();
    }

    void clearLists(){
         this.buttonList.clear();
         this.pointerList.clear();
         this.textList.clear();
    }

    boolean getShow(){
    return this.show;
    }

    // Render Highscore at specific Position
    void displayHighscore(int _x, int _y){
        textFont(gamefont, 24);
        textAlign(RIGHT);
        fill(255, 0, 0);
        text("Highscore: "+ game.highscore, _x, _y);
    }

    boolean getGlobalHitTest(){
        return this.globalHitTest;
    } 
    void setScaleFaktor(int _scaleFaktor){
       this.scaleFaktor = _scaleFaktor;
    }
    void setRender(String _type)
    {
        if(_type == "titleScreen"){
            this.type = 0;
        }
        if(_type == "gameScreen"){
            this.type = 1;
        }
    }

    public void renderLists(){
        for (int k = 0; k < this.textList.size() ; k++){
            this.textList.get(k).render();
        }
        for (int i = 0; i < this.buttonList.size() ; i++){
            this.buttonList.get(i).render();
        }
        for (int i = 0; i < this.pointerList.size() ; i++){
            this.pointerList.get(i).render();
        }
    }

    //Update Buttons - Check Interaction - Set Status
    void updateLists(){

        for (int j = 0; j < this.buttonList.size() ; j++){

            this.buttonList.get(j).setOffset(this.offset.x, this.offset.y);
            this.buttonList.get(j).update();
            if(this.buttonList.get(j).getFinish() == true){
                this.buttonList.remove(j);
            }

            for(int k = 0; k < this.pointerList.size() ; k++)
            {
                    if (this.buttonList.get(j).getHitTest() == true && this.pointerList.get(k).getSelected() == true && this.buttonList.get(j).getSelected() == false && this.buttonList.get(j).getInTransition() == false){
    
                        // Singe Special Button Logic
                        if (this.buttonList.get(j).getType() == "global"){
                            this.pointerList.get(k).setSelected(false);
                            game.setGlobalStatus(this.buttonList.get(j).getStatus());
                        }
                        if (this.buttonList.get(j).getType() == "backSelection"){
                            this.pointerList.get(k).setSelected(false);
                            this.pointerList.get(k).setSelectedOnce(false);
                            game.setGameStatus(this.buttonList.get(j).getStatus());
    
                        }
                        if (this.buttonList.get(j).getType() == "playSelection"){
                            this.pointerList.get(k).setSelected(false);
                            this.pointerList.get(k).setSelectedOnce(false);
                            game.setGameStatus(this.buttonList.get(j).getStatus());
    
                        }
    
                        if (this.pointerList.get(k).getSelectedOnce() == true){
    
                            }
                        else{
                            if (this.buttonList.get(j).getType() == "playerSelection"){
    
                                game.playerSelection.setPlayerStatus(this.pointerList.get(k).getPlayer(),this.buttonList.get(j).getStatus());
                                this.buttonList.get(j).setSelected(true);
                                this.pointerList.get(k).setSelectedOnce(true);
                                this.pointerList.get(k).setSelected(false);
    
                            }
                            if (this.buttonList.get(j).getType() == "gameSelection"){
    
                                game.playerSelection.setWorldID(this.buttonList.get(j).getStatus());
                                this.buttonList.get(j).setSelected(true);
                                this.pointerList.get(k).setSelectedOnce(true);
                                this.pointerList.get(k).setSelected(false);
    
                            }
    
                        }
                    }
             }
        }


        this.globalHitTest = runHitTest();

        if (globalHitTest == true){

        }
        else{
        }

        for (int i = 0; i < this.pointerList.size() ; i++){
            this.pointerList.get(i).update();
            this.pointerList.get(i).setHitTest(this.globalHitTest);
        }
        for (int k = 0; k < this.textList.size() ; k++){
        this.textList.get(k).update();
        this.textList.get(k).render();
        }
    }

}