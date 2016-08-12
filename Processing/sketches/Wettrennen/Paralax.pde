class Paralax 
{
  PImage hintergrund;
  int x1, x2, y, speed;
  String loadname;

  Paralax(String tempLoadname, int tempSpeed) 
  {
    this.loadname = tempLoadname;
    this.speed = tempSpeed;
    this.x1 = 0;
    this.x2 = x1 - 5000; //Hintergrundbild läuft von 0 bis 5000 Pixel
    this.y = 0;
    this.hintergrund = loadImage(loadname);
  }
  void display() 
  {
    image(hintergrund, x1, y);
    image(hintergrund, x2, y);
    image(hintergrund, x1, y);
    
    this.x1 = x1 - speed*2;
    this.x2 = x2 - speed*2;
    if (keyPressed) 
    {
      if (key == CODED) 
      {
        if (keyCode == RIGHT) //Bewegung des Hintergrunds nach rechts bei gedrückter rechter Pfeiltaste
        {
          this.x1 = x1 - speed*2;
          this.x2 = x2 - speed*2;
        }
      }
    }
    else 
    {
      this.x1 = x1;
      this.x2 = x2;
    }

    if (this.x2 == 0)
    {
      this.x1 =- 5000;
    }

    if (this.x1 == 0)
    {
      this.x2 =- 5000;
    }

    if (this.x1 == -3720)
    {
      this.x2 = 1280;
    }

    if (this.x2 == -3720)
    {
      this.x1 = 1280;
    }
  }
}

