public class Hud extends Gui{

    private int worldID;

    Hud(PVector _pos,int _worldID){
        this.buttonList = new ArrayList<Gui_Button>();
        this.pointerList = new ArrayList<Kinect_Pointer>();
        this.textList = new ArrayList<FrameText>();

        this.bgColor = color(200,20,20);
        this.fgColor = color(200,120,50);
        this.globalHitTest = false;

        this.show = true;
        this.pos = _pos;
        this.scale = new PVector(0,0);
        this.offset = new PVector(0,0);
        this.worldID = _worldID;
        this.offset = new PVector(0, 0);
    }


  void create(int _worldID){

    switch(_worldID){
        case 0:
           this.createText(new PVector(1920-160,1080-10),new PVector(0,0),24,"Version: "+ buildVersion ,color(250,255,255),"plain");
        break;
        case 1:

        break;
        case 2:

        break;
        case 3:

        break;
    }

  }

  void update(){
  this.updateLists();


  }

  void render(){
  this.renderLists();


  }


}
