using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestion : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject hintIcon;

    public GameObject UIPanel;

    public bool playerIsClose;
    private PlayerController thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && playerIsClose)
        {
            dialoguePanel.SetActive(true);
            thePlayer.canMove = false;
            UIPanel.SetActive(false);
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
        }
    }

    public void Resume()
    {
        dialoguePanel.SetActive(false);
        thePlayer.canMove = true;
        UIPanel.SetActive(true);
    }
}
