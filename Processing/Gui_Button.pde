public class Gui_Button{

    public PVector pos, posSave, scale, textAlign, offset, border;
    private int fontSize, status, imageCount, transitionType, transitionStatus, edge;
    private float varT, minT, maxT, speedT, timeElapsed, maxTime, transitionSpeed;
    public boolean hitTest, selected, renderPrimitives, inTransition, finish;
    private String buttonName, type;
    private PFont font;
    private FrameWork image;

     // Basic
    Gui_Button(PVector _pos, String _buttonName, int _status, String _type, String _transition){
        this.pos = _pos;
        this.posSave = new PVector(this.pos.x,this.pos.y);
        this.scale = new PVector(300,100);
        this.textAlign = new PVector(this.scale.x/2,72);
        this.offset = new PVector(0, 0);
        this.type = _type;
        this.hitTest = false;
        this.buttonName = _buttonName;
        this.fontSize = 64;
        this.varT = 60;
        this.minT = 40;
        this.maxT = 220;
        this.speedT = 2; // Transition Speed Hover
        this.transitionType = getTransition(_transition);
        this.transitionSpeed = 0.05;
        this.status = _status;
        this.font = gamefont;
        this.image = null;
        this.imageCount = 0;
        this.border = new PVector(10,10);
        this.renderPrimitives = true;
        this.transitionStatus = 0;
        this.edge = 30;
        this.timeElapsed = 0;
        this.maxTime = 200;
        this.finish = false;
    }

    // Advanced Konstructor
    Gui_Button(PVector _pos, PVector _scale, String _buttonName, int _fontSize, PVector _textAlign, int _status,String _type, PFont _font, String _transition){
        this.pos = _pos;
        this.posSave = new PVector(this.pos.x,this.pos.y);
        this.scale = _scale;
        this.textAlign = _textAlign;
        this.offset = new PVector(0, 0);
        this.type = _type;
        this.hitTest = false;
        this.buttonName = _buttonName;
        this.fontSize = _fontSize;
        this.varT = 60;
        this.minT = 40;
        this.maxT = 220;
        this.speedT = 2; // Transition Speed Hover
        this.transitionType = getTransition(_transition);
        this.transitionSpeed = 0.05;
        this.status = _status;
        this.font = _font;
        this.image = null;
        this.imageCount = 0;
        this.border = new PVector(10,10);
        this.renderPrimitives = true;
        this.transitionStatus = 0;
        this.edge = 30;
        this.timeElapsed = 0;
        this.maxTime = 200;
        this.finish = false;

    }
     //// Basic Framwork (with Image)  Konstructor
     Gui_Button(PVector _pos, String _buttonName, int _status, String _type, String _transition, FrameWork _image){
        this.pos =_pos;
        this.posSave = new PVector(this.pos.x,this.pos.y);
        this.image = _image;
        this.scale = new PVector(this.image.getWidth(), this.image.getHeight());
        this.textAlign = new PVector(this.scale.x/2,this.scale.y/2);
        this.offset = new PVector(0, 0);
        this.type = _type;
        this.hitTest = false;
        this.buttonName = _buttonName;
        this.fontSize = int(this.scale.y /3) ;
        this.varT = 60;
        this.minT = 40;
        this.maxT = 220;
        this.speedT = 2; // Transition Speed Hover
        this.transitionType = getTransition(_transition);
        this.transitionSpeed = 0.05;
        this.status = _status;
        this.font = gamefont;
        this.image.getImageCount();
        this.border = new PVector(10,10);
        this.renderPrimitives = false;
        this.transitionStatus = 0;
        this.edge = 30;
        this.timeElapsed = 0;
        this.maxTime = 200;
        this.finish = false;
    }

    void update(){

    updateHover();
    updateTransition();

    }

    void updateTransition(){

        switch(this.transitionType){
        case 0:
            //None
        break;
        case 1:
            //swipeIn
            switch(this.transitionStatus){
                case 0:
                    randomMaker(this.transitionType,0);
                    this.timeElapsed = 0;
                    this.transitionStatus = 1;
                case 1:
                    this.inTransition = true;
                    this.pos.lerp(this.posSave, this.transitionSpeed);
                    this.timeElapsed++;

                    if (this.timeElapsed >= this.maxTime){
                    this.transitionStatus = 2;
                    this.inTransition = false;
                    randomMaker(this.transitionType,1);
                    this.timeElapsed = 0;
                    }
                break;
                case 2:
                //Nothing
                break;
                case 3:
                    this.inTransition = true;
                    this.pos.lerp(this.posSave, this.transitionSpeed);
                    this.timeElapsed++;
                    if (this.timeElapsed >= this.maxTime-100){
                        this.finish = true;
                    }
                break;
         }

        break;
        case 2:
             switch(this.transitionStatus){
              case 0:
                  randomMaker(this.transitionType,0);
                  this.timeElapsed = 0;
                  this.transitionStatus = 1;
              case 1:
                  this.inTransition = true;
                  this.pos.lerp(this.posSave, this.transitionSpeed);
                  this.timeElapsed++;

                  if (this.timeElapsed >= this.maxTime){
                  this.transitionStatus = 2;
                  this.inTransition = false;
                  randomMaker(this.transitionType,1);
                  this.timeElapsed = 0;
                  }
              break;
              case 2:
              //Nothing
              break;
              case 3:
                  this.inTransition = true;
                  this.pos.lerp(this.posSave, this.transitionSpeed);
                  this.timeElapsed++;
                  if (this.timeElapsed >= this.maxTime-100){
                      this.finish = true;
                  }
              break;
             }

        break;
        }

    }

    // Update Status on Buttons
    void updateHover(){
        if(this.image != null){
            this.hitTest = makeHitTest();
            if (this.hitTest == true){
                if (this.renderPrimitives = true){
                    hoverEffect(this.hitTest);
                }
                this.image.updateOnce(false);
                if( this.image.getFrame() >= 0 ){
                    this.image.resetOnce();
                }
            }
            if (this.hitTest == false){
                if (this.renderPrimitives = true){
                    hoverEffect(this.hitTest);
                }
                this.image.updateOnce(true);
                if( this.image.getFrame() <= this.imageCount ){
                    this.image.resetOnce();
                }
            }
        }
        else
        {
            this.hitTest = makeHitTest();
            if(this.renderPrimitives == true){
                hoverEffect(this.hitTest);
            }
        }
    }

    void render(){
        if(this.renderPrimitives == true){
            textAlign(CENTER);
            if (this.selected == true){
                fill(this.varT+200, 255, 255);
                rect(this.offset.x+this.pos.x-this.border.x, this.offset.y + this.pos.y-this.border.y, (this.scale.x+this.border.x*2), this.scale.y+(this.border.y*2));
            }
            fill(this.varT);
            rect(this.offset.x+this.pos.x, this.offset.y + this.pos.y, this.scale.x, this.scale.y,this.edge);
            textFont(this.font, this.fontSize);

            if(this.hitTest){
                fill(this.varT+50, 255, 255);
            }
            else{
                fill(this.varT+0, 255, 255);
            }
            text(this.buttonName, this.offset.x +this.pos.x + this.textAlign.x, this.offset.y + this.pos.y + this.textAlign.y);
        }
        if(this.image != null){
        this.image.drawAnimation(this.pos.x, this.pos.y, int(this.scale.x), int(this.scale.y));
        }
    }

    void hoverEffect(boolean _hitTest){
        if (_hitTest){
            if (this.varT < maxT){
                this.varT += this.speedT;
            }
        }else{
            if (this.varT > minT){
                this.varT -= this.speedT;
            }
        }
    }
    // Getter & Setter

    // Return True if Mouse is over the Button
    boolean getHitTest(){
        return this.hitTest;
    }

    // Return the int Value, its getting as if was created (to set gamestatus)
    int getStatus(){
        return this.status;
    }

    // Return the Type
    String getType(){
        return this.type;
    }

    // Move the Buttons Around;)
    void setOffset(float _offsetX, float _offsetY){
        this.offset.set( _offsetX, _offsetY);
    }

    boolean makeHitTest(){
        if (mouseX >= this.pos.x && mouseX <= this.pos.x + this.scale.x && mouseY >= this.pos.y && mouseY <= this.pos.y + this.scale.y ){
          return true;
        }
        else{
            return false;
        }
    }

    void setSelected(boolean _selected){
        this.selected = _selected;
    }
    boolean getSelected(){
        return this.selected;
    }

  // Set Transision Type
  int getTransition(String _transition){
      int type = 0;
      if (_transition == "swipeInRandom"){
          type = 1;
      }
       if (_transition == "swipeSide"){
          type = 2;
      }
      return type;
  }

  void randomMaker(int _transistionType,int _inverted){
      int random = int(random (0,2));
      switch(_transistionType){
      case 0:
      //Nothin
      break;
      case 1:
        switch(_inverted){
            case 0:
                switch(random){
                    case 0:
                        this.pos.x = displayWidth + this.scale.x + random(0,200);
                        this.pos.y =+ random(-50,50);
                    break;
                    case 1:
                        this.pos.x = 0 - this.scale.x - random(0,200);
                        this.pos.y =+ random(-50,50);
                    break;
                }
            break;
            case 1:
                switch(random){
                    case 0:
                        this.posSave.x = displayWidth + this.scale.x + random(0,200);
                        this.posSave.y =+ random(-50,50);
                    break;
                    case 1:
                        this.posSave.x = 0 - this.scale.x - random(0,200);
                        this.posSave.y =+ random(-50,50);
                    break;
                }
            break;
        }
      break;
      case 2:
        switch(_inverted){
              case 0:

                  if (this.pos.x > displayWidth/2){
                      this.pos.x = displayWidth + this.scale.x + random(0,200);
                      this.pos.y =+ random(-50,50);
                  }
                  else{
                      this.pos.x = 0 - this.scale.x - random(0,200);
                      this.pos.y =+ random(-50,50);
                  }
              break;
              case 1:
                 if (this.posSave.x > displayWidth/2){
                      this.posSave.x = displayWidth + this.scale.x + random(0,200);
                      this.posSave.y =+ random(-50,50);
                  }
                  else{
                      this.posSave.x = 0 - this.scale.x - random(0,200);
                      this.posSave.y =+ random(-50,50);
                  }
              break;
          }

      break;
      }

  }


  void setReverse(){
    switch(this.transitionStatus){
    case 0:
    // none (add fadeOut later)
    break;
    case 1:
    this.transitionStatus = 3;
    break;
    case 2:
    this.transitionStatus = 3;
    break;

    }
  }

  boolean getInTransition(){
      return this.inTransition;
  }

  boolean getFinish(){
      return this.finish;
  }
}
