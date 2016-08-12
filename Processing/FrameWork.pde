//Version 1.1d

//CC LaVADragon 2015

public class FrameWork{

    private PImage[] images;
    private String imagePrefix, imageExt;
    private int imageCount, frame, imageSurfix, xTileP, yTileP, scaleX, scaleY, scaleFaktor;
    private float timeDelay, lastTime ;
    private boolean tiled, animatePingPong, once, animateNorm;


    // Kontruktor for a bunch of loose Images
    FrameWork(String _imagePrefix, String _imageExt, int _imageSurfix, int _imageCount, int _timeDelay){
        this.imageCount = _imageCount;
        this.imagePrefix = _imagePrefix;
        this.imageExt = _imageExt;
        this.imageSurfix = _imageSurfix;
        this.frame = 0;
        this.lastTime = 0;
        this.once = false;
        this.timeDelay = _timeDelay;
        this.animatePingPong = false;
        this.animateNorm = false;
        this.images = new PImage[this.imageCount];
        this.tiled = false;
        this.xTileP = this.imageCount;
        this.yTileP = 1;
        this.scaleX = 0;
        this.scaleY = 0;
        this.scaleFaktor = 1;

        for (int i = 0; i < (this.imageCount); i++){
          // Load Images into Array
          String filename = this.imagePrefix + nf(i, this.imageSurfix) + this.imageExt;
          this.images[i] = loadImage(filename);
        }

    }



    //Konstruktor for Tiled AnimationsSheets Bitmaps Square
    FrameWork(String _imagePrefix, String _imageExt, int _xTileP, int _yTileP, int _scale, int _timeDelay){
        this.imagePrefix = _imagePrefix;
        this.imageExt = _imageExt;
        this.imageSurfix = 0;
        this.frame = 0;
        this.lastTime = 0;
        this.once = false;
        this.timeDelay = _timeDelay;
        this.animatePingPong = false;
        this.animateNorm = false;
        this.tiled = false;
        this.xTileP = _xTileP;
        this.yTileP = _yTileP;
        this.scaleX = _scale;
        this.scaleY = _scale;
        this.scaleFaktor = 1;

        this.imageCount = (this.xTileP*this.yTileP-1);

        if (this.yTileP > 1){
          this.tiled = true;
        }

        this.images = new PImage[this.xTileP*this.yTileP];

        // Load Images into Array
        String filename = this.imagePrefix + this.imageExt;
        PImage tiledpic = loadImage(filename);

        int i = 0;
        for (int y = 0; y < this.yTileP; y++){
            for (int x = 0; x < this.xTileP; x++){
                this.images[i] = createImage(this.scaleX, this.scaleY, ARGB);
                this.images[i].copy(tiledpic, x*this.scaleX, y*this.scaleY, this.scaleX, this.scaleY, 0, 0, this.scaleX, this.scaleY);
                i++;
            }
        }
    }

    //Konstruktor for Tiled AnimationsSheets Bitmaps nonSquare
    FrameWork(String _imagePrefix, String _imageExt, int _xTileP, int _yTileP, int _scaleX, int _scaleY, int _timeDelay){
        this.imagePrefix = _imagePrefix;
        this.imageExt = _imageExt;
        this.imageSurfix = 0;
        this.frame = 0;
        this.lastTime = 0;
        this.once = false;
        this.timeDelay = _timeDelay;
        this.animatePingPong = false;
        this.animateNorm = false;
        this.tiled = false;
        this.xTileP = _xTileP;
        this.yTileP = _yTileP;
        this.scaleX = _scaleX;
        this.scaleY = _scaleY;
        this.scaleFaktor = 1;

        this.imageCount = (this.xTileP*this.yTileP)-1;

        if (this.yTileP > 1){
            this.tiled = true;
        }

        this.images = new PImage[this.xTileP*this.yTileP];

        // Load Images into Array
        String filename = this.imagePrefix + this.imageExt;
        PImage tiledpic = loadImage(filename);

        int i = 0;
        for (int y = 0; y < this.yTileP; y++){
            for (int x = 0; x < this.xTileP; x++){
                this.images[i] = createImage(this.scaleX, this.scaleY, ARGB);
                this.images[i].copy(tiledpic, x*this.scaleX, y*this.scaleY, this.scaleX, this.scaleY, 0, 0, this.scaleX, this.scaleY);
                i++;
            }
        }
    }




