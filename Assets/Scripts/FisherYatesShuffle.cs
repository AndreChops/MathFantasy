using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FisherYatesShuffle : MonoBehaviour
{
    public int[] valueArray;

    // public Sprite[] spriteSoal;
    // public Image imageSoal;
    // int nomorSoal;

    // public List<int> valueList;
    // public List<string> letterString;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleValue(valueArray);   

        // ShuffleSprite(spriteSoal);
        // imageSoal.sprite = spriteSoal[nomorSoal];

        // ShuffleValueList(valueList);
        // ShuffleStringList(letterString);
    }

    public void ShuffleValue(int[] value)
    {
        for(int i = value.Length - 1; i > 0; i--)
        {
            int random = Random.Range(0, i);
            int tmp = value[i];

            value[i] = value[random];
            value[random] = tmp;
        }
    }

    // public void ShuffleSprite(Sprite[] sprite)
    // {
    //     for(int i = sprite.Length - 1; i > 0 ; i--)
    //     {
    //         int random = Random.Range(0, i);

    //         Sprite tmp = sprite[i];

    //         sprite[i] = sprite[random];
    //         sprite[random] = tmp;
    //     }
    // }

    // public void ShuffleValueList(List<int> value)
    // {
    //     for(int i = value.Count - 1; i > 0; i--)
    //     {
    //         int random = Random.Range(0, i);

    //         int tmp = value[i];

    //         value[i] = value[random];
    //         value[random] = tmp;
    //     }
    // }

    // public void ShuffleStringList(List<string> letter)
    // {
    //     for(int i = letter.Count - 1; i > 0; i--)
    //     {
    //         int random = Random.Range(0, i);

    //         string tmp = letter[i];

    //         letter[i] = letter[random];
    //         letter[random] = tmp;
    //     }
    // }
}
