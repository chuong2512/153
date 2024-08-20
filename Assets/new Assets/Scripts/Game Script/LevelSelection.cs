using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//using ChartboostSDK;

public class LevelSelection : MonoBehaviour {

	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject page1;
	public GameObject page2;
	public GameObject[] Levels;
	public GameObject backButton;
	public GameObject nextButton;
	public GameObject loading;
	public int pageNo;
	private RaycastHit hit;
	private Ray myRay;
	public int levelNum = 1;
	int unlockLevelNum;
	public GameObject lockDialog;
	public GameObject lockDialogOkButton;
	// Use this for initialization
    void Awake()
    {
    }
	void Start () 
	{
        //AddControllerScript.LevelSelectionAds();
		if(PlayerPrefs.GetInt("UnlockedLevel") == 0)
		{
			PlayerPrefs.SetInt("UnlockedLevel",1);
			unlockLevelNum = PlayerPrefs.GetInt("UnlockedLevel");
		}
		else
		{
			unlockLevelNum = PlayerPrefs.GetInt("UnlockedLevel");
		}
        //unlockLevelNum = 20;
		pageNo = 1;
		levelNum = 1;
		unlockLevels(unlockLevelNum);
		//First 4 Unlocked Ended 
	}

   
	// Update is called once per frame
	void Update () 
	{
		myRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (myRay, out hit)) 
		{
			if(Input.GetMouseButtonUp(0))
			{
				buttonUpFunction(hit);
			}
		}
		setButtons();
	}

	private void buttonUpFunction(RaycastHit mhit)
	{
		for(int i = 0; i < Levels.Length; i++)
		{
			if(mhit.collider.gameObject == Levels[i].gameObject)
			{
				setLevelDefault();
				levelNum = i + 1;
				Debug.Log("Level Number " + levelNum  + " is Clicked ");
				//Levels[i].transform.localScale = new Vector3(1.2f,1.2f,1.0f);
				PlayerPrefs.SetInt("Level",levelNum);
				//============== Load Level =======================
				if(unlockLevelNum >= levelNum)
				{
					loading.SetActive(true);
                    StartCoroutine("wait");
				}
				else
				{
					iTween.MoveTo(lockDialog,iTween.Hash("y",0.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
				}
			}
		}

		//===== Left Right Buttons ==================
		if(mhit.collider.gameObject == leftButton.gameObject)
		{
			pageNo = 1;
		}
		if(mhit.collider.gameObject == rightButton.gameObject)
		{
			pageNo = 2;
		}
		if(mhit.collider.gameObject == lockDialogOkButton.gameObject)
		{
			iTween.MoveTo(lockDialog,iTween.Hash("y",-6.0f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		if(mhit.collider.gameObject == backButton.gameObject)
		{
			SceneManager.LoadScene("Truck Selection");
		}
		if(mhit.collider.gameObject == nextButton.gameObject)
		{
			if(unlockLevelNum >= levelNum)
			{
				loading.SetActive(true);
				SceneManager.LoadScene("Level"+levelNum);
			}
			else
			{
				iTween.MoveTo(lockDialog,iTween.Hash("y",0.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
			}
		}
	}

	private void unlockLevels(int num)
	{
		for(int i = 0; i < num; i++)
		{
			Levels[i].transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	private void setLevelDefault()
	{
		PlayerPrefs.SetInt("Level",1);
		for(int i = 0; i < Levels.Length; i++)
		{
			int num = i + 1;
			//Levels[i].transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		}
	}

	public void setButtons()
	{
		if(pageNo == 1)
		{
			page1.SetActive(true);
			page2.SetActive(false);
			leftButton.SetActive(false);
			rightButton.SetActive(true);
		}
		if(pageNo == 2)
		{
			page1.SetActive(false);
			page2.SetActive(true);
			leftButton.SetActive(true);
			rightButton.SetActive(false);
		}
	}
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Level" + levelNum);
        AdsHandler.Instance.LoadAd();
        StopCoroutine("wait");
    }
}