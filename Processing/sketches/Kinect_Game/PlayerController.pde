class PlayerController 
{
  MouseJoint mouseJoint;

  PlayerController() 
  {
    mouseJoint = null;
  }

  void update(float x, float y) 
  {
    if (mouseJoint != null) 
    {
      Vec2 mouseWorld = box2d.coordPixelsToWorld(x,y);
      mouseJoint.setTarget(mouseWorld);
    }
  }

  void bind(float x, float y, Player Player) 
  {
    MouseJointDef md = new MouseJointDef();
    
    md.bodyA = box2d.getGroundBody();
    md.bodyB = player.body;
    Vec2 mp = box2d.coordPixelsToWorld(x,y);
    md.target.set(mp);
    md.maxForce = 100000.0 * player.body.m_mass;
    md.frequencyHz = 60.0;
    md.dampingRatio = 0.1;

    mouseJoint = (MouseJoint) box2d.world.createJoint(md);
  }

  void destroy() {
    if (mouseJoint != null) {
      box2d.world.destroyJoint(mouseJoint);
      mouseJoint = null;
    }
  }

}


