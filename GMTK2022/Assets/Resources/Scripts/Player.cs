using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject BUMPER_PREFAB;

    private BumperZone bumperZone;

    // Start is called before the first frame update
    void Start()
    {
        bumperZone = GetComponentInChildren<BumperZone>();
    }

    // Update is called once per frame
    void Update()
    {
        int n = 0;
        for (var key = KeyCode.Alpha1; key <= KeyCode.Alpha6; key++)
        {
            if (Input.GetKey(key))
            {
                n = key - KeyCode.Alpha1 + 1;
                break;
            }
        }
        if (n != 0) bumperZone.GenerateBumperFormation(n);
    }
}
