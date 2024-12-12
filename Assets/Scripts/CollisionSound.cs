using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip _collisionSound; // Assign the collision sound in the Inspector

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on " + gameObject.name);
        }
        else
        {
            audioSource.playOnAwake = false; // Ensure the sound doesn't play on awake
            audioSource.clip = _collisionSound;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")) && audioSource != null)
        {
            if (!audioSource.isPlaying) // Check if the audio source is not already playing
            {
                audioSource.Play();
            }
        }
    }
}
