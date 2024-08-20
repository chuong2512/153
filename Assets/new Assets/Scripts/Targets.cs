         using System.Collections;using UnityEngine;


public class Targets : MonoBehaviour {


    public GameObject[] targets;
    public GameObject currentTarget;
    public int count = 0;

    void Start()
    {
        currentTarget = targets[0];
    }
    void Update()
    {
       
        
    }
   public void nexttarget()
    {
       Debug.Log("count =  " + count);
	  count = count + 1;
       if (count == targets.Length )
       {
           currentTarget = targets[targets.Length - 1];
       }
       else
       {
           currentTarget = targets[count-1];
       }
		// for (int i=0; i < targets.Length; i++) {
			// targets[i].SetActive (false);

		// }
    }

}
