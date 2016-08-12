class FrameText{

  
public PVector pos, offset;
private float timeDelay, lastTime, textScale, textScaleSpeed, maxTextScale, minTextScale, radian;
public boolean loop;
public PFont font;

FrameText(PVector _pos,PVector _offset){
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

}

void update(){

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

  
}

void render(){
  
  
  pushMatrix(); 
  textFont(font);
  textAlign(CENTER);
  fill(255,190,255);
  
 
  scale(this.textScale,this.textScale);
 
  translate(150,300);
  rotate(radians(this.radian-60));
 
  text("evenmore Pixels!!!!",this.pos.x,this.pos.y);
 
  fill(255,190,255);
  popMatrix();
  

}

}
