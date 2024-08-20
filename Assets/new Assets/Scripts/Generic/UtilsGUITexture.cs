using UnityEngine;
using System.Collections;

public class UtilsGUITexture : MonoBehaviour {


	public static float DESIGN_SCREEN_WIDTH_PIXELS = 960;
	public static float DESIGN_SCREEN_HEIGHT_PIXELS = 640;


	public static float getRatioOfX(){
		//Debug.Log("Screen Width= " + Screen.currentResolution.width +"Design Resolution Width" +DESIGN_SCREEN_WIDTH_PIXELS);
		return Screen.currentResolution.width/DESIGN_SCREEN_WIDTH_PIXELS;
	}

	public static float getRatioOfY(){
		//Debug.Log("Screen Heihgt= " + Screen.currentResolution.height +"Design Resolution Heihgt" +DESIGN_SCREEN_HEIGHT_PIXELS);
		return Screen.currentResolution.height/DESIGN_SCREEN_HEIGHT_PIXELS;
	}

	public static float getScaledWidth(float value){
		return value/DESIGN_SCREEN_WIDTH_PIXELS;
	}
	public static float getScaledHeight(float value){
		return value/DESIGN_SCREEN_HEIGHT_PIXELS;
	}

		
	
	public static float getScaledPositionX(float value){
		return value/Screen.currentResolution.width;
	}
	public static float getScaledPositionY(float value){
		return value/Screen.currentResolution.height;
	}
	
//	public GameObject getGUITextureObj(GameObject GUIObj){
//		GUIObj.transform.guiTexture.pixelInset = new Rect(0,0,GUIObj.transform.guiTexture.pixelInset.width*getRatioOfY(),
//		GUIObj.transform.guiTexture.pixelInset.height*getRatioOfX());
//		return GUIObj;
//	}

}
