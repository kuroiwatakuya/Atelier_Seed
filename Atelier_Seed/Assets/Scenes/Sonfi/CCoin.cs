﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoin : MonoBehaviour
{
    public CPlayerScript PlayerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            PlayerScript.GetCoin++;
            this.gameObject.SetActive(false);
        }
    }
}
