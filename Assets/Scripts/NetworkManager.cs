using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        connectToServer();
    }

    private void connectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Initialing Connection!");

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        base.OnConnectedToMaster();
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
