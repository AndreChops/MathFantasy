using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Quest : MonoBehaviour
{
    public TextMeshProUGUI soalText;
    public TextMeshProUGUI[] jawabanTexts;

    public int[] randomSoals; //random soal array
    // public List<int> randomSoalsList; //random soal list

    public int[] randomJawabans; //random jawaban array
    // public List<int> randomJawabansList; //random jawaban list

    public ControlQuest controlQuest;
    int nomerSoal; //0

    public GameObject panelLevelFinished;
    public GameObject panelGameOver;

    // [Header("Soal")]
    // public Image imageSoal;
    // public Button[] buttonJawabans;

    // [Header("Materi")]
    // public GameObject materi; //Panel
    // public Image imageMateri;
    // public Sprite[] spriteMateri; // 0 1 2 3 | max value 4
    // int countMateri;

    [Header("Fisher Yates")]
    public FisherYatesShuffle fisherYates;

    public float totalSoal;

    void Start()
    {
        RandomNomerSoal();
        GenerateQuest();
    }

    void RandomNomerSoal()
    {
        fisherYates.ShuffleValue(randomSoals);
    }

    void RandomNomerJawaban()
    {
        fisherYates.ShuffleValue(randomJawabans);
    }

    void GenerateQuest()
    {
        RandomNomerJawaban();

        soalText.text = controlQuest.soals[randomSoals[nomerSoal]].elementSoal.soal;

        for(int i = 0; i < jawabanTexts.Length; i++)
        {
            jawabanTexts[i].transform.parent.gameObject.SetActive(true);

            jawabanTexts[i].text = controlQuest.soals[randomSoals[nomerSoal]].elementSoal.jawabans[randomJawabans[i]];
        }
    }

    public void ButtonJawabanSoal()
    {
        TextMeshProUGUI currentJawaban = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if(currentJawaban.text == controlQuest.soals[randomSoals[nomerSoal]].elementSoal.jawabans[controlQuest.soals[randomSoals[nomerSoal]].elementSoal.jawabanBenar])
        {
            // Debug.Log(nomerSoal);
            // Debug.Log(totalSoal);
            ButtonNextSoal();
            ScoreManager.instance.AddKillScore();
            if(nomerSoal == totalSoal)
            {
                panelLevelFinished.SetActive(true);
            }
            else
            {
                GenerateQuest();
            }
        }    
        else
        {
            panelGameOver.SetActive(true);
        }

    }

    public void ButtonNextSoal()
    {
        nomerSoal++;
        GenerateQuest();
    }
}

