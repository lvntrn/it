public PFont gamefont;
public FrameText frameText;

void setup(){
    size(512,512);
    gamefont = loadFont("MechaC48.vlw");
    frameText = new FrameText(new PVector(250,90),new PVector(0,0));
}

void draw(){
  background(0);
  frameText.update();
  frameText.render();
}
