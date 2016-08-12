class FrameText{


public PVector pos, offset;
private float timeDelay, lastTime, textScale, textScaleSpeed, maxTextScale, minTextScale, radian;
public boolean loop;
public PFont font;
private String text;
private int type, fontSize;
private String splashArray[];
public color textColor;

FrameText(PVector _pos,PVector _offset,int _fontSize, String _text,color _textColor, String _type){
    this.pos = _pos;
    this.offset = _offset;
    this.textScale = 1;
    this.loop = false;
    this.font = gamefont;
    this.maxTextScale = 1.025;
    this.minTextScale = 0.975;
    this.lastTime = 0;
    this.timeDelay = 20;
    this.textScaleSpeed = 0.0015;
    this.radian = 0;
    this.text = _text;
    this.fontSize = _fontSize;
    this.loadSplash();
    this.textColor = _textColor;

    this.type = 0;
    if(_type == "plain"){
        this.type = 0;
    }
    if(_type == "spring"){
        this.type = 1;
    }
    if(_type == "random"){
        this.type = 10;
    }

}

void update(){

    switch(this.type){

    case 0:
    break;
    case 1:
        if (millis() - this.lastTime >= this.timeDelay){
            if (this.loop == false){
                this.textScale+=this.textScaleSpeed;
                this.radian+=this.textScaleSpeed*8;
                if (this.textScale > this.maxTextScale){
                    this.loop = true;
                }

            }
            if (this.loop == true){
                this.textScale-=this.textScaleSpeed;
                this.radian+=this.textScaleSpeed*8;
                    if (this.textScale < this.minTextScale){
                        this.loop = false;
                    }
            }
           this.lastTime = millis();
       }
    break;
    case 10:
    this.text = this.getRandomText();
    this.type = 1;
    break;
    }
}


void render(){

  switch(this.type){
      case 0:
      // Render random Text at specific Position
          textFont(this.font, this.fontSize);
          textAlign(LEFT);
          fill(this.textColor);
          text(this.text, this.pos.x, this.pos.y);
      break;
      case 1:
          pushMatrix();
          textFont(this.font,this.fontSize);
          textAlign(CENTER);
          fill(this.textColor);


          scale(this.textScale,this.textScale);

          translate(150,300);
          rotate(radians(this.radian-60));
          text(this.text,this.pos.x,this.pos.y);
          //text("evenmore Pixels!!!!",this.pos.x,this.pos.y);

          fill(255,190,255);
          popMatrix();
      break;
  }
}


String getRandomText(){
    int random = int(random(0,this.splashArray.length));
    return splashArray[random];
}

void loadSplash(){
    String data[] = loadStrings("data/splash.txt");
    this.splashArray = new String[data.length];
    for (int i = 0; i < data.length; i++){
        this.splashArray[i] = data[i];
    }
}
}
