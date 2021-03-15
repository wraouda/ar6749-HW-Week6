using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.GetComponent<ASCIILevelLoader>().CurrentLevel++; //change the value of currentLevel
        GameManager.instance.level++; // increases level count
    }
}
