using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextSceneLoad;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void nextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            Debug.Log("End");
        }
        else
        {
            SceneManager.LoadScene(nextSceneLoad);

            if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }

    public void unlockLevel()
    {
        PlayerPrefs.SetInt("levelAt", nextSceneLoad);
    }
}
