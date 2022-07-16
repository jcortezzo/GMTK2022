using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [field:SerializeField]
    public float Speed { get; private set; }

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bumper bumper = collision.gameObject.GetComponent<Bumper>();
        if (bumper != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                // only if bumper is rotating, otherwise default to Unity physics
                // for the bounce
                if (bumper.IsCharged)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(bumper.BumpForce * point.normal.normalized, ForceMode2D.Impulse);
                }
            }
        }
    }
}
