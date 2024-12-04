using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float theshold;

    public Vector3 spawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < theshold)
        {
            transform.position = spawnPoint;
            GameManager.lives -= 1;
        }
    }
}