    // Need to add more? must be loose Images , u can Group them  // Beta feature
    void addImages(String _imagePrefix, String _imageExt, int _imageSurfix){
        // make shure it Adds a imaginere Row to Animate Probibly
        this.xTileP = this.imageCount;
        this.yTileP++;
    // Make sure that add the same length that before ,so no mess up Animations
    int row = this.imageCount;
    for (int i = this.imageCount; i < this.imageCount*2; i++){
      // Load Images into Array

        String filename = _imagePrefix + nf(i, _imageSurfix) + _imageExt;
        this.images[i] = loadImage(filename);
    }
    // Make sure the Count is Right
    this.imageCount *= 2;
    }

    // for 0 to last Frame , need to be reset() to use this animation type again
    // for timing proposes
    void updateOnce(boolean _reverse){
        if (_reverse){
            if (this.once == false){
                if (millis() - this.lastTime >= this.timeDelay){
                        this.frame--;
                        if (this.frame <= 0){

                            this.once = true;
                            this.frame = 0;
                        }
                    this.lastTime = millis();
                }
            }
        }
        else{
            if (this.once == false){
                if (millis() - this.lastTime >= this.timeDelay){
                        this.frame++;
                        if (this.frame >= this.imageCount){

                            this.once = true;
                            this.frame = this.imageCount;
                        }
                    this.lastTime = millis();
                }
            }
        }
    }

    void resetOnce(){
        this.once = false;
    }

  // For More Rows in the Image use this Methode to define the Range   /  0 == first row vertical
    void updateLoop(int _status, int _rowY){
        switch(_status){
          // PingPong Animation
            case 0:
                if (millis() - this.lastTime >= this.timeDelay){
                    if (this.frame < this.xTileP*_rowY +1){
                        this.frame = this.xTileP*_rowY;
                        this.animatePingPong = false;
                    }
                    if (this.animatePingPong){
                        this.frame--;
                    }
                    else{
                        this.frame++;
                    }
                    if (this.frame >= this.xTileP*_rowY+this.xTileP -1){
                        // this.frame = this.xTileP*_rowY;
                        this.animatePingPong = true;
                    }
                this.lastTime = millis();
                }
            break;
                // from 0 -> lastframe ,begin again at 0
            case 1:
                if (millis() - this.lastTime >= this.timeDelay){

                    if (this.frame < this.xTileP*_rowY){
                        this.frame = this.xTileP*_rowY;
                    }
                    this.frame++;

                    if (this.frame >= this.xTileP*_rowY+this.xTileP){
                        this.frame = this.xTileP*_rowY;
                    }
                this.lastTime = millis();
                }
            break;
        }
    }

    // Standart Methode , may be updated
    void updateLoop(int _status){
        switch(_status){
            // PingPong Animation
            case 0:
                if (millis() - this.lastTime >= this.timeDelay){

                    if (this.animatePingPong == false){
                        this.frame++;
                        if (this.frame >= this.imageCount){
                            this.frame--;
                            this.animatePingPong = !this.animatePingPong;
                        }
                    }
                    if (this.animatePingPong == true){
                        this.frame--;
                        if (this.frame <= 0){
                            this.frame = 0;
                            this.animatePingPong = !this.animatePingPong;
                        }
                    }
                    this.lastTime = millis();
                }
            break;
            // from 0 -> lastframe ,begin again at 0
            case 1:
                if (millis() - this.lastTime >= this.timeDelay){
                    this.frame = (this.frame+1) % this.imageCount;
                    this.lastTime = millis();
                }
            break;
        }
    }



    // Easyest Way to draw a Picture from this Array
    void drawImage(float _x, float _y, int _frame){
        if ( _frame >= 0 && _frame < this.imageCount){
            // Update Frames
            image(this.images[_frame], _x, _y, this.images[_frame].width*this.scaleFaktor, this.images[_frame].height*this.scaleFaktor);
        }
    }

    // If u need the Regular PImage Methode
    void drawImage(float _x, float _y, int _width, int _height, int _frame){
    if ( _frame >= 0 && _frame < this.imageCount){
        // Update Frames
        image(this.images[_frame], _x, _y, _width, _height);
        }
    }

