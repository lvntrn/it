public class GameWorld{

	private int worldID, lv, highscore, maxPlayer, score , worldStatus,gBorderX,gBorderY;
        private float timeDelay, lastTime ;
        private ArrayList<Player> playerList;
	private ArrayList<Enemy> enemyList;
	private ArrayList<Item> itemList;
	private ArrayList<Particle> particleList;

	GameWorld(int _worldID, int _lv){
		this.worldID = _worldID;
		this.lv = _lv;
		this.highscore = 0;
		this.score = 0;
		this.maxPlayer = 0;
		this.worldStatus = 0;
        this.gBorderX = screenWidth;
        this.gBorderY = screenHeight;
        this.lastTime = 0;
        this.timeDelay = 1000; // ms  Aka Spawnrate
		this.particleList = new ArrayList<Particle>();
		this.playerList = new ArrayList<Player>();
		this.enemyList = new ArrayList<Enemy>();
		this.itemList = new ArrayList<Item>();
	}

	//GameLogic
	void update(){
		switch(this.worldID){
			case 0:
			switch(this.worldStatus){
				case 0:
        	        	// Test Create
		            this.createPlayer(new PVector(screenWidth/3,700),0);

		            this.worldStatus = 1;
                case 1:
					if (this.enemyList.size() < 10){

						if (millis() - this.lastTime >= this.timeDelay){
							this.createEnemy(new PVector(random(50,(screenWidth-50)),-64),0);
							this.lastTime = millis();
						}
					}

					this.updateAll();
				break;
			}

			case 1:
			break;

			case 2:
			break;

			case 3:
			break;
		}
	}

    //GameRender
	void render(){
		switch(this.worldID){
			case 0:
			switch(this.worldStatus){
				case 0:

				case 1:
			        background(0);
					renderAll();
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

	void saveStats(){
		gameSave.setHighScore(this.worldID, this.highscore);
        gameSave.setMaxLV(this.worldID, this.lv);
        gameSave.saveAll();

	}

	void loadStats(){
		gameSave.reloadAll();
		gameSave.getHighScore(this.worldID);
        gameSave.getMaxLV(this.worldID);
	}

	// Getter & Setter
	void setMaxPlayer(int _maxPlayer){
		this.maxPlayer = _maxPlayer;
	}

	// Create entities
	void createPlayer(PVector _pos,int _player){
		Player player = new Player(this.worldID, _pos, _player);
        this.playerList.add(player);
	}
	void createEnemy(PVector _pos, int _type){
		Enemy enemy = new Enemy(this.worldID, _pos, _type);
        this.enemyList.add(enemy);
	}
	void createItem(PVector _pos,int _type){
		Item item = new Item(this.worldID, _pos, _type);
        this.itemList.add(item);
	}
	void createParticle(PVector _pos,PVector _diameter, PVector _speed, PVector _deaccel, boolean _movement, boolean _hsbLoop,int _type,
    	float _hsbLoopAmount, color _colorHSB, float _gravity, float _timeDelay, float _radiusRedux){
		Particle particle = new Particle(new PVector(_pos.x,_pos.y) ,new PVector(_diameter.x,_diameter.y), new PVector(_speed.x,_speed.y),new PVector(_deaccel.x,_deaccel.y), _movement, _hsbLoop, _type, _hsbLoopAmount, color(_colorHSB), _gravity, _timeDelay, _radiusRedux);
		this.particleList.add(particle);
	}

	// Render & Update entities
	void renderPlayer(){
		for(int i = 0; i < this.playerList.size(); i++){
			this.playerList.get(i).render();
		}
	}
	void updatePlayer(){
		for(int i = 0; i < this.playerList.size(); i++){
			this.playerList.get(i).update();
			if (this.playerList.get(i).getDeath()){
				this.playerList.remove(i);
			}
		}
	}
	void renderEnemy(){
		for(int i = 0; i < this.enemyList.size(); i++){
			this.enemyList.get(i).render();
		}
	}
	void updateEnemy(){
		for(int i = 0; i < this.enemyList.size(); i++){
			this.enemyList.get(i).update();
			if (this.enemyList.get(i).getDeath()){
				this.enemyList.remove(i);
			}
		}
	}
	void renderItem(){
		for(int i = 0; i < this.itemList.size(); i++){
			this.itemList.get(i).render();
		}
	}
	void updateItem(){
		for(int i = 0; i < this.itemList.size(); i++){
			this.itemList.get(i).update();
			if (this.itemList.get(i).getDeath()){
				this.itemList.remove(i);
			}
		}
	}
	void renderParticle(){
		for(int i = 0; i < this.particleList.size(); i++){
			this.particleList.get(i).render();
		}
	}
	void updateParticle(){
		for(int i = 0; i < this.particleList.size(); i++){
			this.particleList.get(i).update();
			if (this.particleList.get(i).burnOut()){
				this.itemList.remove(i);
			}
		}
	}
	void updateAll(){
		updatePlayer();
		updateEnemy();
		updateItem();
		updateParticle();
	}
	void renderAll(){
		renderPlayer();
		renderEnemy();
		renderItem();
		renderParticle();
	}
    int getBorderX(){
		return this.gBorderX;
    }
    int getBorderY(){
		return this.gBorderY;
    }

}
