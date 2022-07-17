using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
public class GameConfig : ScriptableObject
{
    public bool InifiniteMode;

    private void OnEnable()
    {
        InifiniteMode = false;
    }

}
