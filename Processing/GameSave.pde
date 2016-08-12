// Version 1.0

// CC LaVADraGoN 2015

public class GameSave{
	private int[] worldID, highScore, maxLV;
	private int saveFileSize;
	private JSONArray savefile;
	private JSONObject data;


	GameSave(int _size){

		this.saveFileSize = _size;
		this.worldID = new int[this.saveFileSize];
		this.highScore = new int[this.saveFileSize];
		this.maxLV = new int[this.saveFileSize];
		this.savefile = new JSONArray();
		this.data = new JSONObject();

	    try{
	    	// Try Load Savefile From Disk
	        this.savefile = loadJSONArray("data/savefile.json");
		}
	    catch (Exception e){
	    	// If Not there Create New One
	    	createFile();
	    }
		// Load JSON into Class
		this.reloadAll();
	}

	void reloadAll(){

		// Parse Savefile into Variables
		for (int i = 0; i < this.savefile.size(); i++) {
			this.data = savefile.getJSONObject(i);
			this.worldID[i] = data.getInt("worldID");
			this.highScore[i] = data.getInt("highScore");
			this.maxLV[i] = data.getInt("maxLV");
		}
		// Clear DataObject
		this.data = new JSONObject();
	}


	void saveAll(){

		for (int i = 0; i < this.saveFileSize; i++) {
			// Parse Data from Arrays into JSON Object
		    this.data.setInt("worldID", this.worldID[i]);
		    this.data.setInt("highScore", this.highScore[i]);
		    this.data.setInt("maxLV", this.maxLV[i]);
		    // Add Data to JsonArray
			this.savefile.setJSONObject(i, this.data);
			// Clear DataObject
			this.data = new JSONObject();
		}
		// Save JsonArray to HardDrive
	    saveJSONArray(this.savefile, "data/savefile.json");
	}


	void createFile(){
		for (int i = 0; i < this.saveFileSize; i++) {
			this.worldID[i] = i;
			this.highScore[i] = 0;
			this.maxLV[i] = 0;
			// Parse Data from Arrays into JSON Object
			this.data.setInt("worldID", this.worldID[i]);
			this.data.setInt("highScore", this.highScore[i]);
			this.data.setInt("maxLV", this.maxLV[i]);
			// Add Data to JsonArray
			this.savefile.setJSONObject(i, this.data);
			// Clear DataObject
			this.data = new JSONObject();
		}
		// Save JsonArray to HardDrive
		saveJSONArray(this.savefile, "data/savefile.json");
	}


	void printArray(String _name){

		if (_name == "all"){
			for (int i = 0; i < this.savefile.size(); i++) {
				println("WorldID = "+ this.worldID[i] + ", HighScore = " + this.highScore[i] + ", maxLV = " + this.maxLV[i]);
			}
		}
		if (_name == "worldID"){
			for (int i = 0; i < this.savefile.size(); i++) {
				println("WorldID["+i+"] = " + this.worldID[i]);
			}
		}
		if (_name == "highScore"){
			for (int i = 0; i < this.savefile.size(); i++) {
				println("highScore["+i+"] = " + this.highScore[i]);
			}
		}
		if (_name == "maxLV"){
			for (int i = 0; i < this.savefile.size(); i++) {
				println("maxLV["+i+"] = " + this.maxLV[i]);
			}
		}
	}


	int getHighScore(int _worldID){
		return this.highScore[_worldID];
	}

	int getMaxLV(int _worldID){
		return this.maxLV[_worldID];
	}
	void setHighScore(int _worldID, int _highScore){
		this.highScore[_worldID] = _highScore;
	}
	void setMaxLV(int _worldID,int _maxLV){
		this.maxLV[_worldID] = _maxLV;
	}

}
