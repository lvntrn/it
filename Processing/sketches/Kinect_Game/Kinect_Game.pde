import shiffman.box2d.*;
import org.jbox2d.common.*;
import org.jbox2d.dynamics.joints.*;
import org.jbox2d.collision.shapes.*;
import org.jbox2d.collision.shapes.Shape;
import org.jbox2d.common.*;
import org.jbox2d.dynamics.*;
import org.jbox2d.dynamics.contacts.*;
import SimpleOpenNI.*;
import ddf.minim.*;

Minim minim;

SimpleOpenNI  context;
Box2DProcessing box2d;
Player player;
PlayerController playerController;
Boundary wall_l;
Boundary wall_r;
Boundary wall_u;
ArrayList<Ball> ball;

color[]userClr = new color[]
{ color(255,0,0),
  color(0,255,0),
  color(0,0,255),
  color(255,255,0),
  color(255,0,255),
  color(0,255,255)
};
PVector com = new PVector(); 

PVector com2d = new PVector();
int life;
int score;
boolean alife;
boolean gameisready;


AudioSample bounce;


void setup() 
{
  frameRate(30);
  gameisready=false;
  alife=true;
  size(1280,960);
  life=3;
  score=0;
  context = new SimpleOpenNI(this);
  minim = new Minim(this);
  box2d = new Box2DProcessing(this);
  bounce = minim.loadSample("Plub.mp3");



  context.setMirror(true);
  if(context.isInit() == false)
  {
     println("Can't init SimpleOpenNI, maybe the camera is not connected!"); 
     exit();
     return;  
  }
  context.enableDepth();
  context.enableRGB();
  context.enableUser();
  
  box2d.createWorld();
  box2d.listenForCollisions();

  player = new Player(width/2,height/2,60);
  playerController = new PlayerController();
  playerController.bind(width/2,height/2,player);

  ball = new ArrayList<Ball>();
  
  ball.add(new Ball(random(width/2-width/3,width/2+width/3), 60, 60,"ball_beach.png"));
  
  wall_l = new Boundary(0, height/2, 2, height*2);
  wall_r = new Boundary(width, height/2, 2, height*2);
  wall_u = new Boundary(width/2, -50, width*2,2);
  box2d.setGravity(0,-100);


  
}

void draw() 
{
  context.update();
  image(context.rgbImage(),0,0,1280,960);
  
  int[] userList = context.getUsers();
  for(int i=0;i<userList.length;i++)
  {
    if(context.isTrackingSkeleton(userList[i]))
    {
      
      drawSkeleton(userList[i]);
      gameisready=true;
      
    }     
    else
    {
      gameisready=false;
    } 
  }
  
  if (gameisready=true)
  {
    if (alife==false&& life>0) 
    {
    ball.add(new Ball(random(width), 60, 60,"ball_beach.png"));
    alife=true;
    }

    box2d.step();
  
    player.body.setAngularVelocity(0);
  
    for (int i = ball.size()-1; i >= 0; i--) 
    {
      Ball p = ball.get(i);
      p.display();
      if (p.done()) 
      {
        ball.remove(i);
      }
    }
  
    player.display();
    wall_l.display();
    wall_r.display();
    fill(255);
    text("live" +" "+life, 10, 20);
    text("score" +" "+score, 10, 40);
    if (life==0||life<0)
    {
      background(255);
      fill(0);
      textSize(20);
      text("gameover", 10, 40);
      text("Press"+"mouse"+" "+"to"+" "+"Restart", 10, 80);
    }  
  }
  else
  {
    fill(255);
    textSize(20);
    text("Game is not ready", 10, 40);
  }

  
}


void beginContact(Contact cp) {
  Fixture f1 = cp.getFixtureA();
  Fixture f2 = cp.getFixtureB();
  Body b1 = f1.getBody();
  Body b2 = f2.getBody();
  Object o1 = b1.getUserData();
  Object o2 = b2.getUserData();
  if (o1.getClass() == Player.class) 
  {
    Ball p = (Ball) o2;
    p.score();
  } 
  else if (o2.getClass() == Player.class) 
  {
    Ball p = (Ball) o1;
    p.score();
  }
}

void endContact(Contact cp) 
{
}

void drawSkeleton(int userId)
{
  PVector jointPos = new PVector();
  context.getJointPositionSkeleton(userId,SimpleOpenNI.SKEL_HEAD,jointPos);
  PVector jointPos_Proj = new PVector(); 
  context.convertRealWorldToProjective(jointPos,jointPos_Proj);
  playerController.update(jointPos_Proj.x*2,jointPos_Proj.y*2);
}

void onNewUser(SimpleOpenNI curContext, int userId)
{
  println("onNewUser - userId: " + userId);
  println("\tstart tracking skeleton");
  curContext.startTrackingSkeleton(userId);
}

void onLostUser(SimpleOpenNI curContext, int userId)
{
  println("onLostUser - userId: " + userId);
}

void onVisibleUser(SimpleOpenNI curContext, int userId)
{
}

void mouseClicked() 
{
  if (life==0||life<0)
  {
    setup();
  }
}

void stop()
{
  bounce.close();
  minim.stop();
  super.stop(); // tells program to continue doing its normal ending activity
}














