using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;  
using System.Xml;
using System.Xml.Serialization;


public class vg_interstitial : MonoBehaviour {
	public static string GBundleId;
	public static string admobAdsid;
	public static string unityAdsid;
	public static string FBAdsid;
	public static int showadcompany = 1;
	public static string GAndroidPushNotId;
	public string BundleId;
	public string BundleIdIos;
	public string AdmobId;
	public string AdmobIdIos;
	[HideInInspector]
	public string UnityId;
	[HideInInspector]
	public string UnityIdIos;
	[HideInInspector]
	public string FBAdCode;
	public string AndroidPushNotId;
	Texture2D bannerimg;
	public Image instImage;
	public GameObject instcanvas;
	private string blink;
	private string spotid;
	private string adid;
	public static string houseadslink;
	public static string houseadslinkbanner;
	public static string[] addcomarray;
	public static int adcounter;

	void Awake() {
		#if UNITY_IOS
	//	unityAdsid = UnityIdIos;
		admobAdsid = AdmobIdIos;
		BundleId = BundleIdIos;
		houseadslink = "http://androidhaios.vascogames.com";
		houseadslinkbanner = "http://madsios.vascogames.com";
		#else
		GAndroidPushNotId = AndroidPushNotId;
	//	unityAdsid = UnityId;
		admobAdsid = AdmobId;
		houseadslink = "http://androidha.vascogames.com";
		houseadslinkbanner = "http://androidha.vascogames.com";
		#endif
		FBAdsid = FBAdCode;
		GBundleId = BundleId;
//		Dictionary<string, string> extras =  new Dictionary<string, string>();
//		NativeNotification.CreateNotification(123, null, "http://icons.iconarchive.com/icons/mazenl77/I-like-buttons-3a/512/Cute-Ball-Go-icon.png", "I need you driver!","Help me!", extras, false, new Color(0,0,0,0), true, "http://www.ctv.es/RECURSOS/SONIDOS/wav/ayaaah.wav", true,10);
		//FB.Init(SetInit,OnHideUnity);
	}

	private void SetInit()                                                                       
	{                                                                                            
		//FB.ActivateApp();                                                                  
	}       

