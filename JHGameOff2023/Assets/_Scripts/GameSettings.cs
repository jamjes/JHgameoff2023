//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public bool SOUND_FX = true;
    public bool BG_MUSIC = true;
}
