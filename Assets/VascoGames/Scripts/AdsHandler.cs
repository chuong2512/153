using UnityEngine;
using System.Collections;

public class AdsHandler : MonoBehaviour {
	

	[HideInInspector]
	public ShowAdOnLoad SA;

	// To be Pulled of the Inhosue server .
	public static int Priority_Ad = 1;    // 1 Admob , 2 UnityAds , 3 Own Ad

	private int adccounter = 0;

	float ad_reload_time = 0;

	private bool unityinit = false;
	private bool fbainit = false;
	private bool admobinit = false;
	private int adcountern = 0;
	private bool admobadspreloaded = false;
	private static bool _instanceFound = false;

	// Use this for initialization

	public void change_priority(int num){

		Priority_Ad = num;
		init_ads();
		switch (Priority_Ad)
		{
		case 1:
			prewarm_ads();
			break;
		}
	}

	public static AdsHandler Instance;
	
	void Awake(){
		Instance = this;
	}

	void OnEnable()
	{

	}

	void OnDisable()
	{
		

	}

	void OnReceiveAdInterstitial()
	{
			Debug.Log (this.GetType().ToString() + " - OnReceiveAdInterstitial() Fired.");
		
		/// Your code here...
		admobadspreloaded = true;
	}
	
	/**
	 * Fired when an Interstitial Ad fails to be received.
	 * 
	 *  @param err
	 *          string - The error string
	 */
	void OnFailedToReceiveAdInterstitial(string err)
	{
			Debug.Log (this.GetType().ToString() + " - OnFailedToReceiveAdInterstitial() Fired. Error: " + err);
		
		/// Your code here...
		admobadspreloaded = false;
	}




	IEnumerator Start () {
		DontDestroyOnLoad(transform.parent);
		DontDestroyOnLoad(gameObject);
		SA = GetComponent<ShowAdOnLoad>();
		yield return new WaitForSeconds(1);
		Priority_Ad = vg_interstitial.showadcompany;
		Debug.Log("show company "  + Priority_Ad);
		init_ads();
	}

	public void init_ads(){
		// Pull Priortiy Number from server and Preload the Ad.
		if(Priority_Ad == 2 && !unityinit){ // UnityAds
			Debug.Log("init unity ad with "  + vg_interstitial.unityAdsid);
			#if UNITY_IOS
			
			UH.iosGameID = vg_interstitial.unityAdsid;
			
			#elif UNITY_ANDROID
			
			UH.androidGameID = vg_interstitial.unityAdsid;
			UH.initn();
			#endif
			unityinit = true;
		} else if(Priority_Ad == 4 && !fbainit){ // UnityAds
			
		} else{ // Show Own Ads.
			
			
		}

		if(!admobinit){ // Admob
			Debug.Log("init admob with "  + vg_interstitial.admobAdsid);
			#if UNITY_IOS
			
			AdmobAd.Instance().Init(vg_interstitial.admobAdsid);
			
			#elif UNITY_ANDROID
			
			AdmobAd.Instance().Init(vg_interstitial.admobAdsid);
			
			#endif
			admobinit = true;
			prewarm_ads();
			
		}
	}

	public void nextcompany() 
	{
		if(vg_interstitial.addcomarray == null)
			return;

		if(vg_interstitial.addcomarray.Length > 1) {
			adccounter++;

			if(adccounter >= vg_interstitial.addcomarray.Length) {
				adccounter = 0;
			}

			change_priority(int.Parse(vg_interstitial.addcomarray[adccounter]));

		}
	}

	public void prewarm_ads(){
		StartCoroutine(Delay_ads(ad_reload_time));
	}

	IEnumerator Delay_ads(float T){
		yield return new WaitForSeconds(T);
		if(!admobadspreloaded) {
		} 

		if(Priority_Ad == 4) {
		
		}
		ad_reload_time = 4f;
	}

	public static void Show_Advertizment(){
		Instance.LoadAd();
	}

	public void showadmobdirect(){
		Debug.Log("Admob status " + admobadspreloaded);
		if(admobadspreloaded) {
		}
	}

	public void LoadAd(){
		adcountern++;
		if(vg_interstitial.adcounter <= adcountern) {
			adcountern = 0;
		switch (Priority_Ad)
		{
		case 1:
			show_admob();
			break;
		case 2:
			show_unityads();
			break;
		case 3:
			show_Own_ad();
			break;
		case 4:
				//show_fb();
			break;
		case 5:
				show_page_ad();
			break;
		case 6:
				//no ad
			break;
		default:
			show_admob();
			break;
		}
		}
		nextcompany();
	}
	public void show_admob(){
		Debug.Log("Admob status " + admobadspreloaded);
		if(admobadspreloaded) {
			admobadspreloaded = false;
		}
		prewarm_ads();
	}

	public void show_fb(){
		
		prewarm_ads();
	}

	public void show_page_ad() {
		Instantiate(Resources.Load("vg_ownpagead"));
	}

	public void show_unityads(){

		#if UNITY_IOS || UNITY_ANDROID
		SA.init_unity_ads();
		#endif
	}

	public void show_Own_ad(){
		Instantiate(Resources.Load("VascoGames_ownha"));
	}
}
