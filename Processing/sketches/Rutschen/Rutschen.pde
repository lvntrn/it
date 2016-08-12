Game game;

void setup () 
{
  size (800,600);
  game = new Game();
}

void draw()
{
  game.Start();
}

void keyPressed() 
{
  if (keyCode == RIGHT) 
  {
    game.rightPressed(true);
  }
  
  if (keyCode == LEFT)  
  {
    game.leftPressed(true);
  }
}

void keyReleased() 
{
  if (keyCode == RIGHT) 
  { 
    game.rightPressed(false);
  }
  
  if (keyCode == LEFT)  
  {
    game.leftPressed(false);
  }
}
