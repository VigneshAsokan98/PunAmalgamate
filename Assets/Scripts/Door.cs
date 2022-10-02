using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Door : MonoBehaviour
{
    public TextMeshProUGUI PromptUI;

    private void Start()
    {
        //PromptUI = GameObject.FindGameObjectWithTag("Prompt").GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerController>().isKeyPicked)
            {
                other.GetComponent<PlayerController>().isKeyPicked = false;
                OpenDoor();
            }
            else
                PromptUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PromptUI.gameObject.SetActive(false);
        }
    }

    private void OpenDoor()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Animation>().Play();
    }
}
