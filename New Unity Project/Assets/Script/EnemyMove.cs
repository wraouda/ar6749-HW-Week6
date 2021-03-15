using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.up * Mathf.Sin(Time.realtimeSinceStartup); // make the creature go up and down like a sine wave

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            GameManager.instance.GetComponent<ASCIILevelLoader>().ResetPlayer();
            GameManager.instance.lives--;
        }
    }
}
