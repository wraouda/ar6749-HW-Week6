using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            GetComponent<AudioSource>().Play();
            GameManager.instance.GetComponent<ASCIILevelLoader>().ResetPlayer();
            GameManager.instance.lives--;
        }
    }
}
