using UnityEngine;
using System.Collections;

public class ArrowMovey : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(transform.gameObject,iTween.Hash("x",(transform.localRotation.x),"y",(transform.localRotation.y+2f),"easeType", iTween.EaseType.linear, "loopType", iTween.LoopType.pingPong, "time", 0.5f));
		//iTween.RotateBy(gameObject, iTween.Hash("y", 360.0f, "looptype", iTween.LoopType.loop));

	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
