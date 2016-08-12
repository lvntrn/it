class Player3
{
  float sizem = 130;//größe des Players
  float x; //x position des Players
  float y; //y position des Player
  float speed=5; //geschwindigkeit
  boolean j; // ist gerade in der luf
  int jumpHeight = 120; //Jump Höhe
  float ziel;

  //Kräfte

  boolean right, up;
  boolean ground3;
  boolean jump;

  PImage[] images; 
  int imageCount, frame, frameRun, count;
  String imagePrefix;

  Player3 (float _x, float _y)
  {
    this.x= _x;
    this.y= _y;

    this.frameRun = 1;
    this.imagePrefix = "cat_";
    this.imageCount = 14; //Anzahl der Bilder
    this.images = new PImage[this.imageCount];
    this.ziel = 1000;

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
    

    if (jump == true || ground3 == false)
    {
      this.jump1();
    }
      
  //wenn Player aus dem bild ist gewonnen....
     if (this.x > this.ziel)
    {
      this.winPlayer();
    }
  }
  
  void winPlayer(){
   textSize(32);
   text("player 2 hat gewonnen",10,30);
   fill(0,0,0);
  }
 

  //rennt automatisch
  void autorun()
  {
    if (!right) 
    { //wenn nicht nach rechts gedrückt wird soll speed 10 betragen
    //verändert die geschwindigkeit
    
      this.speed = 1;
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
    if (this.ground3 == true)
    {
      this.jump = true;
      this.ground3 = false;
    }
    if (this.jump == true && y <= this.jumpHeight) //wenn ich springen kann und die y höhe kleiner oder gleich der Sprunghöhe ist 
    {
      this.jump = false; //kann ich nicht mehr springen --> kann nicht mehr in der Luft springen nur am Boden
    }
    if ( jump == true && y > this.jumpHeight) // wenn ich springen kann und die y höhe größer als die sprunghöhe ist
    {
      y-= 5; // bis zur sprunghöhe hüpfen nach oben
      println(y);
    }
    if ( (jump == false)  )  
    {
      y+= 7;//player geht wieder auf den Boden zurück
    }
    if (ground3 == false && jump == false && y >= 400)
    {
      ground3 = true;
    }
  }


  void animation()
  {
    if (ground3 == true && this.right) //wenn pferd auf dem Boden und rechtspfeil
    {
      image(images[frame], this.x, this.y);
    }
    if (this.ground3 == true)//wenn Pferd auf boden
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

    {

    }
    this.frameRun ++;

    if (this.frameRun > 4) //wenn das Bild über 4 beträgt dann soll das Bild wieder bei 1 anfangen-> schleife
    {
      this.frameRun = 1;
    }
    this.frame = this.frameRun;
  }
  //player wird außerhalb des sichtbaren bereichs gezeichnet

}

