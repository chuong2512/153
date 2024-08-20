using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ModeSelectionScript : MonoBehaviour
{
    public GameObject carrerButton;
    public GameObject freemodeButton;
    public GameObject backButton;
    public AudioClip clickSound;

    private RaycastHit hit;
    private Ray myRay;


    // Use this for initialization
    void Start()
    {
        //Chartboost.CBBinding.showInterstitial("ModeSelection");
    }

    // Update is called once per frame
    void Update()
    {
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(myRay, out hit))
        {
            if (Input.GetMouseButtonUp(0) == true)
            {
                btnFunctions(hit);
            }
        }


        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    private void btnFunctions(RaycastHit mhit)
    {
        if (mhit.collider.gameObject == carrerButton.gameObject)
        {
            //Handheld.Vibrate();
            //Camera.main.audio.PlayOneShot(clickSound);
            PlayerPrefs.SetInt("Level", 1);
            SceneManager.LoadScene("Truck Selection");
        }

        if (mhit.collider.gameObject == freemodeButton.gameObject)
        {
            //Camera.main.audio.PlayOneShot(clickSound);
            PlayerPrefs.SetInt("Level", 0);
            SceneManager.LoadScene("Truck Selection");
        }

        if (mhit.collider.gameObject == backButton.gameObject)
        {
            //Camera.main.audio.PlayOneShot(clickSound);
            SceneManager.LoadScene("Main Menu");
        }
    }
}