using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCoordinator : MonoBehaviour
{
    public static GameCoordinator Instance;

    public FloatSO maxBallCount;
    public FloatSO currentBallCount;
    public GameConfig gameConfig;

    public BallShooter ballShooter;
    public StaminaBar staminaBar;
    public Player player;
    public CameraShake camShake;

    public GameObject outer;

    public GameObject wallRight;
    public GameObject goalPostRight;

    public GameObject wallLeft;
    public GameObject goalPostLeft;

    public ParticleSystem explosionGate;

    public DieUIController dieController;
    public int Lives { get; private set; }

    public float rotateTime = 1f;

    public float timeElapsed;
    public float TIME_CHANGE = 15;
    
    public int gameStage;

    public float stage5Time = 53.5f;
    public float stage10Time = 100.5f;

    private bool stage0FirstTime = true;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Jukebox.Instance.PlayMusic("mid_crush", timeElapsed);
        dieController.onRollCompleted.AddListener(ChangeDiceBumperAndUI);
        dieController.updatePlayerDice += ChangeBumperNoReset;
        currentBallCount.value = 0;
        maxBallCount.value = 0;
    }

    private int prevStage;
    private float screenShakeTime;
    private float TIME_SHAKE_MAX = 0.333f;
    // Update is called once per frame
    void Update()
    {

        staminaBar.DecreaseStamina(Time.deltaTime * 10);
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    camShake.TriggerSakeRoutine(0.1f, 0.4f, 1);
        //}
        timeElapsed += Time.deltaTime;

        screenShakeTime += Time.deltaTime;
        if(screenShakeTime >= TIME_SHAKE_MAX)
        {
            screenShakeTime = 0;
            camShake.TriggerSakeRoutine(0.1f, 0.1f, 1);
        }
        
        int stage = CalculateStage();
        gameStage = stage;

        if (prevStage != 0 && gameStage == 0)
        {
            // game looped
            if (!gameConfig.InifiniteMode)
            {
                SceneManager.LoadScene("End");

            }
        }


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
        float currentSongTime = Jukebox.Instance.GetMusicCurrentAudioSource().time;
        if (gameStage == 4)
        {
            float deltaStage5 = currentSongTime - stage5Time;
            if (Mathf.Abs(deltaStage5) < 0.01f)
            {
                return 5;
            }
            return 4;
        }
        if (gameStage == 9)
        {
            float deltaStage5 = currentSongTime - stage10Time;
            if (Mathf.Abs(deltaStage5) < 0.01f)
            {
                return 10;
            }
            return 9;
        }
        return (int)(currentSongTime / TIME_CHANGE);
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
        dieController.AnimateDieRoll(3);
        Jukebox.Instance.PlaySFX("roll_dice");
        //StartCoroutine(IESimulateRollDice(3));
    }
    private void ChangeBumperNoReset(int num)
    {
        player.UpdateBumper(num);
    }
    private void ChangeDiceBumperAndUI(int num)
    {
        player.UpdateBumper(num);
        staminaBar.Reset();
        Jukebox.Instance.PlaySFX("roll_dice_finished");
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
        Jukebox.Instance.PlaySFX("screen_rotate");
        player.transform.rotation = Quaternion.identity;
    }

    

    // Right side
    private void Stage5()
    {   
        camShake.TriggerSakeRoutine(0.1f, 0.7f, 1);
        System.Action<ITween<Vector3>> updateScale = (t) =>
        {
            player.transform.localScale = t.CurrentValue;
        };
        if (wallRight.activeSelf)
        {
            Instantiate(explosionGate, wallRight.transform.position, Quaternion.identity).Play();
            
            ballShooter.resetTime = 1.5f;
            Vector3 oldScale = player.transform.localScale;
            Vector3 newScale = new Vector3(5, 5, 5);
            player.gameObject.Tween("Scale up", oldScale, newScale, 0.75f, TweenScaleFunctions.CubicEaseIn, updateScale);

            wallRight.SetActive(false);
            goalPostRight.SetActive(true);
        } else
        {
            // janky hardcoded number
            ballShooter.resetTime = 1.5f;
            Vector3 oldScale = player.transform.localScale;
            Vector3 newScale = new Vector3(5, 5, 5);
            player.gameObject.Tween("Scale down", oldScale, newScale, 0.75f, TweenScaleFunctions.CubicEaseIn, updateScale);

            wallRight.SetActive(true);
            goalPostRight.SetActive(false);
        }
        Jukebox.Instance.PlaySFX("score2");
    }

    // Left side
    private void Stage10()
    {
        camShake.TriggerSakeRoutine(0.1f, 0.7f, 1);
        System.Action<ITween<Vector3>> updateScale = (t) =>
        {
            player.transform.localScale = t.CurrentValue;
        };
        if (wallLeft.activeSelf)
        {
            Instantiate(explosionGate, wallLeft.transform.position, Quaternion.identity).Play();
            ballShooter.resetTime = 0.75f;
            Vector3 oldScale = player.transform.localScale;
            Vector3 newScale = new Vector3(7, 7, 7);
            player.gameObject.Tween("Scale up", oldScale, newScale, 0.75f, TweenScaleFunctions.CubicEaseIn, updateScale);

            wallLeft.SetActive(false);
            goalPostLeft.SetActive(true);
        } else
        {
            // janky hardcoded number
            ballShooter.resetTime = 3;
            Vector3 oldScale = player.transform.localScale;
            Vector3 newScale = new Vector3(2.5f, 2.5f, 2.5f);
            player.gameObject.Tween("Scale down", oldScale, newScale, 0.75f, TweenScaleFunctions.CubicEaseIn, updateScale);

            wallLeft.SetActive(true);
            goalPostLeft.SetActive(false);

        }
        Jukebox.Instance.PlaySFX("score2");
    }
}

