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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bumper bumper = collision.gameObject.GetComponent<Bumper>();
        if (bumper != null)
        {
            // hit a bumper!
            Debug.Log("Hit a bumper!");

            Vector2 collisionPoint = collision.ClosestPoint(transform.position);
            Vector2 collisionNormal = new Vector2(transform.position.x, transform.position.y) - collisionPoint;

            // TODO: Change to AddForce possibly, adjust on game feel
            rb.velocity = collisionNormal.normalized * Speed;


            //// Below is the Jankies API....
            //ContactPoint2D[] contactPoints = new ContactPoint2D[20];  // ??? wtf
            //int contactPointsSize = collision.GetContacts(contactPoints);
            //Debug.Log(contactPointsSize);

            //// only look at first ContactPoint... for now
            //for (int i = 0; i < contactPointsSize; i++)
            //{
            //    ContactPoint2D contactPoint = contactPoints[i];
            //    rb.velocity = contactPoint.normal.normalized * Speed;
            //    break;
            //}
        }
    }
}
