using UnityEngine;
using System.Collections;

public class Rotation: MonoBehaviour{


public	Camera hcamera; 
bool flag;
bool isMouseUp ;
Rect steeringRect;
Rect steeringRect2;


	

public Texture2D touchRectTexture;	
public Texture2D texture_steering ;
//public Texture2D texture_body   ;
float stx;
float sty;
DriveScript driveScript;

public GameObject Truck;
public GameObject Truck1;
public GameObject Truck2;
public GameObject Truck3;
public GameObject Truck4;
public GameObject Truck5;
public GameObject Truck6;
public int busNum;

Vector2 p1;
Vector2 p2;

float angle2;
float angle1;

float total;
float prevtotal;
bool isClockwise;
bool isPaused;

//	private GameObject hudCameraObj;
//	private HudCameraScript hScript;
	
	void Pause( bool val ){
		isPaused = val;
  	}
	
    int Platform;
    
	void Start () 
	{

		//Truck = GameObject.FindGameObjectWithTag("Truck");


//		hudCameraObj = GameObject.Find("Hud Camera");
//		hScript = (HudCameraScript) hudCameraObj.GetComponent("HudCameraScript");

		//texture_steering = (Texture2D) Resources.Load("steering",typeof(Texture2D));
		//texture_body = (Texture2D) Resources.Load("bikebody",typeof(Texture2D));
//		stx = Utils.getX(40);
//		sty = Utils.getY(250);
		sty =  Utils.getY(260);
		stx = Utils.getX(10); 		
		flag = false;
		
		
		if(PlayerPrefs.GetInt("Truck") == 1){
			Truck = Truck1;
		}
		if(PlayerPrefs.GetInt("Truck") == 2){
			Truck = Truck2;
		}
		if(PlayerPrefs.GetInt("Truck") == 3){
			Truck = Truck3;
		}
		if(PlayerPrefs.GetInt("Truck") == 4){
			Truck = Truck4;
		}
		if(PlayerPrefs.GetInt("Truck") == 5){
			Truck = Truck5;
		}
		if(PlayerPrefs.GetInt("Truck") == 6){
			Truck = Truck6;
		}
		driveScript = (DriveScript) Truck.GetComponent("DriveScript");
		
		steeringRect = new Rect(stx,sty, Utils.getX(220), Utils.getX(220));
		steeringRect2 = new Rect(stx - Utils.getX(90),sty - Utils.getY(90), Utils.getX(400), Utils.getX(400));
		//steeringRect2 = new Rect(Utils.getX(-80),Utils.getY(150), Utils.getX(400), Utils.getY(400));
		//steeringRect2 = new Rect(-40,sty-50, Utils.getX(texture_steering.width+290), Utils.getY(texture_steering.height+280));
	 	Platform = (int) Application.platform;
//	 	for( int i = 0 ; i< Camera.allCameras.Length;i++){
// 		 	if(Camera.allCameras[i].name == "hcamera"){
// 		 		hcamera = Camera.allCameras[i];
// 		 	}
// 		}
	}
		 
     

