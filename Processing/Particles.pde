class Particle extends GameActor{

    private float timeDelay, radiusRedux, lastTime, hsbLoopAmount, hue, saturation, brightness, alpha;
    private color colorHSB;
    private boolean  hsbLoop;
    private int loop;
    private PVector thickness;

    // normal Konstruktor
    Particle(PVector _pos,PVector _diameter, PVector _speed, PVector _deaccel, boolean _movement, boolean _hsbLoop,int _type,
    float _hsbLoopAmount, color _colorHSB, float _gravity, float _timeDelay, float _radiusRedux){

        this.pos = _pos;
        this.speed = new PVector (0.2, 0.2);
        this.accel = new PVector (0.2, 0.2);
        this.deaccel = _deaccel;
        this.diameter = _diameter;
        this.repPos = new PVector (0, 0);
        this.sign = new PVector (0, 0);
        this.savePos = new PVector (0, 0);
        this.offset = new PVector (0, 0);
        this.movement = _movement;
        this.collision = true;

        this.gravity = _gravity;
        this.hsbLoop =  _hsbLoop;
        this.type = _type;
        this.hsbLoopAmount = _hsbLoopAmount;
        this.colorHSB = _colorHSB;
        this.timeDelay = _timeDelay;
        this.radiusRedux = _radiusRedux;
        this.lastTime = 0;
        this.loop = 0;
        this.thickness = new PVector(5,5);
        //get Colors for better Handling
        this.hue = hue(this.colorHSB);
        this.saturation = saturation(this.colorHSB);
        this.brightness = brightness(this.colorHSB);
        this.alpha = alpha(this.colorHSB);
    }

    // Update Color if any , if moving make it move
    void update(){
        this.deaccel.add(1, 1, 0);

        if (this.movement == true){
           this.runParticlePhysics();
        }

        if (this.hsbLoop == true){
            this.updateLoop(360,this.hsbLoopAmount);
            this.hue = this.loop;
        }
        switch(this.type)
        {
            case 0:
            break;
            case 1:
            this.speed.add(random(-5,5),random(-5,5),0);
            break;
        }

        // Reduce Radius to Remove it Later
        if (millis() - this.lastTime >= timeDelay){
            if (this.diameter.x > 1 ){
                this.diameter.sub(this.radiusRedux, 0, 0);
            }
            if (this.diameter.y > 1){
                this.diameter.sub(0, this.radiusRedux, 0);
            }
            this.lastTime = millis();
            }
        }

    // Control the Color and Rendering
    void render(){
        noStroke();
        fill(this.hue,this.saturation,this.brightness,this.alpha);
        ellipse(this.pos.x, this.pos.y, this.diameter.x, this.diameter.y);
    }

     // If diameter is lower than X Pixel , return true
    boolean burnOut(){
        if (this.diameter.x <= 1 || this.diameter.y <= 1){
            return true;
        }
        else{
            return false;
        }
    }

    void updateLoop(int _limit, float _speed){
        this.loop+= _speed;
        if (this.loop >= _limit)
        {
            this.loop = 0;
        }
    }

}
