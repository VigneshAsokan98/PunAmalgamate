using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    int SceneIdx = 1;
    public GameSetting gameSetting;
    public GameObject VRrig;
    public Camera PCcamera;
    public Camera VRcamera;
    public Canvas canvas;

    private void Start()
    {
        SceneIdx = PlayerPrefs.GetInt("CurrentLevelIdx", 1);

        if (gameSetting.Build_platform == GameSetting.Platform.PC)
        {
            VRrig.SetActive(false);
            PCcamera.gameObject.SetActive(true);
            canvas.worldCamera = PCcamera;
        }
        else
        {
            VRrig.SetActive(true);
            PCcamera.gameObject.SetActive(false);
            canvas.worldCamera = VRcamera;
        }

    }
    public void connectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Initialing Connection!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        base.OnConnectedToMaster();
        InitRoom(); 
    }
    public void InitRoom()
    {
        //Loading Scene
        PhotonNetwork.LoadLevel(SceneIdx);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        base.OnJoinedRoom();

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player Joined");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
