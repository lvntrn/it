class Enemy extends GameActor{

  int worldID;

    Enemy(int _worldID, PVector _pos, int _type){
        this.pos = _pos;
        this.worldID = _worldID;
        this.type = _type;
      }

	void update(){
        switch(this.worldID){
            case 0:
                switch(this.type){
                    //Logic for each differend Enemy Type goes here
                    case 0:
                    switch(this.status){
                        case 0:
                            this.speed.set(random(-0.01,0.01),-0.1,0.01);
                            this.deaccel.set(0.5, 0.5);
                            this.diameter.set(random(32,64),random(32,64));
                            this.gravity = 0.5;

                            this.status = 1;
                            case 1:

                            runParticlePhysics();
                            this.speed.add(0,0.01,0);

                            if (this.pos.y + this.diameter.y > game.gameWorld.getBorderY() +200 || this.pos.y < - 200 ||
                                this.pos.x + this.diameter.x > game.gameWorld.getBorderX() || this.pos.x < 0){
                                    this.death = true;
                            }

                        break;
                    }
                    break;
                    case 1:

                    break;
                }
            break;
            case 1:

            break;
            case 2:

            break;
            case 3:

            break;
        }
	}

	void render(){
        if(game.debugMode == true){
            fill(0,255,255);
            rect(this.pos.x,this.pos.y,this.diameter.x,this.diameter.y);
        }

        switch(this.worldID){
            case 0:
                switch(this.type){
                    //Render goes here
                    case 0:

                    break;
                    case 1:

                    break;
                }
            break;
            case 1:

            break;
            case 2:

            break;
            case 3:

            break;
        }
    }

}
