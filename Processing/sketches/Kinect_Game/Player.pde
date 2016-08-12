class Player 
{
  Body body;
  float r;
  float x;
  float y;

  Player(float x_, float y_,float r_) 
  {
    x = x_;
    y = y_;
    r=r_;
    makeBody(new Vec2(x, y),r);
    body.setUserData(this);
  }

  void killBody() 
  {
    box2d.destroyBody(body);
  }

  boolean contains(float x, float y) 
  {
    Vec2 worldPoint = box2d.coordPixelsToWorld(x, y);
    Fixture f = body.getFixtureList();
    boolean inside = f.testPoint(worldPoint);
    return inside;
  }

  void display() 
  {
    Vec2 pos = box2d.getBodyPixelCoord(body);
    float a = body.getAngle();
    pushMatrix();
    translate(pos.x, pos.y);
    noStroke();
    fill(102,154,31,70);
    rotate(a);
    ellipse(0, 0, r*2, r*2);
    popMatrix();
  }

  void makeBody(Vec2 center,float body_r) 
  {
    BodyDef bd = new BodyDef();
    bd.type = BodyType.DYNAMIC;
    bd.position.set(box2d.coordPixelsToWorld(center));
    body = box2d.createBody(bd);

    CircleShape cs = new CircleShape();
    cs.m_radius = box2d.scalarPixelsToWorld(body_r);

    FixtureDef fd = new FixtureDef();
    fd.shape = cs;
    fd.density = 1;
    fd.friction = 0.3;
    fd.restitution = 0.5;

    body.createFixture(fd);
    
    body.setLinearVelocity(new Vec2(random(-5, 5), random(2, 5)));
    body.setAngularVelocity(random(-5, 5));
  }
}


