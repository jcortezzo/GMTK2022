using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FloatValue", order = 1)]
public class FloatSO : ScriptableObject
{
    public float value;

    private void OnEnable()
    {
        value = 0;
    }

}
