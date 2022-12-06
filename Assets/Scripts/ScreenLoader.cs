using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    public static int sceneNumber;

    // Start is called before the first frame update
    void Start()
    {
        if (sceneNumber == 0)
        {
            StartCoroutine(ToNextScene());
        }
    }

    IEnumerator ToNextScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        sceneNumber++;
    }
}
