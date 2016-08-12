Game game;


boolean gameMode;


void setup ()
{  
  size(1200, 700);
  game = new Game();
  frameRate(22);
}


void draw ()
{
  game.drawGame();
}


void keyPressed()
{
  if (keyCode == UP || keyCode == LEFT || keyCode == RIGHT )
  {
    game.setKey(keyCode, true);//greift auf game zurück und übergibt keycode und false (mode)
  }
}

void keyReleased ()
{
  if (keyCode == UP || keyCode == LEFT || keyCode == RIGHT )
  {
    game.setKey(keyCode, false);//greift auf game zurück und übergibt keycode und false (mode)
  }
}

