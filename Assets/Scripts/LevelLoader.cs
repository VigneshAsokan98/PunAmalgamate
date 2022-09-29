using Photon.Pun.Demo.SlotRacer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [SerializeField]
    Texture2D[] Levels;
    Vector3 SpawnPosition;

    public int MaxLevels
    {
        get { return (Levels.Length - 1); }
    }
    public ColorToPrefab[] colorMaps;
    int CurrentLevel;
    float interval = 20f;

    GameObject LevelObjects = null;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InitLevel();
    }

    public void InitLevel()
    {        
        CurrentLevel = 0;
        LoadLevel(CurrentLevel);
    }
    public void LoadLevel(int levelidx)
    {
        if (LevelObjects != null)
            Destroy(LevelObjects);

        LevelObjects = new GameObject();
        LevelObjects.name = "LevelElements";
        SpawnPosition = Vector3.zero;
        for (int x = 0; x < Levels[levelidx].width; x++)
        {
            for (int y = 0; y < Levels[levelidx].height; y++)
            {
                LoadPrefab(x, y, levelidx);
                SpawnPosition.z += interval;
            }
            SpawnPosition.z = 0;
            SpawnPosition.x += interval;
        }
    }
    private void LoadPrefab(int x, int y,int levelidx)
    {
        Color pixelColor = Levels[levelidx].GetPixel(x, y);

        if (pixelColor.a == 0)
            return;

        foreach(ColorToPrefab colormap in colorMaps)
        {
            if(colormap.color.Equals(pixelColor))
            {
                Instantiate(colormap.prefab, SpawnPosition, Quaternion.identity, LevelObjects.transform);
            }
        }

    }
}
