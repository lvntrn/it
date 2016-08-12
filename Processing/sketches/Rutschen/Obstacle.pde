class obstacle
{
  float oX;
  float oY;
  float speed;
  float oWidth = 143;
  float oHeight = 125;
  boolean collision;
  PImage obstacle;

  obstacle()
  {
    this.oX = random(100,500);
    this.oY = random(-500,-300);
    this.collision = false;
    this.speed = random(3,7);
  }

  void obstacle(float pX, float pY, float pWidth)
  {
    oY += speed;
    obstacle = loadImage("obstacle.png");
    image(obstacle,oX,oY);

    if(oY>600)
    {
      oX = random(100,500);
      oY = random(-500,-300);
    }
    
    if ((this.oY + oHeight - 100 > pY) && (((this.oX > pX) && (this.oX < pX + (pWidth-10)) || ((this.oX + oWidth > pX) && (this.oX + oWidth < pX + (pWidth-10))))))
    {
      this.collision = true;
    } 
    else 
    {
      this.collision = false;
    }
  }
  
  boolean collision()
  {
    return this.collision;
  }
}

