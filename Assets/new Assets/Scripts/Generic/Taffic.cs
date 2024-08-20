using UnityEngine;
using System.Collections;

public class Taffic : MonoBehaviour {

	public float speed = 0.0f;
	
	private bool turn = false;
	public GameObject tempobj;
	// Use this for initialization
	void Start () {
		//speed = 2.0f;
		tempobj.transform.rotation = Quaternion.Euler (0,90,0);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(0.0f,0.0f,speed*Time.deltaTime);
		if(turn == true){
			//this.transform.Rotate(0.0f,speed*8*Time.deltaTime,0.0f);
			//this.transform.rotation = Quaternion.Euler (0,90,0);
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation,tempobj.transform.rotation,Time.deltaTime*3);
			//this.transform.rotation = Quaternion.Slerp(this.transform.rotation,tempobj.transform.rotation,Time.deltaTime*20);
		}
		
	}
	
	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.tag == "signal"){
			Debug.Log("Signal Start");
			turn = true;
		}
	}
	
	
	void OnTriggerExit(Collider collsion) {
		if(collsion.gameObject.tag == "signal"){
			Debug.Log("Signal End");
			turn = false;
		}
	}
	
	
//	void OnCollisionEnter(Collision collision){
//		if(collision.gameObject.tag == "signal"){
//			Debug.Log("Signal !");
//			//Handheld.Vibrate();
//		}
//	}


}
