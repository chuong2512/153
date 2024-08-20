/// <summary>
/// ShowAdOnLoad.cs - Written for Unity Ads asset package v1.1.4 (SDK v1.4.2)
///  by Nikkolai Davenport <nikkolai@unity3d.com> 
///
/// A simple example for showing ads on load using the UnityAdsHelper script.
/// </summary>

using UnityEngine;
using System.Collections;

public class ShowAdOnLoad : MonoBehaviour 	
{
	public string zoneID;
	public bool useTimeout = true;
	public float timeoutDuration = 15f;
	public float yieldTime = 0.5f;
	
	private float _startTime = 0f;

	#if UNITY_IOS || UNITY_ANDROID

	public void init_unity_ads()
    {
		StartCoroutine(Start_Ads());
	}

	IEnumerator Start_Ads ()
    {
		string zoneName = string.IsNullOrEmpty(zoneID) ? "the default ad placement zone" : zoneID;
		_startTime = Time.timeSinceLevelLoad;
		while (!UnityAdsHelper.isInitialized)
        {
			/*if (useTimeout && Time.timeSinceLevelLoad - _startTime > timeoutDuration)
            {
			    Debug.LogWarning(string.Format("Unity Ads failed to initialize in a timely manner. " + "An ad for {0} will not be shown on load.",zoneName));
				yield break;	
			}
			yield return new WaitForSeconds(yieldTime);*/
            yield break;
		}

		Debug.Log("Unity Ads has finished initializing. Waiting for ads to be ready...");
		_startTime = Time.timeSinceLevelLoad;
		
		while (!UnityAdsHelper.IsReady(zoneID))
		{
			/*if (useTimeout && Time.timeSinceLevelLoad - _startTime > timeoutDuration)		
			{
				Debug.LogWarning(string.Format("Unity Ads failed to be ready in a timely manner. " + "An ad for {0} will not be shown on load.",zoneName));	
				yield break;
			}
			yield return new WaitForSeconds(yieldTime);*/
            yield break;
		}

		Debug.Log(string.Format("Ads for {0} are available and ready. Showing ad now...",zoneName));
		UnityAdsHelper.ShowAd(zoneID);
	}	
	#endif
}