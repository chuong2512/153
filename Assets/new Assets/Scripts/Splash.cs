using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Wait());
    }

    void wait()
    {
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        wait();
    }
}