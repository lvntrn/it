class Check //Metallstücke zum Sammeln 
{
  float x;
  float y;
  float winkel = 0;
  float imgwidth = 50;
  float imgheight = 50;
  float randomSpeed;
  boolean missed;
  boolean catched;
  PImage img;

  Check()
  {
    this.x = random(0, 600); 
    this.y = 0;
    this.missed = false;
    this.catched = false;
    this.randomSpeed = random(1, 5);
  }   


  void update (float _playerX, float _playerY, float _playerimgwidth)
  {
   
    this.img = loadImage("Fass.png");
    image (this.img, this.x, this.y, imgwidth, imgheight);
    this.y=this.y+this.randomSpeed;
    
   

    //Kollisionsabfrage mit Player-Objekt
    if (((this.y + imgheight) > _playerY) && ((this.x > _playerX && this.x < _playerX+_playerimgwidth)||(this.x+imgwidth > _playerX && this.x+imgwidth < _playerX+_playerimgwidth)))
    {
      this.catched = true;
    }
  }



  boolean catched ()
  {
    return catched;
  }


  boolean missed ()
  {
    if (this.y > height)
    {
      missed = true;
    }
    return missed;
  }
}

