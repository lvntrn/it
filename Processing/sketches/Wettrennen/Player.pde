class Player
{
  float sizem = 130;//größe des Players
  float x; //x position des Players
  float y; //y position des Player
  float speed=10; //geschwindigkeit
  boolean j; // ist gerade in der luf
  int jumpHeight = 150; //Jump Höhe
  float ziel;

  //Kräfte

  boolean right, up;

  boolean ground1;
  boolean jump;

  PImage[] images; 
  int imageCount, frame, frameRun, count;
  String imagePrefix;

  Player (float _x, float _y)
  {
    this.x= _x;
    this.y= _y;
    this.ziel=1000;
    this.frameRun = 1;
   this.imagePrefix = "mädchen_rennen_";
    this.imageCount = 14; //Anzahl der Bilder
    this.images = new PImage[this.imageCount];

    //Bilder als Schleife erzeugen animationseffekt
    for (int i = 0; i < this.imageCount; i++) 
    {
      String filename = imagePrefix + nf(i, 2) + ".png"; //
      this.images[i] = loadImage(filename); //Bilder in die Liste laden
    }
  }

  void update ()
  {
    this.autorun();
    this.animation();
    this.checkKey();
    

    if (jump == true || ground1 == false)
    {
      this.jump1();
    }
    
    if (this.x > this.ziel)
    {
      this.winPlayer();
    }
  }
  
  void winPlayer(){
   textSize(32);
   text("player 1 hat gewonnen",10,30);
   fill(0,0,0);
  }
  
  //rennt automatisch
  void autorun()
  {
    if (!right) 
    { //wenn nicht nach rechts gedrückt wird soll speed 1.5 betragen
      this.speed = 1.5;
      runRight();
    }
    else 
    {
      this.speed = 15; //sonst soll er mit 15 speed laufen
    }
    this.x += speed;
  }

  void checkKey()
  {
    if (keyPressed)
    {
      if (this.right)
      {
        this.x+=this.speed;
        this.runRight();
      }
      //wenn nach obenpfeil gedrückt
      if (this.up )
      {

        this.jump1(); //wenn ich nach oben klicken soll jump ausgeführt werden
      }
    }
  }

  void jump1() 
  {
    if (this.ground1 == true)
    {
      this.jump = true;
      this.ground1 = false;
    }
    if (this.jump == true && y <= this.jumpHeight) //wenn ich springen kann und die y höhe kleiner oder gleich der Sprunghöhe ist 
    {
      this.jump = false; //kann ich nicht mehr springen --> kann nicht mehr in der Luft springen nur am Boden
    }
    if ( jump == true && y > this.jumpHeight) // wenn ich springen kann und die y höhe größer als die sprunghöhe ist
    {
      y-= 7; // bis zur sprunghöhe hüpfen nach oben
      println(y);
    }
    if ( (jump == false)  )  
    {
      y+= 10;//player geht wieder auf den Boden zurück mit 7 geschwindigkeit
    }
    if (ground1 == false && jump == false && y >= 400)
    {
      ground1 = true;
    }

  }
 

  void animation()
  {
    if (ground1 == true && this.right) //wenn pferd auf dem Boden und rechtspfeil
    {
      image(images[frame], this.x, this.y);
    }
    if (this.ground1 == true)//wenn Pferd auf boden
    {
      image(images[frame], this.x, this.y);
    }
    else // wenn in der luft
    {
      image(images[3], this.x, this.y);
    }
  }

  float getsizem()
  {
    return this.sizem;
  }
  float gety()
  {
    return this.y;
  }
  float getx()
  {
    return this.x;
  }

  void setKey (int keyCodeNum, boolean mode)
  {
    if (keyCodeNum == UP )
    {
      this.up = mode;
    }
    if (keyCodeNum == RIGHT)
    {
      this.right = mode;
    }
  }

  void runRight() 
  {
 
    this.frameRun ++;

    if (this.frameRun > 4) //wenn das Bild über 4 beträgt dann soll das Bild wieder bei 1 anfangen-> schleife
    {
      this.frameRun = 1;
    }
    this.frame = this.frameRun;
  }
  //player wird außerhalb des sichtbaren bereichs gezeichnet

  void reset()
  {
    this.x= -500;
    this.y= -500;


  }
}

