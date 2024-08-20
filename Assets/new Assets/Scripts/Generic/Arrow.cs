using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {


	public GameObject arrowCollider;
	private ArrowCollider arrowColliderScript;
	
	// Use this for initialization
	void Start () {
		arrowColliderScript = (ArrowCollider) arrowCollider.GetComponent("ArrowCollider");
	}
	
	// Update is called once per frame
	void Update () {
	//Debug.Log("LookAt Function" + arrowCollider.gameObject.name);
		transform.LookAt(arrowColliderScript.waypoint);
	}
}
