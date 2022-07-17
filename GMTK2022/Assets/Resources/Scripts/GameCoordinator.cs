using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    public StaminaBar staminaBar;
    public Player player;
    public CameraShake camShake;

    public GameObject outer;

    public GameObject wallRight;
    public GameObject goalPostRight;

    public GameObject wallLeft;
    public GameObject goalPostLeft;
    public int Lives { get; private set; }

    public float rotateTime = 1f;

    public float timeElapsed;
    public float TIME_CHANGE = 15;
    
    public int gameStage;

    public float stage5Time = 53.5f;
    void Start()
    {
        Jukebox.Instance.PlayMusic("mid_crush");
    }

    private int prevStage;
    // Update is called once per frame
    void Update()
    {

        staminaBar.DecreaseStamina(Time.deltaTime * 10);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            camShake.TriggerSakeRoutine(0.1f, 0.4f, 1);
        }
        timeElapsed += Time.deltaTime;

        int stage = CalculateStage();
        gameStage = stage;

        if (prevStage != stage)
        {
            switch (stage)
            {
                case 0:
                    break;
                case 5:
                    Stage5();
                    break;
                case 10:
                    Stage10();
                    break;
                default:
                    StageRotate();
                    break;
            }
        }
        prevStage = stage;
    }

    private int CalculateStage()
    {
        if(gameStage == 4)
        {
            float deltaStage5 = timeElapsed - stage5Time;
            if (Mathf.Abs(deltaStage5) < 0.01f)
            {
                return 5;
            }
            return 4;
        }
        return (int)(timeElapsed / TIME_CHANGE);
    }
    public void Hurt()
    {
        Lives = Mathf.Max(Lives - 1, 0);
    }

    public bool IsDead()
    {
        return Lives <= 0;
    }

    public void SimulateRollDice()
    {
        StartCoroutine(IESimulateRollDice(3));
    }

    private IEnumerator IESimulateRollDice(float duration)
    {

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        int diceNum = RollDice.Roll();
        player.UpdateBumper(diceNum);
        staminaBar.Reset();
    }

    private IEnumerator Rotate(Transform transform, float duration, float angle)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, angle) * startRotation;

        for (float t = 0f; t < duration; t += Time.deltaTime / duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
        transform.rotation = endRotation;
    }

    // Rotate 90
    private void StageRotate()
    {
        StartCoroutine(Rotate(outer.transform, rotateTime, 90));
    }

    // Right side
    private void Stage5()
    {
        wallRight.SetActive(false);
        goalPostRight.SetActive(true);
    }

    // Left side
    private void Stage10()
    {
        wallLeft.SetActive(false);
        goalPostLeft.SetActive(true);
    }
}
