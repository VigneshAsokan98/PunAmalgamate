using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();   
    }

    private void Update()
    {
        if(!photonView.IsMine)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Camera Disablinggg!");
        }
    }
}
