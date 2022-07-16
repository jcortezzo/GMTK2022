using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaminaBar : MonoBehaviour
{
    public float max;
    public float CurrentPercentage { 
        get { 
            return currentValue / max; 
        }
    }
    public UnityEvent onStaminaDepleted;

    private float currentValue;

    private void Start()
    {
        Reset();
    }

    public void DecreaseStamina(float value)
    {
        if (value < 0 || currentValue <= 0) return;
        if(currentValue - value >= 0)
        {
            currentValue -= value;
        } else
        {
            currentValue = 0;
        }
        if(currentValue <= 0)
        {
            onStaminaDepleted.Invoke();
            Debug.Log("stamina 0");
        }
    }

    public void Reset()
    {
        currentValue = max;
    }

}
