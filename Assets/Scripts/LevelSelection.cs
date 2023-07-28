using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButton;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);

        for(int i = 0; i < levelButton.Length; i++)
        {
            if(i + 1 > levelAt)
            {
                // Debug.Log(PlayerPrefs.GetInt("levelAt"));
                levelButton[i].interactable = false;
                levelButton[i].GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
            }
        }
    }
}
