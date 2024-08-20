using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;  
using System.Xml;
using System.Xml.Serialization;

public class vg_adtester : MonoBehaviour {

	void Start () {
	
		
	}

	public void showad() {
		AdsHandler.Instance.LoadAd();
	}

	public void showadmob() {
		AdsHandler.Instance.change_priority(1);
		AdsHandler.Instance.LoadAd();
	}

	public void showunity() {
		AdsHandler.Instance.change_priority(2);
		AdsHandler.Instance.LoadAd();
	}

	public void showownad() {
		AdsHandler.Instance.change_priority(3);
		AdsHandler.Instance.LoadAd();
	}

	public void showpagead() {
		AdsHandler.Instance.change_priority(5);
		AdsHandler.Instance.LoadAd();
	}

	public void showfbad() {
		AdsHandler.Instance.change_priority(4);
		AdsHandler.Instance.LoadAd();
	}

}
