using UnityEngine;
using System.Collections;

public class smoothMainMenu : MonoBehaviour {
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
	
	// Place the script in the Camera-Control group in the component menu
	[AddComponentMenu("Camera-Control/Smooth Follow")]
	
	void Start () {
		PlayerPrefs.SetInt ("selectionOn", 0);
		PlayerPrefs.SetInt ("selection", 1);
		//		distance = 5.0f;
		//		height = 2.0f;
		//		heightDamping = 2.0f;
		//		rotationDamping = 1.0f;
	}
	
	void LateUpdate () {
		
		
		tempObj = GameObject.FindGameObjectWithTag("Truck");
		target = tempObj.transform;
		
		// Early out if we don't have a target
		if (!target) return;
		
		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y+side;
		float wantedHeight = target.position.y + height;
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);
		
		// Always look at the target
		transform.LookAt(target);
		if(PlayerPrefs.GetInt("cam")==0)
		{
			// transform.position.z = 10.0;
			// the height we want the camera to be above the target
			//  wantedHeight = target.position.y + 44.0;
			distance=30f;
			height=12f;
			side=110f;
			wantedHeight = target.position.y + height;
		}
		if(PlayerPrefs.GetInt("cam")==1)
		{
			// transform.position.z = 10.0;
			// the height we want the camera to be above the target
			//  wantedHeight = target.position.y + 44.0;
			distance=35f;
			height= 20f;
			side=0f;
			wantedHeight = target.position.y + height;
		}
		if(PlayerPrefs.GetInt("cam")==2)
		{
			// transform.position.z = 10.0;
			// the height we want the camera to be above the target
			//  wantedHeight = target.position.y + 44.0;
			distance=20f;
			height=10f;
			side=0f;
			wantedHeight = target.position.y + height;
		}
		if(PlayerPrefs.GetInt("cam")==3)
		{
			// transform.position.z = 10.0;
			// the height we want the camera to be above the target
			//  wantedHeight = target.position.y + 44.0;
			distance=25f;
			height=14f;
			side=0f;
			wantedHeight = target.position.y + height;
		}
		if(PlayerPrefs.GetInt("cam")==4)
		{
			// transform.position.z = 10.0;
			// the height we want the camera to be above the target
			//  wantedHeight = target.position.y + 44.0;
			distance=30f;
			height=18f;
			side=80f;
			wantedHeight = target.position.y + height;
		}
	}
}
