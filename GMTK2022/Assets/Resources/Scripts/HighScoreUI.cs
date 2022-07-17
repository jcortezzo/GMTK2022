using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    public GameConfig config;
    public TMP_Text titleText;
    public TMP_Text subText;

    public TMP_Text ownGoalText;
    public FloatSO goalSO;

    public TMP_Text highScoreText;
    public FloatSO highScoreSO;

    
    // Start is called before the first frame update
    void Start()
    {
        if(config.InifiniteMode)
        {
            titleText.text = "Gave up already?";
            subText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ownGoalText.text = $"Own Goal : {goalSO.value}";
        highScoreText.text = $"Max Saved : {highScoreSO.value}";
        
    }
}
