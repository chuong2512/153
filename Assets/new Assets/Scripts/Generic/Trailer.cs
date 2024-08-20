using UnityEngine;
using System.Collections;

public class Trailer : MonoBehaviour {


	public Vector3 centerOfMass;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.gameObject.GetComponent<Rigidbody>().centerOfMass = centerOfMass;
	}
}
