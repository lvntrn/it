class Ball 
{
  
  Body body;
  float r;
  float x;
  float y;
  PImage img;
  String imageName;

  Ball(float x_, float y_, float r_,String imageName_ ) 
  {
    r = r_;
    x = x_;
    y = y_;
    imageName=imageName_;
    img = loadImage(imageName);
    makeBody(x, y, r);
    body.setUserData(this);
  }

  void killBody() {
    box2d.destroyBody(body);
    life--;
    alife=false;
  }

  void score() 
  {
    score+=100;
    bounce.trigger();
  }

  boolean done() 
  {
    Vec2 pos = box2d.getBodyPixelCoord(body);
    if (pos.y > height+r*2) {
      killBody();
      return true;
    }
    return false;
  }


  // 
  void display() {
    Vec2 pos = box2d.getBodyPixelCoord(body);
    float a = body.getAngle();
    pushMatrix();
    translate(pos.x, pos.y);
    rotate(a);
    image(img, 0-r, 0-r,r*2,r*2);
    popMatrix();
  }

  void makeBody(float x, float y, float r) 
  {
    BodyDef bd = new BodyDef();
    bd.position = box2d.coordPixelsToWorld(x, y);
    bd.type = BodyType.DYNAMIC;
    body = box2d.createBody(bd);

    CircleShape cs = new CircleShape();
    cs.m_radius = box2d.scalarPixelsToWorld(r);

    FixtureDef fd = new FixtureDef();
    fd.shape = cs;
    fd.density = 1;
    fd.friction = 0.01;
    fd.restitution = 0.1;

    body.createFixture(fd);
    
    body.setAngularVelocity(random(-10, 10));
  }
}

