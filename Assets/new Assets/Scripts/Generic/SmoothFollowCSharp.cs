using UnityEngine;
using System.Collections;

public class SmoothFollowCSharp : MonoBehaviour {
	
	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 6.0f;
	// the height we want the camera to be above the target
	public float height = 1.5f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	public GameObject tempObj;
	public float side = 0f;
	private GameObject hudCameraObj;
	private HudCameraScript hudScript;
	// Place the script in the Camera-Control group in the component menu
	[AddComponentMenu("Camera-Control/Smooth Follow")]
	
	
	void Start () 
    {
		hudCameraObj = GameObject.Find("Hud Camera");
		hudScript = (HudCameraScript) hudCameraObj.GetComponent("HudCameraScript");
		PlayerPrefs.SetInt ("selectionOn", 1);
		//PlayerPrefs.SetInt ("water2", 1);
		PlayerPrefs.SetInt ("selection", 0);
		//		distance = 5.0f;
		//		height = 2.0f;
		heightDamping = 3.0f;
		rotationDamping = 3.0f;
		side = 90;
		PlayerPrefs.SetInt ("cam", 0);
		PlayerPrefs.SetInt ("hornStart",0);
		//PlayerPrefs.SetInt ("sus",1);
	}
	void Update()
    {
        //if (PlayerPrefs.GetInt ("hornStart") == 1) 
        //{
        //    Time.timeScale = 1.0f;
        //}

	}
	
	void LateUpdate () {
		if (PlayerPrefs.GetInt ("selectionOn") == 1) 
        {
			//hudScript.isPause=true;
			tempObj = GameObject.FindGameObjectWithTag ("Truck");
			target = tempObj.transform;
			PlayerPrefs.SetInt ("targetfirst", 1);
			//PlayerPrefs.SetInt ("cam", 0);
			float wantedRotationAngle = target.eulerAngles.y + side;
			float wantedHeight = target.position.y + height;
			
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
			
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
			
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;
			
			// Set the height of the camera
			transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
			//side =90;
			// Always look at the target
			transform.LookAt (target);
			Time.timeScale = 0.3f;
			side-= 0.5f;

			height = 0.0f;
			distance = 5.0f;
			hudScript.isPause = true;
			StartCoroutine ("fireView");
		} 
        else 
        {
			hudScript.isPause=false;
			tempObj = GameObject.FindGameObjectWithTag ("Truck");
			target = tempObj.transform;
			//PlayerPrefs.SetInt ("cam", 0);
			// Early out if we don't have a target
			if (!target)
				return;
			
			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y + side;
			float wantedHeight = target.position.y + height;
			
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
			
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
			
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;
			
			// Set the height of the camera
			transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
			
			// Always look at the target
			transform.LookAt (target);
			if (PlayerPrefs.GetInt ("cam") == 0) 
            {
				// transform.position.z = 10.0;
				// the height we want the camera to be above the target
				//  wantedHeight = target.position.y + 44.0;
				heightDamping = 3.0f;
				rotationDamping = 3.0f;
				distance = 6.0f;
				height = 3.0f;
				side= 0f;
				wantedHeight = target.position.y + height;
//				if (PlayerPrefs.GetInt ("selectionOn") == 0) {
//					side = 0f;
//				} else {
//					//side += 0.5f;
//				}
				
				
			}
			if (PlayerPrefs.GetInt ("cam") == 1) 
            {
				// transform.position.z = 10.0;
				// the height we want the camera to be above the target
				//  wantedHeight = target.position.y + 44.0;
				heightDamping = 3.0f;
				rotationDamping = 3.0f;
				distance = 9f;
				height = 5f;
				wantedHeight = target.position.y + height;
//				if (PlayerPrefs.GetInt ("selectionOn") == 0) {
//					side = 0f;
//				} else {
//					side += 0.5f;
//				}
				side=0f;
			}
			if (PlayerPrefs.GetInt ("cam") == 2) 
            {
				// transform.position.z = 10.0;
				// the height we want the camera to be above the target
				//  wantedHeight = target.position.y + 44.0;
				heightDamping = 3.0f;
				rotationDamping = 3.0f;
				distance = 5f;
				height = 2f;
//				if (PlayerPrefs.GetInt ("selectionOn") == 0) {
//					side = 0f;
//				} else {
//					side += 0.5f;
//				}
				side=-60f;
				wantedHeight = target.position.y + height;
			}
			if (PlayerPrefs.GetInt ("cam") == 3) 
            {
				// transform.position.z = 10.0;
				// the height we want the camera to be above the target
				//  wantedHeight = target.position.y + 44.0;
				heightDamping = 3.0f;
				rotationDamping = 3.0f;
				distance = 5f;
				height = 2f;
//				if (PlayerPrefs.GetInt ("selectionOn") == 0) {
//					side = 0f;
//				} else {
//					side += 0.5f;
//				}
				side=120f;
				wantedHeight = target.position.y + height;
			}
			if (PlayerPrefs.GetInt ("cam") == 4) 
            {
				// transform.position.z = 10.0;
				// the height we want the camera to be above the target
				//  wantedHeight = target.position.y + 44.0;
				heightDamping = 3.0f;
				rotationDamping = 3.0f;
				distance = 9.0f;
				height = 3.0f;
				//				if (PlayerPrefs.GetInt ("selectionOn") == 0) {
				//					side = 0f;
				//				} else {
				//					side += 0.5f;
				//				}
				side= 120f;
				wantedHeight = target.position.y + height;
			}
//			if (PlayerPrefs.GetInt ("cam") == 4) {
//				// transform.position.z = 10.0;
//				// the height we want the camera to be above the target
//				//  wantedHeight = target.position.y + 44.0;
//				//target = GameObject.FindGameObjectWithTag("fire").transform;
////				tempObj = GameObject.FindGameObjectWithTag ("fire");
////				target = tempObj.transform;
//				//transform.LookAt (target);
//				
//				distance = 30f;
//				height = 12f;
//				side = 155f;
//				if(PlayerPrefs.GetInt("water2")==2){
//					side += 0.2f;
//				}
				//wantedHeight = target.position.y + height;
				
			//}
		}
	}
	IEnumerator fireView()
    {
		yield return new WaitForSeconds (5.2f);
        hudScript.skipBtn.SetActive(false);
        Time.timeScale = 1;
		PlayerPrefs.SetInt ("selectionOn", 0);
		PlayerPrefs.SetInt ("cam", 0);
		PlayerPrefs.SetInt ("selection",1);
		PlayerPrefs.SetInt ("hornStart",1);
		StopCoroutine ("fireView");
	}
}
