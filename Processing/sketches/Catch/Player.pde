class Player //Hände
{
  float x;
  float y;
 
  boolean moving = true;
  boolean moveLeft = false;
  boolean moveRight = false;
  float playerimgwidth = 200;
  float playerimgheight = 200;
  PImage img;


  Player(float _x, float _y )
  {
    this.x = _x;
    this.y = _y;

  }   

  void update()
  {
    this.img = loadImage("auffangen.png");
    image (this.img, this.x, this.y, playerimgwidth, playerimgheight);

    // Kollionsabfrage - Randbegrenzung

    if (this.x < width-180)
    {
      if (this.moveRight)
      {
        this.x = this.x+5;
      }
    }

    if (this.x  > -20)
    {
      if (this.moveLeft)
      {
        this.x  =this.x-5;
      }
    }
  }
  

  //Tastenbefehle zum Bewegen des Players

  float getX()
  {
    return this.x;
  }

  float getY()
  {
    return this.y;
  }

float getPlayerimgwidth()
{
     return this.playerimgwidth;
}

  void moveLeft(boolean moving)
  {
    this.moveLeft = moving;
  }


  void moveRight(boolean moving)
  {
    this.moveRight = moving;
  }
}

