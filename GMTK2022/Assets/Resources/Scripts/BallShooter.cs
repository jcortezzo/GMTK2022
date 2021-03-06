using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform target;
    [SerializeField] private float force;

    public float resetTime;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeElapsed >= resetTime)
        {
            timeElapsed = 0;
            Kick();
        }
        timeElapsed += Time.deltaTime;
    }

    void Kick()
    {
        Vector2 dir = (target.position - this.transform.position).normalized;
        Ball ball = Instantiate(ballPrefab, this.transform.position, Quaternion.identity).GetComponent<Ball>();
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * force);
        GameCoordinator.Instance.currentBallCount.value++;
        GameCoordinator.Instance.maxBallCount.value = Mathf.Max(GameCoordinator.Instance.maxBallCount.value, GameCoordinator.Instance.currentBallCount.value);
    }

}
