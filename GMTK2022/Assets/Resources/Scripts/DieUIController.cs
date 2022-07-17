using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieUIController : MonoBehaviour
{
    private BumperZone bumperZone;
    public UnityEventRollDice onRollCompleted;
    public delegate void UpdatePlayerDice(int num);
    public UpdatePlayerDice updatePlayerDice;

    [SerializeField]
    private float secondsBetweenRolls;

    private void Awake()
    {
        onRollCompleted = new UnityEventRollDice();
    }
    // Start is called before the first frame update
    void Start()
    {
        bumperZone = GetComponentInChildren<BumperZone>();
        bumperZone.GenerateBumperFormation(1);
    }

    // Update is called once per frame
    void Update()
    { 
        //for (var key = KeyCode.Alpha1; key <= KeyCode.Alpha6; key++)
        //{
        //    if (Input.GetKey(key))
        //    {
        //        n = key - KeyCode.Alpha1 + 1;
        //        break;
        //    }
        //}
        //if (Input.GetKey(KeyCode.Alpha7)) AnimateDieRoll(3f);
    }

    public void AnimateDieRoll(float time)
    {
        StartCoroutine(IEAnimateDieRoll(time));
    }

    private IEnumerator IEAnimateDieRoll(float time)
    {
        int roll = 0;
        for (; time > 0; time -= secondsBetweenRolls)
        {
            roll = RollDice.Roll();
            bumperZone.GenerateBumperFormation(roll);
            updatePlayerDice(roll);
            yield return new WaitForSeconds(secondsBetweenRolls);
        }
        // event should invoke() the UI and the player
        // to agree what face they should show, and facilitate
        // some small animation to player (just like the UI
        // and player glowing for a second or something) to
        // signify the change
        onRollCompleted.Invoke(roll);
    }
}

public class UnityEventRollDice : UnityEvent<int>
{

}
