using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Variables 
    public GameSetting gameSetting;
    public GameObject VRrig;
    public Camera PCcamera;
    public Camera VRcamera;
    public Canvas canvas;

    private void Start()
    {

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
    #endregion
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        base.OnConnectedToMaster();
        PhotonNetwork.AutomaticallySyncScene = true;
        InitRoom(); 
    }
    public void InitRoom()
    {
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
        //Loading Scene
        PhotonNetwork.LoadLevel("Game");

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player Joined");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
