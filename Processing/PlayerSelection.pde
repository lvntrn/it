private class PlayerSelection{
// PlayerSelection Buffer Class
	private int player[];
	private int maxPlayer;
	private int worldID;

	PlayerSelection(int _maxPlayer){
		this.maxPlayer = _maxPlayer;
		this.player = new int[this.maxPlayer];

		for (int i = 0; i < this.maxPlayer; i++) {
			this.player[i] = 0;
		}

	}

	void setPlayerStatus(int _player,int _status){
		player[_player] = _status;
	}
	int getPlayerStatus(int _player){
		return player[_player];
	}

	void setWorldID(int _worldID){
		this.worldID = _worldID;
	}
	int getWorldID(){
		return this.worldID;
	}
	int getMaxPlayer(){
		return this.maxPlayer;
	}
}
