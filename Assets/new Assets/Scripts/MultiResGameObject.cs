using UnityEngine;
using System.Collections;

public class MultiResGameObject : MonoBehaviour {
	
	// Advert size which looks good (May not match i6 intended ad size due to GUI Texture in pre unity 4.5 being bad)
	public int pixelWidth = 155;
	public int pixelHeight = 190;
	
	// Standard resolution used by the dev in the editor
	public int stdResWidth = 480;	
	public int stdResHeight = 320;
	
	// Storage for the GUI Texture
	
	void Awake()
	{
	}
	
	// Change this to Update() if you wish to test in the editor
	void Start() 
	{
		
		// Landscape games use the width, portrait games use the height
		bool isLandscape = (Screen.height > Screen.width) ? false : true;
		
		// Get the screen value we'll be using (Screen.height and width return an int, affecting floats so we declare it as a float here)
		float scrVal = isLandscape ? Screen.width : Screen.height;
		
		// Calculate the new scaled width and height
		float newWidth = pixelWidth * (scrVal / (isLandscape ? stdResWidth : stdResHeight));
		float newHeight = pixelHeight * (scrVal / (isLandscape ? stdResWidth : stdResHeight));
		
	}
	
}
