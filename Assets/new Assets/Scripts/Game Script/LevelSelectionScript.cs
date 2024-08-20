using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectionScript : MonoBehaviour
{
    public GameObject imagesObj;
    public GameObject levelBtnObj;

    public GameObject[] LevelImages;
    public GameObject[] Buttons;
    public GameObject leftBtn;
    public GameObject rightBtn;
    public GameObject backBtn;
    public GameObject driveBtn;

    public GameObject lockDialog;
    public GameObject lockDialogOkButton;

    //public GameObject transparentBg;
    public GameObject loadingImage;

    //public AudioClip scrollSound;
    //public AudioClip clickSound;

    Rect touchRect;
    public Texture aTexture;
    int levelNum = 1;
    int unlockLevelNum;
    private RaycastHit hit;
    private Ray myRay;


    bool isMove = false;


    // Use this for initialization
    void Start()
    {
        //LeadBoltAds.LoadAlert();

        //
        //PlayerPrefs.SetInt("UnlockedLevel",20);

        if (PlayerPrefs.GetInt("UnlockedLevel") == 0)
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1);
            unlockLevelNum = PlayerPrefs.GetInt("UnlockedLevel");
        }
        else
        {
            unlockLevelNum = PlayerPrefs.GetInt("UnlockedLevel");
        }

        //LeadBoltAds.LoadInterstitial();

        touchRect = new Rect(Utils.getX(0), Utils.getY(90), Utils.getX(800), Utils.getY(240));
        setLevelDefault();
        unlockLevels(unlockLevelNum);
    }

    void OnGUI()
    {
        //GUI.DrawTexture(touchRect, aTexture);
    }

    // Update is called once per frame
    void Update()
    {
        onScreenTouchObjectTranslateFunction(imagesObj);

        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(myRay, out hit))
        {
            if (Input.GetMouseButtonUp(0) == true)
            {
                myBtnUpFunctions(hit);
            }

            if (Input.GetMouseButtonDown(0) == true)
            {
                myBtnDownFunctions(hit);
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Truck Selection");
        }


        if (imagesObj.transform.position.x > 8.0f)
        {
            iTween.MoveTo(imagesObj,
                iTween.Hash("x", 0.0f, "easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none,
                    "time", 1.0f));
        }

        if (imagesObj.transform.position.x < (21.0f * -3.0f))
        {
            iTween.MoveTo(imagesObj,
                iTween.Hash("x", (19 * -3), "easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none,
                    "time", 1.0f));
        }
    }


    private void levelSelect(int level)
    {
        setLevelDefault();
        levelNum = level + 1;
        Debug.Log("Level Number " + levelNum + " is Clicked ");
        //LevelImages[level].transform.localScale = new Vector3(1.2f,1.2f,1.0f);
        LevelImages[level].GetComponent<SpriteRenderer>().sprite =
            (Sprite)Resources.Load("2D/LevelSelection/Levels/" + levelNum, typeof(Sprite));

        //iTween.ScaleTo(LevelImages[level],iTween.Hash("x",1.5f,"y",1.5f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));

        PlayerPrefs.SetInt("Level", levelNum);
    }


    private void myBtnDownFunctions(RaycastHit mhit)
    {
//		if(isMove == false){
//			if(mhit.collider.gameObject.tag == "Locked"){
//				iTween.MoveTo(lockDialog,iTween.Hash("y",0.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
//			}
//		}
    }

    private void myBtnUpFunctions(RaycastHit mhit)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (mhit.collider.gameObject == Buttons[i].gameObject)
            {
                iTween.MoveTo(imagesObj,
                    iTween.Hash("x", (i * -4), "easeType", iTween.EaseType.easeOutBack, "loopType",
                        iTween.LoopType.none, "time", 1.0f));
                levelSelect(i);
                if (i == 19)
                {
                    iTween.MoveTo(imagesObj,
                        iTween.Hash("x", (18 * -4), "easeType", iTween.EaseType.easeOutBack, "loopType",
                            iTween.LoopType.none, "time", 1.0f));
                }
            }
        }

        if (isMove == false)
        {
//			if(mhit.collider.gameObject.name == "Locked"){
//				//transparentBg.SetActive(true);
//				//transparentBg.animation.Play("transparentBgFaceIn");
//				//iTween.MoveTo(lockDialog,iTween.Hash("y",0,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
//			}
            for (int i = 0; i < LevelImages.Length; i++)
            {
                if (mhit.collider.gameObject == LevelImages[i].gameObject)
                {
                    setLevelDefault();
                    levelNum = i + 1;
                    Debug.Log("Level Number " + levelNum + " is Clicked ");
                    LevelImages[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
                    //LevelImages[i].GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/LevelSelection/Levels/" + levelNum,typeof(Sprite));
                    PlayerPrefs.SetInt("Level", levelNum);
                }
            }
        }

//		if(mhit.collider.gameObject == lockDialog.transform.GetChild(0).gameObject){
//			//transparentBg.animation.Play("transparentBgFaceOut");
//			//iTween.MoveTo(lockDialog,iTween.Hash("y",-5,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
//		}

        if (mhit.collider.gameObject == leftBtn.gameObject)
        {
            iTween.MoveTo(levelBtnObj,
                iTween.Hash("x", 0.0f, "easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none,
                    "time", 1.0f));
            leftBtn.SetActive(false);
            rightBtn.SetActive(true);
        }

        if (mhit.collider.gameObject == rightBtn.gameObject)
        {
            iTween.MoveTo(levelBtnObj,
                iTween.Hash("x", -7.7f, "easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none,
                    "time", 1.0f));
            //iTween.MoveTo(levelBtnObj,iTween.Hash("x",(levelBtnObj.transform.position.x - Utils.getX(9.5f)),"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
            rightBtn.SetActive(false);
            leftBtn.SetActive(true);
        }

        if (mhit.collider.gameObject == backBtn.gameObject)
        {
            SceneManager.LoadScene("Truck Selection");
        }

        if (mhit.collider.gameObject == driveBtn.gameObject)
        {
            if (unlockLevelNum >= levelNum)
            {
                loadingImage.SetActive(true);
                SceneManager.LoadScene("Level" + levelNum);
            }
            else
            {
                iTween.MoveTo(lockDialog,
                    iTween.Hash("y", 0.0f, "easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none,
                        "time", 1.0f));
            }
        }

        if (mhit.collider.gameObject == lockDialogOkButton.gameObject)
        {
            iTween.MoveTo(lockDialog,
                iTween.Hash("y", -5.0f, "easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none,
                    "time", 1.0f));
        }
    }


    private void onScreenTouchObjectTranslateFunction(GameObject targetObj)
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchCheckPosition = Input.GetTouch(0).position;

            if (isInBounds(touchCheckPosition))
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                //if(touchDeltaPosition.x > 0.1 && touchDeltaPosition.x < -0.1 && isMove == false){
                targetObj.transform.Translate((touchDeltaPosition.x * 0.01f), 0.0f, 0.0f);
                //isMove = true;
                //}

                if (touchDeltaPosition.x > 20)
                {
                    iTween.MoveTo(targetObj,
                        iTween.Hash("x", (targetObj.transform.position.x + 10.0f), "easeType",
                            iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
                }

                if (touchDeltaPosition.x < -20)
                {
                    iTween.MoveTo(targetObj,
                        iTween.Hash("x", (targetObj.transform.position.x - 10.0f), "easeType",
                            iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
                }
            }
        }

        isMove = false;
    }


    private void setLevelDefault()
    {
        PlayerPrefs.SetInt("Level", 1);
        for (int i = 0; i < LevelImages.Length; i++)
        {
            int num = i + 1;
            LevelImages[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            LevelImages[i].GetComponent<SpriteRenderer>().sprite =
                (Sprite)Resources.Load("2D/LevelSelection/Levels/0" + num, typeof(Sprite));
        }
    }

    private void unlockLevels(int num)
    {
        for (int i = 0; i < num; i++)
        {
            LevelImages[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    bool isInBounds(Vector2 position)
    {
        Vector2 v = new Vector2(position.x, Screen.height - position.y);
        return touchRect.Contains(v);
    }
}