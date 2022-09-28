using Photon.Voice.PUN.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit.Forms;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Asteroids;

public class GameManager : MonoBehaviour
{

    static public GameManager instance;
    GameObject SpawnedPlayerPrefab;
    PhotonView view;

    Transform PCspawnPoint;
    Transform VRspawnPoint;
    public GameSetting settings;
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
            SpawnPlayers();
        }
    }

    public void LoadNextLevel()
    {
        view.RPC("LoadLevel", RpcTarget.All);
    }

    [PunRPC]
    void LoadLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        currentLevel++;
        if (currentLevel > LevelLoader.Instance.MaxLevels)
            currentLevel = 0;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        //PhotonNetwork.Destroy(SpawnedPlayerPrefab);
        if (settings.Build_platform == GameSetting.Platform.PC)
        {
            PhotonNetwork.Destroy(SpawnedPlayerPrefab);
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("PCPlayer", PCspawnPoint.position, Quaternion.identity);
        }
        LevelLoader.Instance.LoadLevel(currentLevel);
    }

    public GameObject SpawnPlayers()
    {
        if (settings.Build_platform == GameSetting.Platform.PC)
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("PCPlayer", PCspawnPoint.position, Quaternion.identity);
        else
            SpawnedPlayerPrefab = PhotonNetwork.Instantiate("VRPlayer", VRspawnPoint.position, Quaternion.identity);

        return SpawnedPlayerPrefab;
    }
}
