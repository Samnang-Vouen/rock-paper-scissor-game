using UnityEngine;

public class ChoiceRotator : MonoBehaviour
{
    [Tooltip("Max angle left/right in degrees")] public float amplitude = 12f;
    [Tooltip("Speed of oscillation cycles per second")] public float frequency = 2f;
    [Tooltip("Random phase offset so all are not synced")] public bool randomizePhase = true;
    [Tooltip("Stop rotation when disabled externally")] public bool active = true;

    private float phase;
    private Quaternion initialRotation;

    void Awake()
    {
        initialRotation = transform.localRotation;
        if (randomizePhase)
        {
            phase = Random.value * Mathf.PI * 2f;
        }
    }

    void Update()
    {
        if (!active) return;
        float t = Time.time * frequency + phase;
        float angle = Mathf.Sin(t) * amplitude;
        transform.localRotation = initialRotation * Quaternion.Euler(0f, 0f, angle);
    }

    // Optional: call this after a choice is made to freeze the selected icon.
    public void FreezeAtCurrentAngle()
    {
        active = false;
    }

    // Optional: resume rotation.
    public void Resume()
    {
        active = true;
    }
}
