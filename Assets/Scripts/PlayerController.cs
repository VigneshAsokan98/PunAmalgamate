using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PhotonView photonView;
    public bool isKeyPicked =false;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        Cursor.visible = false;
    }

    private void Update()
    {
        if(!photonView.IsMine)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Camera Disablinggg!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Exit"))
        {
            Debug.Log("Maze Exit");

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            isKeyPicked = true;
        }
    }
}
