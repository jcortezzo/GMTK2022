using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform camTransform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private const float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    private const float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    private Coroutine shakeRoutine;

    public static CameraShake Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        if (camTransform == null)
        {
            camTransform = Camera.main.transform;
        }
    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float shakeTime = 2.0f)
    {
        shakeDuration = shakeTime;
    }

    public void TriggerSakeRoutine(float duration, float shakeMagnitude = shakeMagnitude, float dampingSpeed = dampingSpeed)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
            transform.localPosition = initialPosition;
        }
        shakeRoutine = StartCoroutine(ShakeRoutine(duration, shakeMagnitude, dampingSpeed));
    }
    private IEnumerator ShakeRoutine(float duration, float shakeMagnitude, float dampingSpeed) 
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            normalizedTime += (Time.deltaTime / duration) * dampingSpeed;
            yield return null;
        }
        transform.localPosition = initialPosition;
    }
}