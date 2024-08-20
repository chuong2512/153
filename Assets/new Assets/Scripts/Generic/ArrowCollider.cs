using UnityEngine;
using System.Collections;

public class ArrowCollider : MonoBehaviour {

	public GameObject targetObjects;
	public Transform[] waypoints; 
	public Transform waypoint; 
	private int WPindexPointer;
	
	// Use this for initialization
	void Start () {
		//waypoints = new Transform[targetObjects.gameObject.transform.childCount];
		//loadTargets();
		
		
		WPindexPointer = 0;
		waypoint = waypoints[0];
	}
	
	// Update is called once per frame
	void Update () {
		//waypoint = waypoints[WPindexPointer];
	}
	
	public void loadTargets(){
		for(int i=0; i < waypoints.Length; i++){
			waypoints[i] = targetObjects.gameObject.transform.GetChild(i);
		}
	}
	
	void OnTriggerEnter(Collider obj){ 
		//Debug.Log("OnTrigerEnter :-)" + obj.gameObject.name);
		if(obj.GetComponent<Collider>().gameObject == waypoint.gameObject){
			//Debug.Log("Target Changed");
			obj.GetComponent<Collider>().gameObject.SetActive(false);
			WPindexPointer++;  
			if (WPindexPointer >= waypoints.Length){
				WPindexPointer = 0; 
			}
			waypoint = waypoints[WPindexPointer];
		}
	}
}
//