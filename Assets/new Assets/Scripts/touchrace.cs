using UnityEngine;
using System.Collections;

public class touchrace : MonoBehaviour
{
    // Use this for initialization
    public Camera Camra;
    private RaycastHit hit;
    private Ray myRay;
    public Touch touch;
    public GameObject Vehicle1;
    public GameObject Vehicle2;
    public GameObject Vehicle3;
    public GameObject Vehicle4;

    int myTouchCount;
    [HideInInspector] public GameObject Vehicle;

    [HideInInspector] public DriveScript driveScript;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("Truck") == 1)
        {
            Vehicle1.SetActive(true);
            Vehicle = Vehicle1;
        }

        if (PlayerPrefs.GetInt("Truck") == 2)
        {
            Vehicle2.SetActive(true);
            Vehicle = Vehicle2;
        }

        if (PlayerPrefs.GetInt("Truck") == 3)
        {
            Vehicle3.SetActive(true);
            Vehicle = Vehicle3;
        }

        if (PlayerPrefs.GetInt("Truck") == 4)
        {
            Vehicle4.SetActive(true);
            Vehicle = Vehicle4;
        }
        //	driveScript =  Vehicle.GetComponent<DriveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.1f);
            driveScript = Vehicle.GetComponent<DriveScript>();
            if (driveScript.engineStart == 0.0f)
            {
                Camra.gameObject.GetComponent<HudCameraScript>().startButtonHelp.SetActive(true);
            }
            else
            {
                Camra.gameObject.GetComponent<HudCameraScript>().startButtonHelp.SetActive(false);
            }

            driveScript.accelerator = Camra.gameObject.GetComponent<HudCameraScript>().direction;
            driveScript.raceOn = true;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            driveScript.accelerator = 0f;
            driveScript.raceOn = false;
            driveScript.releaseBreak();
        }
        
    }
}