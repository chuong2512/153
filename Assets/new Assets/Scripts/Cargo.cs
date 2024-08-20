using UnityEngine;
using System.Collections;

public class Cargo : MonoBehaviour {
	public GameObject crash; 
	[HideInInspector] public GameObject hudCameraObj;
	[HideInInspector] public HudCameraScript hudScript;

	// Use this for initialization
	void Start () {
		crash.SetActive (false);
		hudCameraObj = GameObject.Find("Hud Camera");
		hudScript = (HudCameraScript) hudCameraObj.GetComponent("HudCameraScript");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
//		Debug.Log("On Collision Enter = " + other.collider.gameObject.name);
		if(other.gameObject.name == "Hardle" || other.gameObject.name == "Mountain"|| other.gameObject.name == "Road"|| other.gameObject.name == "Bridge"|| other.gameObject.name == "Ground"){
			//Debug.Log("Game Over");
			crash.SetActive(true);
			//hudScript.gameOverFunction();
			StartCoroutine("waitJgr");
		}
	}
	IEnumerator waitJgr(){
		yield return new WaitForSeconds (1.0f);
		hudScript.gameOverFunction();
	}

}
