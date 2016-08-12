CONTAINER Oedgesweep
{
	NAME Oedgesweep;
	INCLUDE Obase;	

	GROUP ES_BASIC_TAB{DEFAULT 1; OPEN;
		
		LINK ES_EDGE_LINK {
			ACCEPT	{	Tedgeselection;	}
		}	
		LINK ES_SWEEP_PROFILE {
			ACCEPT	{	Ospline;Osplinerectangle;Ospline4side;
						Osplinecircle;Osplinearc;Osplinenside;
						Osplineprofile;Osplineflower;Osplinecogwheel;
						Osplineformula;Osplinetext;Osplinecissoid;Osplinecycloid;Osplinehelix;Osplinestar;Osplinecontour;	}
		}		
		SEPARATOR {}		
		REAL ES_SWEEP_OFFSET { MINSLIDER -25.0;MAXSLIDER 50.0;STEP 0.1;UNIT REAL; CUSTOMGUI REALSLIDER;}		
		LONG ES_SUBDIVISION { 
			MIN 0;
			MAX 4;
		}
		SEPARATOR {LINE;}
		GROUP{ COLUMNS 2;
			LONG ES_SPLINE_TYPE{
				CYCLE{
					TYPE_LINEAR;
					TYPE_CUBIC;
					TYPE_AKIMA;
					TYPE_BSPLINE;
					TYPE_BEZIER;
				}
			}
			BOOL ES_SPLINE_CLOSED {}
		}
		LONG ES_SPLINE_POINTS{
			CYCLE{
				POINTS_NONE;
				POINTS_NATURAL;
				POINTS_UNIFORM;
				POINTS_ADAPTIVE;
			}
		}
		LONG ES_SPLINE_SUB { MIN 0; MAX 6; }
		REAL ES_SPLINE_ANGLE { UNIT DEGREE; MIN 0.0; MAX 90.0; }		
		SEPARATOR {LINE;}		
		REAL ES_SWEEP_START_GROWTH { UNIT PERCENT; MIN 0.0; MAX 100.0; }
		REAL ES_SWEEP_END_GROWTH { UNIT PERCENT; MIN 0.0; MAX 100.0; }
		REAL ES_SWEEP_SCALE { UNIT PERCENT; MIN 0.0; }
		REAL ES_SWEEP_ROTATION { UNIT DEGREE; }
		BOOL ES_SWEEP_BANKING {}
	}

	INCLUDE Onurbscaps;
		
	
}