	RaycastHit hit;
	RaycastHit hit2;
 	int steeringTouchPoint     ;
 
 
	void Update () 
	{
		if(!isPaused)
		{
			Ray ray ; 			
	 		int touchCount  = Input.touchCount;
			//int touchCount = hScript.touch;
	 		ray = hcamera.ScreenPointToRay(Input.mousePosition);
			if( Platform == (int) RuntimePlatform.OSXEditor || Platform == (int) RuntimePlatform.WindowsEditor){
		 		if(Input.GetAxis ("Horizontal") > 0){
		 			this.isMouseUp = false;
		 			total += Input.GetAxis ("Horizontal") * 8;
		 			driveScript.steer = total / driveScript.divider;
		 		}
		 		if(Input.GetAxis ("Horizontal") < 0){
		 			this.isMouseUp = false;
		 			total += Input.GetAxis ("Horizontal") * 8;
		 			driveScript.steer = total / driveScript.divider;
		 		}	
		 		else if(Input.GetAxis ("Horizontal") == 0){
		 		//	this.isMouseUp = true; 
		 		}
		 		if(Input.GetMouseButtonDown(0)){
		 				OnDown(new  Vector2(Input.mousePosition.x,Input.mousePosition.y) );  				 
		 		}
				if(Input.GetMouseButton(0)){
		 			OnDrag( new Vector2(Input.mousePosition.x,Input.mousePosition.y)); 
		 		}			
				else if(Input.GetMouseButtonUp(0)){  	 
					OnUp( );
				}
			}
			else{
		  		if(touchCount == 0)
		 		 	OnUp();
		 		for ( int i = 0 ; i < Input.touchCount;i++)
		 		{
		 			Touch touch = Input.GetTouch(i);
		 			ray = hcamera.ScreenPointToRay(touch.position);
		 			
		 			
		 			
		 			if(touch.phase == TouchPhase.Began && isInBounds(touch.position)){
		 				OnDown(touch.position);  
		 				steeringTouchPoint = i;
		 			}
		 			
				
					
					if(touch.phase == TouchPhase.Moved && isInBounds(touch.position))
		 				OnDrag(touch.position); 
		 					
					else if(touch.phase == TouchPhase.Ended){ 
						if (i == steeringTouchPoint)
		 				OnUp();
					}
					else if(touch.phase == TouchPhase.Canceled){ 
						if (i == steeringTouchPoint)
		 				OnUp();
					}
		 		}
			}
				if(this.isMouseUp){
					if(total > 0 ){
						total-=Utils.getX(14);
						if(total < 0){ 
						 	total = 0;
						 	isMouseUp = false;
						}
						driveScript.steer = total / driveScript.divider ;
					}
					else if ( total < 0){
						total+=Utils.getX(14);
						if(total > 0){ 
						 	total = 0;
						 	isMouseUp = false;
						}
						driveScript.steer = total / driveScript.divider ;
						
					}
//					if(isClockwise)
//					{
//						total-=14;
//						if(total < 0){
//						 	total = 0;
//						 	isMouseUp = false;
//						}
//						prevtotal = total;
//					}
//					else if(!isClockwise){
//						total+=14;
//						if(total > 0){
//						 	total = 0;
//						 	isMouseUp = false;
//						}
//						prevtotal = total;
//					}
//		            driveScript.steer = total / driveScript.divider ;
						
				}
		}
	}

	

	bool isInBounds( Vector2 position ){
		Vector2 v = new Vector2( position.x, Screen.height - position.y);
		return steeringRect2.Contains(v) ;
	}
	
	

 	void OnDown(Vector2 touch){   
     	isMouseUp = false;
     	p1 = touch;
    	angle1 = GetAngleOfLineBetweenTwoPoints(new Vector2(stx + steeringRect.width/2,(Screen.height - sty) - steeringRect.height/2),p1);
    }
    
    
    void OnDrag(Vector2 touch)
    {
 		angle2 = GetAngleOfLineBetweenTwoPoints(new Vector2(stx + steeringRect.width/2,(Screen.height - sty) - steeringRect.height/2) , p1 )  ;
    	if(total > 270){
    	 	total = 270;
    	}
		if(total < -270){
    	 	total = -270;
    	}
    	int a1 = (int)angle1;
    	int a2 = (int)angle2;
    	if(a1 != a2 ){
			if(a1 < 0 && a2 < 0){
				total = total + Mathf.Abs(a2) - Mathf.Abs(a1);
				driveScript.steer = total / driveScript.divider ;
			}	     
 			if(a1 > 0 && a2 > 0){
				total = total + Mathf.Abs(a1) - Mathf.Abs(a2) ;
				driveScript.steer = total / driveScript.divider ;
			}	      
			if(total >= prevtotal){
				isClockwise = true;
			}
			else if(total <= prevtotal){
				isClockwise = false; 
			}
			prevtotal =  total;
    	}
    	angle1 = angle2;
    	p1 = touch; 
    }
    
    void OnUp(){
			angle1 = angle2;
			isMouseUp = true;
    }
    
    
	float GetAngleOfLineBetweenTwoPoints( Vector2 p1 ,Vector2  p2 ) {
		float  xDiff = p2.x - p1.x; 
		float  yDiff = p2.y - p1.y; 
		return Mathf.Atan2(yDiff, xDiff) * (180 / Mathf.PI); 
	} 
 

void OnGUI(){
	if(transform != null)
	{
	 	if(total > 270){
    	 	total = 270;
    	}
		if(total < -270){
    	 	total = -270;
    	}
    	
		GUI.color = new Color32(255, 255, 255, 150);
		//GUI.DrawTexture(new Rect (Utils.getX(70),Screen.height - Utils.getY (texture_body.height ) + Utils.getY(80),Utils.getX ( texture_body.width - 100),Utils.getY (texture_body.height - 80 )),texture_body);
		GUI.depth = 2;
		GUI.color = new Color32(255, 255, 255, 255);	
		GUIUtility.RotateAroundPivot(total,new Vector2(stx + steeringRect.width/2,sty+steeringRect.height/2));
			//GUI.DrawTexture(steeringRect,texture_steering);
	 		//GUI.DrawTexture(steeringRect2,texture_steering);
		GUI.DrawTexture(steeringRect,touchRectTexture);
		//GUI.Box(steeringRect2,"");
  	}
}
}