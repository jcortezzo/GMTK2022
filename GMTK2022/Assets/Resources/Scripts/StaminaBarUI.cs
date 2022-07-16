using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    public Image bg;
    public Image fg;
    public StaminaBar staminaBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fg.fillAmount = staminaBar.CurrentPercentage;
    }
}
