using UnityEngine;
using UnityEngine.InputSystem;

public class EngineSound : MonoBehaviour
{
    [SerializeField] AudioSource engineSoundAcceleration;
    [SerializeField] Rigidbody carRigidbody;

    [SerializeField] float minSpeedStart = 1f;
    [SerializeField] float maxSpeed = 50f;
    [SerializeField] float pitchMultiplier = 1.5f;
    [SerializeField] float volumeMultiplier = 1f;

    public InputActionReference breakingAction;

    void Update()
    {
        float speed = carRigidbody.linearVelocity.magnitude;

        if (speed > minSpeedStart)
        {
            if (!engineSoundAcceleration.isPlaying)
                engineSoundAcceleration.Play();

            float normalizedSpeed = Mathf.InverseLerp(minSpeedStart, maxSpeed, speed);
            engineSoundAcceleration.volume = Mathf.Lerp(0.3f, 1f, normalizedSpeed) * volumeMultiplier;
            engineSoundAcceleration.pitch = Mathf.Lerp(0.8f, pitchMultiplier, normalizedSpeed);
        }
        else
        {
            engineSoundAcceleration.volume = Mathf.Lerp(engineSoundAcceleration.volume, 0f, Time.deltaTime * 5f);
        }
    }
}
