int x;

void setup(){
    size(512,512);
    colorMode(HSB, 360,100,100,100);
    noStroke();
}

void draw(){

    background(0,0,0);

    if(x<360){x= x+2;}
    if(x>=360){x=0;}

    pushMatrix();
    translate(mouseX,mouseY);// bring zero point to the center
    for (int i = 0; i <= x; i++){
        fill(x/4,100,100,100);
        ellipse (sin(radians(-1*i))*-20,cos(radians(-1*i))*-20,5,5);//<ellipse
        //ellipse (sin(radians(x+(i)))*20,cos(radians(x+i))*20,2,2);
    }
    popMatrix();
}

int prozent(){

  return 0;
};
