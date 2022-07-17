using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField]
    public float Speed { get; private set; }

    [field:SerializeField]
    public float SwingSpeed { get; private set; }

    [SerializeField]
    private GameObject BUMPER_PREFAB;

    private BumperZone bumperZone;

    private Rigidbody2D rb;
    private Coroutine swingCoroutine;
    private bool isSwinging = false;

    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;

    public int currentBumper;

    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    // Start is called before the first frame update
    void Start()
    {
        bumperZone = GetComponentInChildren<BumperZone>();
        rb = GetComponent<Rigidbody2D>();

        bumperZone.GenerateBumperFormation(1);
        currentBumper = 1;

        maxX = rightWall.transform.position.x;
        minX = leftWall.transform.position.x;
        maxY = topWall.transform.position.y;
        minY = bottomWall.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Swing();
        Move();
    }

    public void UpdateBumper(int n)
    {
        bumperZone.GenerateBumperFormation(n);
        currentBumper = n;
    }


    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var currX = transform.position.x;
        var currY = transform.position.y;
        if (currX >= maxX)
        {
            horizontal = Mathf.Min(horizontal, 0);
        }
        else if (currX <= minX)
        {
            horizontal = Mathf.Max(horizontal, 0);
        }
        if (currY >= maxY)
        {
            vertical = Mathf.Min(vertical, 0);
        }
        else if (currY <= minY)
        {
            vertical = Mathf.Max(vertical, 0);
        }
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        rb.velocity = Speed * direction;
    }

    void Swing()
    {
       
        bool swingLeft = Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.J);
        bool swingRight = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.K);
        if (swingLeft || swingRight)
        {
            CameraShake.Instance.TriggerSakeRoutine(0.1f, 0.4f, 1);
            if (swingCoroutine == null)
            {
                swingCoroutine = StartCoroutine(swingLeft ? RotateLeft(SwingSpeed) : RotateRight(SwingSpeed));
            }
            else if (!isSwinging)
            {
                StopCoroutine(swingCoroutine);
                swingCoroutine = StartCoroutine(swingLeft ? RotateLeft(SwingSpeed) : RotateRight(SwingSpeed));
            }
        }
    }

    public IEnumerator RotateLeft(float duration)
    {
        isSwinging = true;
        bumperZone.ChargeBumpers();
        yield return Rotate(duration, 90);
        bumperZone.UnchargeBumpers();
        isSwinging = false;
    }

    public IEnumerator RotateRight(float duration)
    {
        isSwinging = true;
        bumperZone.ChargeBumpers();
        yield return Rotate(duration, -90);
        bumperZone.UnchargeBumpers();
        isSwinging = false;
    }

    private IEnumerator Rotate(float duration, float angle)
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
}
