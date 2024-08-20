using UnityEngine;
using System.Collections;

public class ObjectRotation : MonoBehaviour {

	public float speed = 0.0f;
	
	public bool x = false;
	public bool y = false;
	public bool z = false;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("pausetouch", 0);
	}
	
	// Update is called once per frame
	void Update () {


					if (x == true) {
						if (PlayerPrefs.GetInt ("pausetouch") == 0) {
								transform.Rotate (speed * Time.deltaTime, 0.0f, 0.0f);
						}
						}
						if (y == true) {
								transform.Rotate (0.0f, speed * Time.deltaTime, 0.0f);
						}
						if (z == true) {
								transform.Rotate (0.0f, 0.0f, speed * Time.deltaTime);
						}
				}
	
	}
