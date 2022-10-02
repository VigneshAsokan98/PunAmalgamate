using Photon.Voice.PUN.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit.Forms;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Asteroids;

public class GameManager : MonoBehaviourPunCallbacks
{

    static public GameManager instance;
    GameObject SpawnedPlayerPrefab;
    PhotonView view;

    Transform PCspawnPoint;
    Transform VRspawnPoint;
    public GameSetting settings;
    int currentLevel = 0;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        view = GetComponent<PhotonView>();
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Lobby")
        {
            PCspawnPoint = GameObject.FindGameObjectWithTag("PCSpawn").transform;
            VRspawnPoint = GameObject.FindGameObjectWithTag("VRSpawn").transform;
            SpawnedPlayerPrefab = SpawnPlayers();
        }
    }

    public void LoadNextLevel()
    {
        PhotonNetwork.Destroy(SpawnedPlayerPrefab);
        view.RPC("LoadLevel", RpcTarget.All);
    }

    [PunRPC]
    void LoadLevel()
    {
        currentLevel++;
        if (currentLevel > LevelLoader.Instance.MaxLevels)
            currentLevel = 0;

        LevelLoader.Instance.LoadLevel(currentLevel);
        SpawnPlayers();
    }

    public GameObject SpawnPlayers()
    {
        if (settings.Build_platform == GameSetting.Platform.PC)
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("PCPlayer", PCspawnPoint.position, Quaternion.identity);
        else
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("VRPlayer", VRspawnPoint.position, Quaternion.identity);

       // Debug.LogError("Player Spawned!!");
        return SpawnedPlayerPrefab;
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(SpawnedPlayerPrefab);
    }
}