    // If u need to Scale and Rotate the Picture
    void drawImage(float _x, float _y, int _rotate, float _mirrorX, float _mirrorY, int _frame){
        if ( _frame >= 0 && _frame < this.imageCount){
            pushMatrix();
            // Rolate it & Scale it
            translate(_x+ this.images[_frame].width/2, _y +this.images[_frame].height/2);
            rotate(radians(_rotate));
            scale(_mirrorX, _mirrorY);
            translate(-_x - this.images[_frame].width/2, -_y - this.images[_frame].height/2);
            // Draw it
            image(this.images[_frame], _x, _y, this.images[_frame].width*this.scaleFaktor, this.images[_frame].height*this.scaleFaktor );
            popMatrix();
        }
    }

    // If u need to Scale and Rotate the Picture and to also stretch the Picture
    // Most Complicated way to Draw the Picture
    void drawImage(float _x, float _y, int _rotate, float _mirrorX, float _mirrorY, int _width, int _height, int _frame){
        if ( _frame >= 0 && _frame < this.imageCount){
            pushMatrix();
            // Rolate it & Scale it
            translate(_x+ this.images[_frame].width/2, _y +this.images[_frame].height/2);
            rotate(radians(_rotate));
            scale(_mirrorX, _mirrorY);
            translate(-_x - this.images[_frame].width/2, -_y - this.images[_frame].height/2);
            // Draw it
            image(this.images[_frame], _x, _y, _width, _height );
            // Dont forget to Pop the Matrix:D
            popMatrix();
        }
    }


    // Easyest Way to draw a Animation from this Array
    void drawAnimation(float _x, float _y){
        if (this.once == false){
            // Update Frames
            image(this.images[this.frame], _x, _y, this.images[this.frame].width*this.scaleFaktor, this.images[this.frame].height*this.scaleFaktor);
        }
    }

    // If u need the Regular PImage Methode with Animation
    void drawAnimation(float _x, float _y, int _width, int _height){
        if (this.once == false){
            // Update Frames
            image(this.images[this.frame], _x, _y, _width, _height);
        }
    }

    // If u need to Scale and Rotate the Picture even when its Animate
    void drawAnimation(float _x, float _y, int _rotate, float _mirrorX, float _mirrorY){
        if (this.once == false){
            pushMatrix();
            // Rolate it & Scale it
            translate(_x+ this.images[this.frame].width/2, _y +this.images[this.frame].height/2);
            rotate(radians(_rotate));
            scale(_mirrorX, _mirrorY);
            translate(-_x - this.images[this.frame].width/2, -_y - this.images[this.frame].height/2);
            // Draw it
            image(this.images[this.frame], _x, _y, this.images[this.frame].width*this.scaleFaktor, this.images[this.frame].height*this.scaleFaktor);
            // Dont forget to Pop the Matrix:D
            popMatrix();
        }
    }

    // If u need to Scale and Rotate the Picture and to stretch the Picture even with Animation
    // Ure Serious?
    void drawAnimaton(float _x, float _y, int _rotate, float _mirrorX, float _mirrorY, int _width, int _height){
        if (this.once == false){
            pushMatrix();
            // Rolate it & Scale it
            translate(_x+ this.images[this.frame].width/2, _y +this.images[this.frame].height/2);
            rotate(radians(_rotate));
            scale(_mirrorX, _mirrorY);
            translate(-_x - this.images[this.frame].width/2, -_y - this.images[this.frame].height/2);
            // Draw it
            image(this.images[this.frame], _x, _y, _width, _height );
            // Dont forget to Pop the Matrix:D
            popMatrix();
        }
    }

    // Some getter and setter, may be usefull.
    int getWidth(){
        return this.images[0].width*this.scaleFaktor;
    }
    int getHeight(){
        return this.images[0].height*this.scaleFaktor;
    }
    int getFrame(){
        return this.frame;
    }
    void setFrame(int _frame){
        this.frame = _frame;
    }
    boolean done(){
        return this.once;
    }
    int getImageCount(){
        return this.imageCount;
    }
    int getXTile(){
        return this.xTileP;
    }
    int getYTile(){
        return this.yTileP;
    }
    int getScaleX(){
        return this.scaleX;
    }
    int getScaleY(){
        return this.scaleY;
    }
    PImage getArray(int _frame){
        return this.images[_frame];
    }
    // set Animation Speed
    void setDelay(float _timeDelay){
        this.timeDelay = _timeDelay;
    }
    int getScaleFactor(){
        return this.scaleFaktor;
    }
    void setScaleFactor(int _scaleFaktor){
        this.scaleFaktor = _scaleFaktor;
    }

}
