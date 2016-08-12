class Game
{
  ArrayList <Obstacle> obstacle;

  float count = 1; //1 Monster am anfang

  Player player;
  Player2 player2;
  Player3 player3;
  Player4 player4;
  
  Paralax parallax1, parallax2;


  float speedY;
  float x, y;
  
  float ground1 = 200;
  float ground2 = 300;
  float ground3= 400;
  float ground4= 500;


  Game() 
  {

    parallax2 = new Paralax ("hintergrund2.png", 5);
    parallax1 = new Paralax ("hintergrund1.png", 2);
    // 2.Level Hintergrundbild



    obstacle = new ArrayList <Obstacle> ();
    for (int i = 0; i< count; i++)
    {
      Obstacle o = new Obstacle (width, ground1+50);
      obstacle.add(o);
    }
    //Player wird initialisiert
    player = new Player (this.x, this.ground1);
    player2 = new Player2 (this.x, this.ground2);
    player3 = new Player3 (this.x, this.ground3);
    player4 = new Player4 (this.x, this.ground4);
    
  }

  void drawGame()
  {

    parallax1.display();
    parallax2.display(); 

    obstacle();
   
    player.update();
    player2.update();  
    player3.update();
    player4.update(); 

  }

  void obstacle()
  {
    boolean unten = false;
    for (int i=0; i < obstacle.size();i++)
    {
      Obstacle o = obstacle.get(i);
      o.update();
      if (o.gety() < width )
      {
        if (o.gety() + o.getSize()-80 > player.gety() && o.gety() < player.gety() + player.getsizem()
          && player .getx() < o.getx() && (o.getx() < player.getx()+player. getsizem()) )
        {
          obstacle.remove(i);
          unten= true;   
        }
        
        else 
        {
          if (o.getx() < 0) // 
          {
            println(o.getx());
            obstacle.remove(i);
            unten = true;             
          }
        }
      }
    }
    if (unten == true)
    {
      //Obstacle o = new Obstacle (random(0, width), 0);
      Obstacle o = new Obstacle (width, ground1+50);
      Obstacle i = new Obstacle (width, 200);
      obstacle.add(o);
      obstacle.add(i);
      unten = false;
    }
  }


  //tasten
  void setKey (int keyCodeNum, boolean mode)
  {
    this.player.setKey(keyCodeNum, mode);
    this.player2.setKey(keyCodeNum, mode);
    this.player3.setKey(keyCodeNum, mode);
    this.player4.setKey(keyCodeNum, mode);
    
  }

}

