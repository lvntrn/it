class Player
{
  float x;
  float y;
  boolean moving = true;
  boolean rightPressed = true;
  boolean leftPressed = true;
  float pWidth = 53;
  float speed = 5;
  PImage player;
  
  Player(float x,float y)
  {
    this.x = x;
    this.y = y;
  }
  
  void Player(boolean collision)
  {
    if(collision)
    {
      this.y += 4;
    }
    
    player = loadImage("player.png");
    image(player,this.x,this.y);
    
    if (this.rightPressed)
    { 
      //println("Rechts");
      this.x += speed;
    }
    
    if (this.leftPressed)
    {
      this.x -= speed;
    }
  
    if(this.x < 140)
    {
      this.x= this.x+10;
    }
  
    if(this.x > 600)
    {
      this.x = this.x-10;
    }
  }
  
  float getX()
  {
    return this.x;
  }

  float getY()
  {
    return this.y;
  }
  
  float getWidth()
  {
    return this.pWidth;
  }
  
  void rightPressed(boolean moving)
  {
    this.rightPressed = moving;
    
  }
  
  void leftPressed(boolean moving)
  {
    this.leftPressed = moving;
  }
 
}
