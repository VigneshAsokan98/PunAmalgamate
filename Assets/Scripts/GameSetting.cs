using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameSettings",menuName = "Setting")]
public class GameSetting : ScriptableObject
{
    public enum Platform { PC, VR}

    public Platform Build_platform;
}
