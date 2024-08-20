using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour{

	private float nextUsage;
	private float delay;
	
	public float trigerTimer;
	public bool triger;
	public float remaningTime = 10.0f;


	// Use this for initialization
	void Start () {
		delay = 1.0f;
		trigerTimer = 70f;
		triger = false;
		remaningTime = PlayerPrefs.GetFloat("RemaningTime");
	}
	
	// Update is called once per frame
	void Update () {
		if(remaningTime <= 0){
			triger = true;
			remaningTime = trigerTimer;
		}

		if (Time.time > nextUsage && triger == false){
			nextUsage = Time.time + delay;
			remaningTime--;
			PlayerPrefs.SetFloat("RemaningTime",remaningTime);
		}


	}

	public void showCounter(){
		Debug.Log("Current Timer = " + nextUsage);
	}
}
