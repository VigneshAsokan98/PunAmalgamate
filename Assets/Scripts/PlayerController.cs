using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public PhotonView photonView;
    public bool isKeyPicked =false;


    public bool isExitHit = false;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        Cursor.visible = false;
    }

    private void Update()
    {
        if(!photonView.IsMine)
        {
            isExitHit = true;
            transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Camera Disablinggg!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key"))
        {
            Debug.Log("KeyHit");
            OnKeyPicked(other.gameObject);
        }

        if (other.CompareTag("Exit"))
        {
            if(isExitHit)
                return;

            Debug.Log("Maze Exit" + isExitHit);
            isExitHit = true;
            GameManager.instance.LoadNextLevel();
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    private void OnKeyPicked(GameObject _object)
    {
        Destroy(_object);
        isKeyPicked = true;
    }

    public void ResetPosition(Vector3 position)
    {
        Debug.LogError("Player Reset");
        transform.position = position; 
        isExitHit = false;
    }
}
