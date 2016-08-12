class Kinect_Pointer extends GameActor{

    private int player, loop, loopSpeed;
    private ArrayList<Particle> particleList;
    public boolean hitTest, selected, selectedOnce;
    private PVector thickness, leapMotion;

    Kinect_Pointer(int _player){
    // GameActor Variables
    	this.pos = new PVector(mouseX, mouseY);
        this.diameter = new PVector (-32, -32);
    	// New Variables
    	this.particleList = new ArrayList<Particle>();
    	this.player = _player;
    	this.loop = 0;
        this.loopSpeed = 2;
        this.type = 1;
    	this.hitTest = false;
    	this.selected = false;
    	this.selectedOnce = false;
        this.thickness = new PVector(10,10);
        this.leapMotion = new PVector(0.5,0.5);
    }

	void update(){

		// Set Potioton of Player Cursor (no Kinect jet)
		this.pos.set(lerp(this.pos.x,mouseX,this.leapMotion.x),lerp(this.pos.y,mouseY,this.leapMotion.y));

		if (this.selected == false && this.hitTest == true){
			this.loop+=this.loopSpeed;
			if(this.loop>=360){
				this.loop=360; this.selected = true;
			}
			else{
			this.selected = false;
			}
		}
		if (this.hitTest == false){
			this.loop = 0;
			this.selected = false;
		}

		//add Particles
		if (game.getGameRunning() == true){
			Particle particle = new Particle(new PVector(this.pos.x,this.pos.y) ,new PVector(35,35), new PVector(0.2,0.2),new PVector(0.5,0.5), true, true, 1, 0.3, color(0,255,255,100),0.8,0,0.5);
			this.particleList.add(particle);
		}

		// Update Particles
		for (int i = 0; i < particleList.size() ; i++){
			this.particleList.get(i).update();
			if (this.particleList.get(i).burnOut()){
				this.particleList.remove(i);
			}
		}
	}


	void render(){

		// Render Particles
		for (int i = 0; i < particleList.size() ; i++){
			this.particleList.get(i).render();
		}
		// If over Button
	    if (this.hitTest == true){
			renderPointer();
		}
        // Text for Each Player
		switch(this.player){
            case 0:
            renderText(this.pos, "P1", color(120,255,255),20, gamefont);
            break;
            case 1:
            break;
            case 2:
            break;
            case 3:
            break;
        }
	}


	void renderText(PVector _pos, String _text, color _color, int _scale, PFont _font){
        textFont(_font, _scale);
        textAlign(CENTER);
        fill(_color);
        text(_text, _pos.x , _pos.y + (_scale/2-1));
    }

	void renderPointer(){
		pushMatrix();
		translate(this.pos.x,this.pos.y); // Draw at Position
			noStroke();
			for (int i = 0; i <= this.loop; i++){
                fill(this.loop/4,255,255,this.loop/6);
                switch(this.type){
                    case 0:
                        ellipse (sin(radians(this.loop +(i+5)))*this.diameter.x,cos(radians(this.loop+i+5))*this.diameter.y,thickness.x,thickness.y);
                    break;
                    case 1:
                        ellipse (sin(radians(-1*i))*this.diameter.x,cos(radians(-1*i))*this.diameter.y,this.thickness.x,this.thickness.y);
                    break;
                }
            }

		popMatrix();
	}

	void setHitTest(boolean _hitTest){
		this.hitTest = _hitTest;
	}
	boolean getHitTest(){
		return this.hitTest;
	}
	boolean getSelected(){
		return this.selected;
	}
	void setSelected(boolean _selected){
		this.selected = _selected;
	}
	boolean getSelectedOnce(){
		return this.selectedOnce;
	}
	void setSelectedOnce(boolean _selectedOnce){
		this.selectedOnce = _selectedOnce;
	}
    int getPlayer(){
        return this.player;
    }
}
