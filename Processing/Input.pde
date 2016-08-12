public class Input {

    // Used to get Keys for other Classes
    public boolean KeyUp, KeyDown, KeyLeft, KeyRight, PmouseButtonL, RmouseButtonL, RmouseButtonR, PmouseButtonR, KeyF3,
    mouseTickR, mouseTickL, KeyAlt, KeyDelete, KeyP, KeyO;

    Input(){
        this.KeyUp = false;
        this.KeyDown = false;
        this.KeyLeft = false;
        this.KeyRight = false;
        this.PmouseButtonL = false;
        this.PmouseButtonR = false;
        this.RmouseButtonL = false;
        this.RmouseButtonR = false;
        this.mouseTickL = false;
        this.mouseTickR = false;
        this.KeyAlt = false;
        this.KeyDelete = false;
        this.KeyF3 = false;
        this.KeyP = false;
        this.KeyO = false;
    }

    //Key Getter
    boolean getKeyUp(){
        return this.KeyUp;
    }
    boolean getKeyDown(){
        return this.KeyDown;
    }
    boolean getKeyLeft(){
        return this.KeyLeft;
    }
    boolean getKeyRight(){
        return this.KeyRight;
    }
    boolean getKeyAlt(){
        return this.KeyAlt;
    }
    boolean getPMouseButtonR(){
        return this.PmouseButtonR;
    }
    boolean getPMouseButtonL(){
        return this.PmouseButtonL;
    }
    boolean getRMouseButtonR(){
        return this.RmouseButtonR;
    }
    boolean getRMouseButtonL(){
        return this.RmouseButtonL;
    }
    boolean getMouseTickR(){
        return this.mouseTickR;
    }
    boolean getMouseTickL(){
        return this.mouseTickL;
    }
    boolean getKeyDelete(){
        return this.KeyDelete;
    }
    boolean getKeyF3(){
        return this.KeyF3;
    }
    boolean getKeyP(){
        return this.KeyP;
    }
    boolean getKeyO(){
        return this.KeyO;
    }

    // Key Setter
    void setKeyUp(boolean _setKeyUp){
        this.KeyUp = _setKeyUp;
    }
    void setKeyDown(boolean _setKeyDown){
        this.KeyDown = _setKeyDown;
    }
    void setKeyLeft(boolean _setKeyLeft){
        this.KeyLeft = _setKeyLeft;
    }
    void setKeyRight(boolean _setKeyRight){
        this.KeyRight = _setKeyRight;
    }
    void setKeyAlt(boolean _setKeyAlt){
        this.KeyAlt = _setKeyAlt;
    }
    void setPMouseButtonR(boolean _setPMouseButtonR){
        this.PmouseButtonR = _setPMouseButtonR;
    }
    void setPMouseButtonL(boolean _setPMouseButtonL){
        this.PmouseButtonL = _setPMouseButtonL;
    }
    void setRMouseButtonR(boolean _setRMouseButtonR){
        this.RmouseButtonR = _setRMouseButtonR;
    }
    void setRMouseButtonL(boolean _setRMouseButtonL){
        this.RmouseButtonL = _setRMouseButtonL;
    }
    void setMouseTickR(boolean _setMouseTickR){
        this.mouseTickR = _setMouseTickR;
    }
    void setMouseTickL(boolean _setMouseTickL){
        this.mouseTickL = _setMouseTickL;
    }
    void setKeyDelete(boolean _setKeyDelete){
        this.KeyDelete = _setKeyDelete;
    }
    void setKeyF3(boolean _setKeyF3){
        this.KeyF3 = _setKeyF3;
    }
    void setKeyP(boolean _setKeyP){
        this.KeyP = _setKeyP;
    }
    void setKeyO(boolean _setKeyO){
        this.KeyO = _setKeyO;
    }
}

//Class Closed


// Global Events (Processing)

//If Key Pressed
void keyPressed(){
    if (key == CODED){
        if (keyCode == UP){
        game.input.setKeyUp(true);
        }
        if (keyCode == DOWN){
        game.input.setKeyDown(true);
        }
        if (keyCode == LEFT){
        game.input.setKeyLeft(true);
        }
        if (keyCode == RIGHT){
        game.input.setKeyRight(true);
        }
        if (keyCode == SHIFT){
        game.input.setKeyAlt(true);
        }
        if (keyCode == DELETE){
        game.input.setKeyDelete(true);
        }
        if (keyCode == 114){
        game.input.setKeyF3(true);
        }
    }

    // Custom Keys Pressed

    switch(key) {
        case('p'):
        case('P'):
        game.input.setKeyP(true);
        break;
        case('o'):
        case('O'):
        game.input.setKeyO(true);
        break;
    }
}

// if Key Released
void keyReleased()
    {
    if (key == CODED){
        if (keyCode == UP){
            game.input.setKeyUp(false);
        }
        if (keyCode == DOWN){
            game.input.setKeyDown(false);
        }
        if (keyCode == LEFT){
            game.input.setKeyLeft(false);
        }
        if (keyCode == RIGHT){
            game.input.setKeyRight(false);
        }
        if (keyCode == SHIFT){
            game.input.setKeyAlt(false);
        }
        if (keyCode == DELETE){
            game.input.setKeyDelete(false);
        }
        if (keyCode == 114){
        game.input.setKeyF3(false);
        }
    }

    // Custom Keys Pressed
    switch(key) {
        case('p'):
        case('P'):
        game.input.setKeyP(false);
        break;
        case('o'):
        case('O'):
        game.input.setKeyO(false);
        break;
    }
}

// Global Events Mouse (Processing)

// Mouse is pressed
void mousePressed(){
    if (mouseButton == LEFT){
    game.input.setPMouseButtonL(true);
    game.input.setRMouseButtonL(false);
    }
    if (mouseButton == RIGHT){
    game.input.setPMouseButtonR(true);
    game.input.setRMouseButtonR(false);
    }
}

// Mouse is released
void mouseReleased(){
    if (mouseButton == LEFT){
        game.input.setPMouseButtonL(false);
        game.input.setRMouseButtonL(true);
    }
    if (mouseButton == RIGHT){
        game.input.setPMouseButtonR(false);
        game.input.setRMouseButtonR(true);
    }
}

// Mouse is clicked
void mouseClicked(){
    if (mouseButton == LEFT){
        game.input.setMouseTickL(true);
    }
    if (mouseButton == RIGHT){
        game.input.setMouseTickR(true);
    }

}
