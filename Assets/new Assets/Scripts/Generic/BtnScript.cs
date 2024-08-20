using UnityEngine;
using System.Collections;

public class BtnScript : MonoBehaviour {


	public Sprite normalBtnSprite;
	public Sprite pressBtnSprite;
	
	private RaycastHit hit;
	private Ray myRay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		myRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (myRay, out hit)) {
			if(Input.GetMouseButtonDown(0) == true && hit.collider.gameObject == transform.gameObject){
				transform.GetComponent<SpriteRenderer>().sprite = pressBtnSprite;
			}
		}
		if(Input.GetMouseButtonUp(0) == true){
			transform.GetComponent<SpriteRenderer>().sprite = normalBtnSprite;
		}
	}
}
