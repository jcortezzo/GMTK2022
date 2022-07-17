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
            ParticleSystem p = Instantiate(fireExplode, collision.gameObject.transform.position, Quaternion.identity);
            p.Play();
            Destroy(p.gameObject, 5f);
            camShake.TriggerShake(0.2f);
            Destroy(collision.gameObject);
            GameCoordinator.Instance.ballGoalCount.value++;
            if (GameCoordinator.Instance.currentBallCount.value > 0)
            {
                GameCoordinator.Instance.currentBallCount.value--;
            }
        }
    }
}
