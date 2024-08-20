using UnityEngine;
using System.Collections;

public class Multires : MonoBehaviour {


	float x , y; 
	SpriteRenderer render ;

	// Use this for initialization
	void Start () {

		x = 800;
		y = 480;
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		render = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>() ;
		Vector3 v1 = Camera.main.WorldToScreenPoint(render.transform.localScale);

		float oldObjectWidth = transform.localScale.x;
		float oldObjectHeight = transform.localScale.y;

		float newObjectWidth = oldObjectWidth *   480 /  800  * screenWidth / screenHeight;
		float newObjectHeight = oldObjectHeight *   800 /  480  * screenWidth / screenHeight;

		transform.localScale = new Vector3(newObjectWidth ,  transform.localScale.y , transform.localScale.z);

		float newObjectPosX = transform.localPosition.x *   480 /  800  * screenWidth / screenHeight;
		transform.localPosition = new Vector3(newObjectPosX , transform.localPosition.y , transform.localPosition.z);
	}
	

}
