Game g;


void setup() {
  g = new Game();
  size(800, 600);
}

void draw()
{
    
  if (g.lives() > 0) 
  {
    background(#6CDCEF);
    g.start();
    
    // framecount Funktion aufrufen g.newsammelstück()
    if (frameCount%60 == 0)
    {
      g.newCheck();
      g.newObstacle();
    }
  }
  else {
    g.gameOver();
  }
}

void keyPressed()
{
  if (keyCode == LEFT)
  {
    g.moveLeft(true);
  }
  if (keyCode == RIGHT)
  {
    g.moveRight(true);
  }
}

void keyReleased()
{
  if (keyCode == LEFT)
  {
    g.moveLeft(false);
  }
  if (keyCode == RIGHT)
  {
    g.moveRight(false);
  }
  if (keyCode == ENTER && (g.lives() <= 0) )
  {
    g.restart(5);
  }
}

