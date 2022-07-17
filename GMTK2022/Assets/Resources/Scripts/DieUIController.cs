using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieUIController : MonoBehaviour
{
    private BumperZone bumperZone;


    public UnityEvent onRollCompleted;

    [SerializeField]
    private float secondsBetweenRolls;

    // Start is called before the first frame update
    void Start()
    {
        bumperZone = GetComponentInChildren<BumperZone>();
    }

    // Update is called once per frame
    void Update()
    {
        int n = 0;
        for (var key = KeyCode.Alpha1; key <= KeyCode.Alpha6; key++)
        {
            if (Input.GetKey(key))
            {
                n = key - KeyCode.Alpha1 + 1;
                break;
            }
        }
        if (n != 0) bumperZone.GenerateBumperFormation(n);
        if (Input.GetKey(KeyCode.Alpha7)) AnimateDieRoll(3f);
    }

    public void AnimateDieRoll(float time)
    {
        StartCoroutine(IEAnimateDieRoll(time));
    }

    private IEnumerator IEAnimateDieRoll(float time)
    {
        for (; time > 0; time -= Time.deltaTime)
        {
            bumperZone.GenerateBumperFormation(RollDice.Roll());
            yield return new WaitForSeconds(secondsBetweenRolls);
        }
        // event should invoke() the UI and the player
        // to agree what face they should show, and facilitate
        // some small animation to player (just like the UI
        // and player glowing for a second or something) to
        // signify the change
        onRollCompleted.Invoke();
    }
}
