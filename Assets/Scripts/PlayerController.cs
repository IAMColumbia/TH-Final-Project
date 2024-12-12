using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    [SerializeField]
    private float _speed = 5.0f;

    public bool _hasPowerup;
    private float _powerupStrength = 15.0f;
    public GameObject PowerupIndicator;

    private Coroutine powerupCountdownCoroutine;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * _speed * Time.deltaTime);
        PowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))  // Make sure this matches exactly with your tag
        {
            Debug.Log("Powerup collected");
            _hasPowerup = true;
            PowerupIndicator.gameObject.SetActive(true);

            // Play power-up sound using a temporary GameObject
            AudioSource powerupAudioSource = other.GetComponent<AudioSource>();
            if (powerupAudioSource != null)
            {
                Debug.Log("AudioSource found on power-up. Clip length: " + powerupAudioSource.clip.length);

                GameObject tempAudio = new GameObject("TempAudio");
                AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
                tempAudioSource.clip = powerupAudioSource.clip;
                tempAudioSource.volume = powerupAudioSource.volume; // Match the volume
                tempAudioSource.Play();
                Debug.Log("Playing power-up sound");
                Destroy(tempAudio, powerupAudioSource.clip.length);
            }
            else
            {
                Debug.LogWarning("AudioSource component not found on power-up");
            }

            Destroy(other.gameObject);

            // Stop the previous powerup countdown if there is one
            if (powerupCountdownCoroutine != null)
            {
                StopCoroutine(powerupCountdownCoroutine);
            }

            // Start a new powerup countdown
            powerupCountdownCoroutine = StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        _hasPowerup = false;
        PowerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + _hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * _powerupStrength, ForceMode.Impulse);
        }
    }
}
