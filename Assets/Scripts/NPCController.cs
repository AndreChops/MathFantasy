using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject hintIcon;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject continueButton;
    public float wordSpeed;
    public bool playerIsClose;

    private PlayerController thePlayer;

    private bool isCoolDown = false;
    private float coolDown = 1f;

    void Start()
    {
        dialogueText.text = "";
        thePlayer = FindObjectOfType<PlayerController>();
    }
    
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.F) && playerIsClose)
        {
            if(isCoolDown == false)
            {
                if(dialoguePanel.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                    StartCoroutine(CoolDown());
                    thePlayer.canMove = false;   
                }
            }
        }

        if(dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }

        if(playerIsClose)
        {
            hintIcon.SetActive(true);
        }
        else if(!playerIsClose)
        {
            hintIcon.SetActive(false);
        }

    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        thePlayer.canMove = true;
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    IEnumerator CoolDown( )
    {
        isCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isCoolDown = false;
    }

    public void NextLine()
    {
        continueButton.SetActive(false);

        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
