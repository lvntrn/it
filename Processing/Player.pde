class Player extends GameActor{

  int worldID, health;
  boolean death;

	Player(int _worldID,PVector _pos, int _type){
        this.pos = _pos;
        this.worldID = _worldID;
        this.type = _type;
        this.health = 3;
    }

	void update(){
        switch(this.worldID){
            case 0:
            // PlayerLogic for World ID 0 goes Here
            switch(this.status){
                case 0:
                    this.diameter.set(192,192);
                    this.accel.set(0.5,0.5);
                    this.deaccel.set(0.05,0.05);
                    this.enableGravity = false;
                    this.maxSpeed.set(5,5);

                    this.status = 1;
                case 1:

                    if(game.input.getKeyUp()){
                        this.movement = true;
                        this.speed.sub(0,this.accel.y,0);
                    }

                    if(game.input.getKeyDown()){
                        this.movement = true;
                        this.speed.add(0,this.accel.y,0);
                    }

                    if(game.input.getKeyLeft()){
                        this.movement = true;
                        this.speed.sub(this.accel.x,0,0);
                    }

                    if(game.input.getKeyRight()){
                        this.speed.add(this.accel.x,0,0);
                        this.movement = true;
                    }

                        this.movement = false;

                    if (this.pos.y + this.diameter.y > game.gameWorld.getBorderY() +200 || this.pos.y < - 200){
                        this.speed.y *= -1;
                    }
                    if  (this.pos.x + this.diameter.x > game.gameWorld.getBorderX() || this.pos.x < 0){
                        this.speed.x *= -1;
                    }

                    runPhysics();

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
              fill(120,255,255);
              rect(this.pos.x,this.pos.y,this.diameter.x,this.diameter.y);
          }

          switch(this.worldID){
              case 0:
                  switch(this.type){
                      // for Each Player a Differend Renderer
                      case 0:

                      break;
                      case 1:

                      break;
                      case 2:

                      break;
                      case 3:

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


    void setHealth(int _health){
        this.health = _health;
    }
    int getHealth(){
        return this.health;
    }
}
