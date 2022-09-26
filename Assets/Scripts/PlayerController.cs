using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PhotonView photonView;

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
}
