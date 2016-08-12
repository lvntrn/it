class Obstacle
{
  float x;
  float y;

  float speedY;
  float size;
  PImage monster;


  Obstacle( float _x, float _y)
  {
    this.x=_x;
    this.y=_y;

    this.speedY = 2;//Die geschwindigkeit der Gegner
    this.size = 100;// die größe 
    
    if (int(random(0, 5000))%3 == 0)
    {
      this.monster=loadImage("gegner-02.png");
    }
    else  
    {
      this.monster=loadImage("gegner1.png");
    }
  }

  void update ()
  {
    this.x = this.x - this.speedY*2;
    image (this.monster, this.x, this.y, this.size, this.size); //bild einpflegen

    if (keyPressed) 
    {
      if (key == CODED) 
      {
        if (keyCode == RIGHT) 
        {
           this.x = this.x - this.speedY*4; //wenn ich nach reechts klicke sollen die monster schneller laufen
        }
      }
    }
  }

// Werte werden zurückgegeben für die kolisionsabfrage
  float gety ()
  {
    return this.y;
  }
  float getSize()
  {
    return this.size;
  }
  float getx ()
  {
    return this.x;
  }
}

