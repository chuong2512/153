using UnityEngine;
using System.Collections;

public class Utils  {

	public static float   getX( float valX){
		float x  = (valX/800)*100;
		 return  (x/100)* Screen.width;
	}
	
	public static float   getY( float valY){
		 float y  = (valY/480)*100;
		  return    (y/100)* Screen.height;
 	}
 	
	public bool isBackKeyPressed(){
	 	return Input.GetKeyDown(KeyCode.Escape);
	}
	
}
