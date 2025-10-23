using UnityEngine;

public class EngineSound : MonoBehaviour
{
    [SerializeField] AudioSource engineSound;
    [SerializeField] Rigidbody carRigidbody;
    [SerializeField] float minSpeed = 0.1f;

    void Start()
    {
        engineSound.loop = true;
    }

    void Update()
    {
        float speed = carRigidbody.linearVelocity.magnitude;

        if (speed > minSpeed)
        {
            if (!engineSound.isPlaying)
                engineSound.Play();
        }
        else
        {
            if (engineSound.isPlaying)
                engineSound.Pause();
        }
    }
}
