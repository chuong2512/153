using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {


    GameObject hudCamera;
    Targets targetScriptobj;
    // Use this for initialization
	void Start () {
        hudCamera = GameObject.FindGameObjectWithTag("Hud Camera");
        targetScriptobj = hudCamera.GetComponent<Targets>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        this.transform.gameObject.SetActive(false);
        targetScriptobj.count = targetScriptobj.count + 1;
        targetScriptobj.nexttarget();

    }
}
