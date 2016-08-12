// Import Lyberies Here
import ddf.minim.*;

// Create Engines Instances
Minim minim;


// Public Variables
public static final int screenWidth = 1920; // Kinect
public static final int screenHeight = 1080;
public String buildVersion = "0.1a Alpha";
public int scaleFaktor = 1;
public int soundBufferSize = 256;
public int maxFrameRate = 120; //  VSync
public int FIXEDUPDATERATE = 1000/60; // 60 hz fixed Timestep
public double deltaTime, previousFrame, elapsedFrame;
public PFont debugfont, gamefont;
public JSONArray savefile;
public float behindFrame;
public int ticks, frames;

// Main Classes
public Game game;
public GameSave gameSave;

//Framework
public ArrayList<FrameWork> menuAniList;
public FrameWork bgImages;

void settings() {
  //size(960, 540,FX2D); // Screen Dimensions
  fullScreen(FX2D);  // Fullscreen Renderer
  smooth(2); //anti-aliased 2x
}


void setup(){
    // setup
    colorMode(HSB, 360, 255, 255, 255);
    frameRate(maxFrameRate);// Max FrameRate
    noCursor();
    deltaTime = millis();
    behindFrame = 0;
    scaleFaktor = getScaleFaktor();


    //Start Instances


    // Load Assets
    debugfont = loadFont("SourceCodePro24.vlw");
    gamefont = loadFont("MechaC48.vlw");
    //  initialize ArrayLists

    menuAniList = new ArrayList<FrameWork>();

    // Load Images into FrameWork
    // FrameWork(String _imagePrefix, String _imageExt, int _xTileP, int _yTileP, int _scaleX, int _scaleY, int _timeDelay)
    FrameWork button_play_test = new FrameWork("tex/TestButtonPlay", ".png", 1, 30, 250,50 ,60);

    // Add Framework to ArrayLists
    // Access thourgh {listname.get(Array)}
    menuAniList.add(button_play_test);

    bgImages = new FrameWork("tex/BGTest_",".png", 1 ,2, 250);


    // Start Mainclasses
    game = new Game();
    gameSave = new GameSave(3);


}

// Updateloop
void draw(){

    deltaTime = millis();
    elapsedFrame = deltaTime - previousFrame;
    ticks++;
    behindFrame += elapsedFrame;

    if (behindFrame >= FIXEDUPDATERATE){
        game.update();
        behindFrame -= FIXEDUPDATERATE;
    }

    game.render();
    frames++;
    previousFrame = millis();
}


//Stop Game
void stop(){
    minim.stop(); // Kill Soundthread
    super.stop(); // End Programm
}

int getScaleFaktor(){

    int resolutionX = 1920;
    int math1;

    math1 = resolutionX / displayWidth;
    return math1;
}
