using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    [field: SerializeField]
    public float Speed { get; private set; }

    [field: SerializeField]
    public float SwingSpeed { get; private set; }

    private Rigidbody2D rb;
    private Coroutine swingCoroutine;
    private bool isSwinging = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Swing();
        Move();
    }

    public void UpdateBumper(int n)
    {
    }


    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        rb.velocity = Speed * Time.deltaTime * direction;
    }

    void Swing()
    {
        bool swingLeft = Input.GetKeyDown(KeyCode.Q);
        bool swingRight = Input.GetKeyDown(KeyCode.E);
        if (swingLeft || swingRight)
        {
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
        
        yield return Rotate(duration, 90);

        isSwinging = false;
    }

    public IEnumerator RotateRight(float duration)
    {
        isSwinging = true;

        yield return Rotate(duration, -90);

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
