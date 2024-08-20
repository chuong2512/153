using UnityEngine;
using System.Collections;
using System;


public class MultiresolutionObject : MonoBehaviour 
{
	 
	public float x,y; 
	SpriteRenderer render ;
	 	
	void Start () {
	
		x = 800;
		y = 480;
		Screen.orientation = ScreenOrientation.AutoRotation;
		
		render = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>() ;
		Vector3 v1 = Camera.main.WorldToScreenPoint( render.transform.localScale);
		
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		
		float oldObjectWidth = transform.localScale.x;
		float oldObjectHeight =   transform.localScale.y;
		
		float newObjectWidth = oldObjectWidth *   480 /  800  * screenHeight / screenWidth;
		float newObjectWidth2 = oldObjectWidth *   480 /  800  * screenWidth / screenHeight; // Correct
		float newObjectWidth3 = oldObjectWidth *   800 /  480  * screenWidth / screenHeight;
		float newObjectWidth4 = oldObjectWidth *   800 /  480  * screenHeight / screenWidth;
		
		float newObjectHeight = oldObjectHeight *   480 /  800  * screenHeight / screenWidth;
		float newObjectHeight2 = oldObjectHeight *   480 /  800  * screenWidth / screenHeight; 
		float newObjectHeight3 = oldObjectHeight *   800 /  480  * screenWidth / screenHeight;// Correct
		float newObjectHeight4 = oldObjectHeight *   800 /  480  * screenHeight / screenWidth;
		
 		
		
 		 // As the Application is Lanscape so this is my observation that the Height scale automatically adjusts on Multiresolution but 
		// we have to adjust the width manually as adjusted below from the obove  Formula i.e; ->  "float newObjectWidth2 = oldObjectWidth *   480 /  800  * screenWidth / screenHeight;"   
 		 
		transform.localScale = new Vector3(newObjectWidth2 ,  transform.localScale.y , transform.localScale.z);
		
		
		// For Multi Res Position
		
		float newObjectPosX = transform.localPosition.x *   480 /  800  * screenWidth / screenHeight; // Correct
		transform.localPosition = new Vector3(newObjectPosX , transform.localPosition.y , transform.localPosition.z);
		
		
 		
		
//		if(transform.guiTexture.texture != null)
//		{ 
//			Rect newInset = new Rect(x,y ,Utils.getX (  transform.guiTexture.texture.width), Utils.getY(transform.guiTexture.texture.height));
//			transform.guiTexture.pixelInset = newInset;
// 		}
		
	}
	
	 
	public static void MultiScaleX(GameObject obj)
	{
		float newObjectPosX = obj.transform.localPosition.x *   480 /  800  * Screen.width / Screen.height; // Correct
		obj.transform.localPosition = new Vector3(newObjectPosX , obj.transform.localPosition.y , obj.transform.localPosition.z);

	}
//	public static float MultiScaleY(float scaley)
//	{
//		
//	}
	
}