	private void OnHideUnity(bool isGameShown)                                                   
	{                                                                                            
	                                                           
		if (isGameShown)                                                                        
		{                                                                                                                                                                               
			//FB.ActivateApp();                                                                
		}                                                                                        
	}  

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	//	instcanvas.SetActive(false);
		System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
		if(Screen.orientation == ScreenOrientation.LandscapeLeft) {
			instImage.rectTransform.sizeDelta = new Vector2(1024 * 0.96f,768 * 0.96f);
			instcanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1024,768);
			//instcanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
			//0.62
		} else {
			instImage.rectTransform.sizeDelta = new Vector2(768 * 0.96f,1024 * 0.96f);
			instcanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(768,1024);
			//instcanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
		}
		instcanvas.SetActive(false);
		if(PlayerPrefs.GetInt("vginstsecondtime") == 1 && cur_time > PlayerPrefs.GetInt("vginstshowtimeout")) {
		StartCoroutine(loadbanner());
		} else {
			PlayerPrefs.SetInt("vginstsecondtime",1);
			StartCoroutine(installcheck());
			//closenow();
		}

	}

	private bool isAppInstalled(string bundleID) {
		#if UNITY_EDITOR
		return false;
		#elif UNITY_IOS
		return false;
		#elif UNITY_ANDROID
		AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
		Debug.Log(" ********LaunchOtherApp " + bundleID);
		AndroidJavaObject launchIntent = null;
		//if the app is installed, no errors. Else, doesn't get past next line
		try{
			launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleID);
			//        
			//        ca.Call("startActivity",launchIntent);
		}catch(Exception ex){
			Debug.Log("exception"+ex.Message);
		}
		if(launchIntent == null)
			return false;
		return true;
		#endif
	}
	
	public void clicked() {
		print ("click");
		StartCoroutine(makeclick());
	}


	public void closenow() {
		Time.timeScale = 1f;
		StartCoroutine(removenowdelay());
	}

	IEnumerator removenowdelay()
	{
		yield return new WaitForSeconds(0.05f);
		instcanvas.SetActive(false);
		Destroy(gameObject);

	}

	IEnumerator makeclick()
	{
		
		WWW www = new WWW(houseadslink + "/loadbanner.php?clicked=1&spotid=" + spotid + "&adid=" + adid + "&bid=" + BundleId + "&deviceid=" + SystemInfo.deviceUniqueIdentifier);
		yield return www;
		
		Application.OpenURL(blink);
		closenow();
		
		
	}

	IEnumerator installcheck()
	{
		
		WWW www = new WWW(houseadslink + "/loadbanner.php?installcheck=1&spotid=" + spotid + "&adid=" + adid + "&bid=" + BundleId + "&deviceid=" + SystemInfo.deviceUniqueIdentifier);
		yield return www;
		XmlDocument	doc= new XmlDocument();
		doc.LoadXml(www.text);
		XmlNodeList bannerinfo = doc.SelectNodes("banner");
		adcounter = int.Parse(bannerinfo[0].SelectSingleNode("adcounter").InnerXml);
		admobAdsid = bannerinfo[0].SelectSingleNode("adadmobid").InnerXml;
		unityAdsid = bannerinfo[0].SelectSingleNode("adunityid").InnerXml;
		FBAdsid = bannerinfo[0].SelectSingleNode("adfbid").InnerXml;
		addcomarray = bannerinfo[0].SelectSingleNode("showadcompany").InnerXml.Split(new[]{","}, System.StringSplitOptions.None);
		showadcompany = int.Parse(addcomarray[0]);
		closenow();
	}

	IEnumerator impressionok()
	{
		
		WWW www = new WWW(houseadslink + "/loadbanner.php?impressieok=1&spotid=" + spotid + "&adid=" + adid + "&bid=" + BundleId + "&deviceid=" + SystemInfo.deviceUniqueIdentifier);
		yield return www;
	}
	
	public IEnumerator loadbanner(int start = 0)
	{

		WWW www = new WWW(houseadslink + "/loadbanner.php?load=" + start + "&bid=" + BundleId + "&deviceid=" + SystemInfo.deviceUniqueIdentifier);
		yield return www;

		XmlDocument	doc= new XmlDocument();
		doc.LoadXml(www.text);
		XmlNodeList bannerinfo = doc.SelectNodes("banner");
		addcomarray = bannerinfo[0].SelectSingleNode("showadcompany").InnerXml.Split(new[]{","}, System.StringSplitOptions.None);
		showadcompany = int.Parse(addcomarray[0]);
		admobAdsid = bannerinfo[0].SelectSingleNode("adadmobid").InnerXml;
		unityAdsid = bannerinfo[0].SelectSingleNode("adunityid").InnerXml;
		FBAdsid = bannerinfo[0].SelectSingleNode("adfbid").InnerXml;
		adcounter = int.Parse(bannerinfo[0].SelectSingleNode("adcounter").InnerXml);
		if(bannerinfo[0].SelectSingleNode("packed").InnerText == "zero") {
			StartCoroutine(installcheck());
		}
		else if(!isAppInstalled(bannerinfo[0].SelectSingleNode("packed").InnerText)) {

			WWW wwwimg = new WWW(bannerinfo[0].SelectSingleNode("image").InnerText);
			bannerimg = new Texture2D(300, 250, TextureFormat.RGB24, false);
			
			yield return wwwimg;
			//www.LoadImageIntoTexture(bannerimg);
			wwwimg.LoadImageIntoTexture(bannerimg);
			Sprite imagespr = Sprite.Create(bannerimg, new Rect(0, 0, bannerimg.width, bannerimg.height), new Vector2(0.5f, 0.5f));

			instImage.sprite = imagespr;
			instcanvas.SetActive(true);
			Time.timeScale = 0f;
			spotid = bannerinfo[0].SelectSingleNode("spotid").InnerText;
			adid = bannerinfo[0].SelectSingleNode("adid").InnerText;
			blink = bannerinfo[0].SelectSingleNode("link").InnerText;

			System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
			int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
			PlayerPrefs.SetInt("vginstshowtimeout" , cur_time + (int.Parse(bannerinfo[0].SelectSingleNode("showtimeout").InnerText) * 60));




				//showtimeout
			StartCoroutine(impressionok());
			//tempmat.mainTexture = bannerimg;

		} else {
			yield return new WaitForSeconds(0.2f);
			StartCoroutine(loadbanner(start + 1));
		}

	}
}
