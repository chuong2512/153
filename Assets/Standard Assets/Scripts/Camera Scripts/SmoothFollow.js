/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function.
Then we apply the smoothed values to the transform's position.
*/

	// The target we are following
	var target : Transform;
	//public var tempObj :GameObject;
	// The distance in the x-z plane to the target
	var distance = 90.0;
	// the height we want the camera to be above the target
	var height = 90.0;
	// How much we 
	var heightDamping = 2.0;
	var rotationDamping = 3.0;

    


@script AddComponentMenu("Camera-Control/Smooth Follow")




	private var wantedRotationAngle : float ;
	private var wantedHeight : float;
	private var currentRotation : Quaternion;
	
	private var currentRotationAngle : float;
	private var currentHeight : float;
	private var angle_offsetX : float;
	private var height_offsetY : float;


	private var touch1 : Vector3;
	private var touch2 : Vector3;
	var platform :int;
	var rect1 : Rect;
	var rect2 : Rect;
	var rect3 : Rect;
	
	function Awake(){
	
	
   // target=GameObject.FindGameObjectWithTag("Truck").transform;	
	
	
	}
	function  Start()
	{
	height = 90.0;
	heightDamping = 100.0;   
	 	platform = Application.platform;
	 	
		rect1 =  Rect( getX(0), getY(150),getX(375),getY(380));
		rect2 = Rect( getX(570), getY(340),getX(260),getY(140));
		
	 	rect3 = Rect( getX(680), getY(200),getX(120),getY(160));
	 	target=GameObject.FindGameObjectWithTag("Truck").transform;	
	 	
	}
	function SetTarget(obj : Transform)
	{
	//target=GameObject.FindGameObjectWithTag("Truck").transform;
		//target = obj;
		angle_offsetX = 0;
		height_offsetY = height ;	
	}
	function Update()
	{
	
	
	

		height += 0.01;
	
		var t1 : int  ;
		var t2 :int ;
	 	
	 	
	 	var h1 : int  ;
		var h2 :int ;
	  
	 	
	 
	 
		if(platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.WindowsEditor)
		{
			
			if ( !rect1.Contains(Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y)) && !rect2.Contains(Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y)) &&  !rect3.Contains(Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y)) )
			{
			
			
				if(Input.GetMouseButtonDown(0))
				{
					touch1 = Input.mousePosition;
					touch2 = Input.mousePosition;
				}
				if(Input.GetMouseButton(0))
				{
					t1   = touch1.x;
					t2   = Input.mousePosition.x;
	 
	 				h1   = touch2.y;
					h2   = Input.mousePosition.y;
	 
	 				angle_offsetX += ((t2-t1)/2);
	 	
					height_offsetY -= ((h2-h1) / 20.0);
	 	
	 				if(height_offsetY < 0)
	 					height_offsetY = 0;
	 				if(height_offsetY > 4.5f)
	 					height_offsetY = 4.5f;
	 	
 	 				rotationDamping = 6;
					heightDamping = 6;
						
					touch1 = Input.mousePosition;
					touch2 = Input.mousePosition;
					
				}
				if(Input.GetMouseButtonUp(0))
				{	
					rotationDamping = 2;
					height = 2;
				}
				
			}
			
		}
		else
		{
			
			for ( var i : int  = 0 ; i < Input.touchCount  && Input.touchCount == 1  ; i++ ) 
	    	{
	      		var touch: Touch = Input.GetTouch(i);
 				 
	 			 
 					
 				if(!rect1.Contains(Vector2( touch.position.x,Screen.height - touch.position.y)) && !rect2.Contains(Vector2(touch.position.x,Screen.height - touch.position.y)) && !rect3.Contains(Vector2(touch.position.x,Screen.height - touch.position.y)))
 				{
				 	
				 	if(touch.phase == TouchPhase.Began )
					{
						touch1 = touch.position; 
						touch2 = touch.position; 
					}
				 	if(touch.phase == TouchPhase.Moved )
					{
						t1    = touch1.x;
						t2    = Input.mousePosition.x;
		 
						h1   = touch2.y;
						h2   = Input.mousePosition.y;
		 
		 				angle_offsetX += (t2-t1)/2;
		 				
						height_offsetY += ((h2-h1) / 20.0);
	 	
		 				if(height_offsetY < 0)
	 					height_offsetY = 0;
		 				
		 				if(height_offsetY > 4.5f)
		 					height_offsetY = 4.5f;
	 	
		 	
						touch1 = touch.position;
						touch2 = touch.position;
					
						rotationDamping = 6;
						heightDamping = 6;
						
					
					
					}
					if(touch.phase == TouchPhase.Ended )
					{
						rotationDamping = 2;
						height = 2;
					}
				}
			}
			 		 
			 
		}
	}
	function LateUpdate () 
	{
	
		// Early out if we don't have a target
		if (!target)
			return;
			
		
	
	 
		// Calculate the current rotation angles
		
		wantedRotationAngle = target.eulerAngles.y +  angle_offsetX ;
		
		
	  	wantedHeight = target.position.y +  height_offsetY ;
	  
		currentRotationAngle = transform.eulerAngles.y;
		currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
	
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
	
		// Convert the angle into a rotation
	 	currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
	
		// Set the height of the camera
		transform.position.y = currentHeight;
		
		// Always look at the target
		transform.LookAt (target);
	}
	
	function OnGUI()
	{
//		GUI.Box(rect1,"");
//		 GUI.Box(rect2,"");
//		GUI.Box(rect3,"");
		
		
	}
	
	
	function   getX(  valX : float) : float
	{
		var  x  : float  = (valX/800)*100;
		return  (x/100)* Screen.width;
	}
	
	function   getY(   valY : float) : float
	{
		 var y  : float = (valY/480)*100;
		return    (y/100)* Screen.height;
	}