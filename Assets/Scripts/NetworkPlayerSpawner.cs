using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject SpawnedPlayerPrefab;

    public GameObject xrOrigin;

    public Transform PCspawnpoint;
    public Transform VRspawnpoint;

    public GameSetting settings;
    private void Start()
    {
        if (settings.Build_platform == GameSetting.Platform.PC)
            xrOrigin.SetActive(false);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (settings.Build_platform == GameSetting.Platform.PC)
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("PCPlayer", PCspawnpoint.position, transform.rotation);
        else
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("VRPlayer", VRspawnpoint.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(SpawnedPlayerPrefab);
    }
}
