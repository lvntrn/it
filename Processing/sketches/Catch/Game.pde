class Game
{
  Player p;

  ArrayList<Check> check;
  ArrayList<Obstacle> obstacle;
  Check c;
  Obstacle o;
  int score;
  int highscore;
  int lives;
  PrintWriter output;
  PImage herz;

  Game()
  {
    check = new ArrayList<Check>();
    obstacle = new ArrayList<Obstacle> ();

    try
    {
      String[] firstline = loadStrings("output.txt");
      highscore = int(firstline[0]);
    }
    catch (Exception e) {
    }


    this.lives = 5;
    //checkobjekte werden in der ArrayList hinzugefügt
    for (int i = 0; i < 1; i++)
    {
      newCheck();
    }
    p = new Player(60, 500);
  }

  void start()
  {
    ///////ZEICHNEN VON ANZEIGEN
    //Lebensanzeige
    herz = loadImage("herz.png");
    println(lives);  
    for (int i = this.lives; i > 0; i--)
    {
      image (herz, 790-20*i, 15, 15, 15);
    }

    //Score anzeigen
    textSize(25);
    fill(0);
    textAlign(RIGHT);
    text(this.score, 790, 60);

    //Highscore anzeigen
    if (this.highscore > 0)
    {
      textAlign(LEFT);
      text("Best:"+ this.highscore, 10, 35);
    }



    //Score als Highscore übernehmen
    if (score >= highscore)
    {
      this.highscore = this.score;
      writeHighscore();
    }

    //Übergabe von X-Position Player in diese Klasse
    p.update();  
    float playerX = p.getX();
    float playerY = p.getY();
    float playerWidth = p.getPlayerimgwidth();

    //Checkobjekte werden ausgegeben
    for (int i = 0; i < check.size(); i++)
    {
      check.get(i).update(playerX, playerY, playerWidth);
      if (check.get(i).catched() == true)
      {
        check.remove(i);
        this.score  = this.score+1*100;
      }
      else if (check.get(i).missed() == true)
      {
        check.remove(i);
        this.lives--;
      }

      // println(check.get(i).moving());
    }
    
    for (int i = 0; i < obstacle.size(); i++)
    {
      obstacle.get(i).update(playerX, playerY, playerWidth);
      if (obstacle.get(i).catched() == true)
      {
        obstacle.remove(i);
        this.score  = this.score-1*100;
        
      }
      else if (obstacle.get(i).missed() == true)
      {
        obstacle.remove(i);
      }

      // println(check.get(i).moving());
    }
  }


  void writeHighscore()
  {
    this.output = createWriter("data/output.txt");
    this.output.println(this.score);
    this.output.flush();
    this.output.close();
  }

  //Durchreichen der Tastenbefehle an die Player-Klasse
  void moveLeft(boolean moving)
  {
    this.p.moveLeft(moving);
  }


  void moveRight(boolean moving)
  {
    this.p.moveRight(moving);
  }

  int lives() {
    return this.lives;
  }

  void gameOver() 
  {
    //GAME OVER
    background(0);
    textSize(40);
    fill(255, 0, 0);
    textAlign(CENTER);
    text("GAME OVER", width/2, height/2);
    fill(255);
    textSize(20);
    text("You reached "+ this.score + " points", width/2, height/2+30);
    fill(255, 0, 0);
    textSize(15);
    text("press ENTER to restart", width/2, height/2+225);
  }

  void restart(int _newlives) {
    this.lives = _newlives;
    this.score = 0;
  }

  // Funktion zum neuen generieren von Checkobjekten... siehe: "Catch"
  void newCheck()
  {
    c = new Check();
    check.add(c);
  }
  
  void newObstacle()
  {
    o = new Obstacle();
    obstacle.add(o);
  }
}

