using UnityEngine;
using System.Collections;

public class ArrowMov : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(transform.gameObject,iTween.Hash("x",(transform.localPosition.x),"y",(transform.localPosition.y+0.5f),"easeType", iTween.EaseType.linear, "loopType", iTween.LoopType.pingPong, "time", 0.6f));
		//iTween.RotateBy(gameObject, iTween.Hash("y", 360.0f, "looptype", iTween.LoopType.loop));

	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
