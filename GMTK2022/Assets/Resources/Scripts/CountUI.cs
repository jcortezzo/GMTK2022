using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountUI : MonoBehaviour
{
    public TMP_Text currentBallText;
    public TMP_Text maxBallText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentBallText.text = "Balls " + GameCoordinator.Instance.currentBallCount.value;
        maxBallText.text = "Max " + GameCoordinator.Instance.maxBallCount.value;
    }
}
