using UnityEngine;

public class BikeSound : MonoBehaviour
{
    public Rigidbody rb;

    public AudioSource engineAudio;

    public float minPitch = 0.8f;
    public float maxPitch = 2f;

    public float pitchMultiplier = 0.05f;

    void Update()
    {
        float speed =
            rb.linearVelocity.magnitude;

        float targetPitch =
            minPitch +
            speed * pitchMultiplier;

        targetPitch =
            Mathf.Clamp(
                targetPitch,
                minPitch,
                maxPitch
            );

        engineAudio.pitch =
            Mathf.Lerp(
                engineAudio.pitch,
                targetPitch,
                Time.deltaTime * 5f
            );
    }
}