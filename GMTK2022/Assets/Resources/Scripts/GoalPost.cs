using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    public CameraShake camShake;
    public ParticleSystem fireExplode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Jukebox.Instance.PlaySFX("goal");
            Instantiate(fireExplode, collision.gameObject.transform.position, Quaternion.identity).Play();
            camShake.TriggerShake(0.2f);
            Destroy(collision.gameObject);
            if (GameCoordinator.Instance.currentBallCount.value > 0)
            {
                GameCoordinator.Instance.currentBallCount.value--;
            }
        }
    }
}
