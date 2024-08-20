using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	
	public GameObject targetObjects;
	public Transform[] waypoints; 
	private Transform waypoint; 
	private int WPindexPointer;
	
	// Use this for initialization
	void Start () {
		waypoints = new Transform[10];
		loadTargets();
		WPindexPointer = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		waypoint = waypoints[WPindexPointer];
		transform.LookAt(waypoint);
	}
	
	
	
//	void OnCollisionEnter(Collision collision){
//		Debug.Log(""+ collision.gameObject.tag);
//	}
	
	public void loadTargets(){
		for(int i=0; i < 10; i++){
			Debug.Log(" Object Num = " + i);
			waypoints[i] = targetObjects.gameObject.transform.GetChild(i);
		}
	}
	
	void OnTriggerEnter (Collider other){ 
		if(other.GetComponent<Collider>().gameObject.tag == "Target"){
			other.GetComponent<Collider>().gameObject.SetActive(false);
			WPindexPointer++;  
			if (WPindexPointer >= waypoints.Length){
				WPindexPointer = 0; 
			}
			
		}
	}
}
