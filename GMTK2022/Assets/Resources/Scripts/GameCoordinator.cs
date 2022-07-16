using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    public StaminaBar staminaBar;
    public Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.DecreaseStamina(Time.deltaTime * 10);
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
}