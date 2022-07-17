using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountUI : MonoBehaviour
{
    public TMP_Text currentBallText;
    public TMP_Text maxBallText;
    public TMP_Text goalCountText;

    public GameConfig config;
    public GameObject giveUpButton;
    // Start is called before the first frame update
    void Start()
    {
        if(config.InifiniteMode)
        {
            giveUpButton.SetActive(true);
        } else
        {
            giveUpButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentBallText.text = "Balls " + GameCoordinator.Instance.currentBallCount.value;
        maxBallText.text = "Max " + GameCoordinator.Instance.maxBallCount.value;
        goalCountText.text = "Goals " + GameCoordinator.Instance.ballGoalCount.value;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }
}
