using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField]
    public float Speed { get; private set; }

    [SerializeField]
    private GameObject BUMPER_PREFAB;

    private BumperZone bumperZone;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        bumperZone = GetComponentInChildren<BumperZone>();
        rb = GetComponent<Rigidbody2D>();
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
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        rb.velocity = direction * Time.deltaTime * Speed;
    }
}
