using UnityEngine;
using System.Collections;

public class rotate_me : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(PlayerPrefs.GetFloat("Rotatespeed"),0f,0f);
	}
}
