using UnityEngine;
using System.Collections;

public class AnimationEventsScript : MonoBehaviour {
	
	public GameObject progressBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void showProgressBar(){
		progressBar.SetActive(true);
	}
	
	public void hideProgressBar(){
		progressBar.SetActive(false);
	}
}
