using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlQuest : MonoBehaviour
{
    [System.Serializable]
    public class Soal
    {
        [System.Serializable]
        public class ElementSoal
        {
            [TextAreaAttribute]
            public string soal;

            public string[] jawabans;

            public int jawabanBenar;
        }

        public ElementSoal elementSoal;

    }

    public List<Soal> soals;
}
