using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject SpawnedPlayerPrefab;

    public GameObject xrOrigin;
    public bool isPlayerPC = false;
    private void Start()
    {
        if (isPlayerPC)
            xrOrigin.SetActive(false);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (isPlayerPC)
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("PCPlayer", new Vector3(0, 1, 5), transform.rotation);
        else
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("VRPlayer", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(SpawnedPlayerPrefab);
    }
}
