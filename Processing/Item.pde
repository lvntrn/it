class Item extends GameActor{

	public int worldID;

	Item(int _worldID,PVector _pos,int _type){
              this.pos = _pos;
     

		this.worldID = _worldID;
		this.type = 0;
    }


	void update(){
		switch(this.worldID){
			case 0:
				switch(this.type){
					//Logic for each differend Item goes here
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

	void render(){
		if(game.debugMode == true){
			fill(190,255,255);
			rect(this.pos.x,this.pos.y,this.diameter.x,this.diameter.y);
		}

		switch(this.worldID){
			case 0:
				switch(this.type){
					//for each Type a differend Renderer? What u like?
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
