using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 3.0f;
    private Rigidbody _enemyRb;
    private GameObject _player;
    private WaveManager waveManager;

    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();

        if (_enemyRb == null)
        {
            Debug.LogError("Rigidbody component is missing from the enemy.");
        }

        _player = GameObject.FindWithTag("Player");

        if (_player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found. Please ensure there's a GameObject tagged 'Player' in the scene.");
        }

        waveManager = GameObject.FindObjectOfType<WaveManager>();

        if (waveManager == null)
        {
            Debug.LogError("WaveManager not found in the scene.");
        }
    }

    void Update()
    {
        if (_player != null && _enemyRb != null)
        {
            Vector3 lookDirection = (_player.transform.position - transform.position).normalized;
            _enemyRb.AddForce(lookDirection * speed);
        }

        if (transform.position.y < -10)
        {
            if (waveManager != null)
            {
                waveManager.EnemyDefeated();
            }
            Destroy(gameObject);
        }
    }
}
