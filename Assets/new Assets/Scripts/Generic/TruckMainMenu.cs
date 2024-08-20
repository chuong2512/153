using UnityEngine;
using System.Collections;

public class TruckMainMenu : MonoBehaviour {
	public GameObject target;

	bool flag=true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (flag) {
						transform.Translate (Vector3.forward * Time.deltaTime*10);
						transform.LookAt (target.transform);
				}
	}

	void OnTriggerEnter(Collider col){

		if (col.GetComponent<Collider>().gameObject == target.gameObject) {
			PlayerPrefs.SetInt ("pausetouch", 1);
			flag=false;

		}
	}
}
