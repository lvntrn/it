class Game
{
  Player p;
  PImage bg;
  PImage finishLine;
  PImage end;
  obstacle[] o;
  boolean collision;
  float fX = 135;
  float fY = -1000;
  int finish = 0;
  
  Game()
  {
    p = new Player(190,300);
    o = new obstacle[2];
    
    for(int i=0; i<2; i++)
    {
      o[i] = new obstacle();
    }
  }

  void Start()
  {
    
    bg = loadImage("bg.png");
    image(bg,800,600);
    background(bg);
    p.Player(collision);
    float pX = p.getX();
    float pY = p.getY();
    float pWidth = p.getWidth();
    
    for(int i=0; i<2; i++)
    {
      o[i].obstacle(pX,pY,pWidth);
    }
   
    for(int i=0; i<2; i++)
    {
      collision = o[i].collision();
    }
    
    finish += 1;
    
    finishLine = loadImage("finishline.png");
    image(finishLine,fX,fY);
    
    if(finish > 10)
    {
      fY +=1;
    }
    
    if(pY < fY - 50)
    {
      end = loadImage("end.png");
      background(end);
    }   
  } 
 
  
  void rightPressed(boolean moving)
  {
    this.p.rightPressed = moving;
  }


  void leftPressed(boolean moving)
  {
    this.p.leftPressed(moving);
  }
  
}
