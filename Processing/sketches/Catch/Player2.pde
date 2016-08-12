class Player2 //Hände
{
  float a;
  float d;
 
  boolean moving = true;
  boolean moveLeft = false;
  boolean moveRight = false;
  float playerimgwidth = 200;
  float playerimgheight = 200;
  PImage img;


  void update()
  {
    this.img = loadImage("auffangen.png");
    image (this.img, this.a, this.d, playerimgwidth, playerimgheight);

    // Kollionsabfrage - Randbegrenzung

    if (this.a < width-180)
    {
      if (this.moveRight)
      {
        this.a = this.a+5;
      }
    }

    if (this.a  > -20)
    {
      if (this.moveLeft)
      {
        this.a  =this.a-5;
      }
    }
  }
  

  //Tastenbefehle zum Bewegen des Players

  float getA()
  {
    return this.a;
  }

  float getD()
  {
    return this.d;
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

