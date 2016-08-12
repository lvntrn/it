public class GameActor
{
    // Provides Often Use Variables and Methodes to other Classes.
    PVector pos, speed, accel, deaccel, maxSpeed, diameter, repPos, sign, savePos, offset;
    float gravity;
    int type, status;
    boolean death, movement, collision, enableGravity;

    GameActor(){
        this.pos = new PVector(0, 0);
        this.speed = new PVector (0, 0);
        this.accel = new PVector (0.1, 0.1);
        this.deaccel = new PVector (0.25, 0.25);
        this.maxSpeed = new PVector (5, 5);
        this.diameter = new PVector (32, 32);
        this.repPos = new PVector (0, 0);
        this.sign = new PVector (0, 0);
        this.savePos = new PVector (0, 0);
        this.offset = new PVector (0, 0);
        this.movement = true;
        this.collision = true;
        this.gravity = 2;
        this.death = false;
        this.type = 0;
        this.status = 0;
        this.enableGravity = true;
    }

    //this(pos, speed, accel, deaccel, diameter, etc.);

    PVector getPos(){
        return this.pos;
    }

    float getX(){
        return this.pos.x;
    }

    float getY(){
        return this.pos.y;
    }

    void setPos(float _x, float _y){
        this.pos.set(_x, _y) ;
    }

    void addSpeed(float _speedX, float _speedY){
        this.speed.add( _speedX, _speedY, 0);
    }

    void setSpeed(float _speedX, float _speedY){
        this.speed.set(_speedX, _speedY, 0);
    }

    void setSpeed(PVector _speed){
        this.speed.set(_speed);
    }

    void addSpeed(PVector _speed){
        this.speed.add(_speed);
    }

    float getSpeedX(){
        return this.speed.x;
    }

    float getSpeedY(){
        return this.speed.y;
    }

    PVector getSpeed(){
        return this.speed;
    }
    PVector getDiameter(){
        return this.diameter;
    }
    float getDiameterX(){
        return diameter.x;
    }

    float getDiameterY(){
        return diameter.y;
    }

    float getGravity(){
        return this.gravity;
    }

    void setOffset(int _offsetX, int _offsetY){
        this.offset.set(_offsetX, _offsetY);
    }

    void setOffset(PVector _offset){
        this.offset = _offset;
    }

    void setDeath(boolean _death){
        this.death = _death;
    }
    boolean getDeath(){
        return this.death;
    }

    void setMovement(boolean _movement){
        this.movement=_movement;
    }

    void setCollision(boolean _collision){
        this.collision = _collision;
    }

    boolean getMovement(boolean _movement){
        return this.movement;
    }

    boolean getCollision(){
        return this.collision;
    }

    boolean hitTest(PVector _pos,PVector _diameter){
        if (_pos.x + _diameter.x > this.pos.x  && _pos.x < this.pos.x+this.diameter.x &&
            _pos.y + _diameter.y > this.pos.y  && _pos.y < this.pos.y+this.diameter.x){
            return true;
        }
        else{
            return false;
        }
    }
    void runParticlePhysics(){
        this.pos.add(this.speed.x/this.deaccel.x, this.speed.y/this.deaccel.y, 0);

        if  (this.enableGravity){
            this.pos.add( 0, this.gravity, 0 );
        }
    }
    void runPhysics(){

        this.pos.add(this.speed.x,this.speed.y,0);

        // Normal Physics
        if (movement == false){
            if (this.speed.x > 0.01 ){
                this.speed.sub(this.deaccel.x,0,0);
            }
            if (this.speed.x < -0.01 ){
                this.speed.add(this.deaccel.x,0,0);
            }
            if( this.speed.x > -0.01 && this.speed.x < 0.01){
                this.speed.x = 0;
            }
            if (this.speed.y > 0.01 ){
                this.speed.sub(0,this.deaccel.y,0);
            }
            if (this.speed.y < -0.01 ){
                this.speed.add(0,this.deaccel.y,0);
            }
            if( this.speed.y > -0.01 && this.speed.y < 0.01){
                this.speed.y = 0;
            }
        }

        // Max Speed

        if (this.speed.x > this.maxSpeed.x){
            this.speed.set(this.maxSpeed.x,0);
        }
        if (this.speed.x < (-1*this.maxSpeed.x)){
            this.speed.set((-1*this.maxSpeed.x),0);
        }

        if (this.speed.y > this.maxSpeed.y){
            this.speed.set(0,this.maxSpeed.y);
        }
        if (this.speed.y < (-1*this.maxSpeed.y)){
            this.speed.set(0,(-1*this.maxSpeed.y));
        }

    }
}
