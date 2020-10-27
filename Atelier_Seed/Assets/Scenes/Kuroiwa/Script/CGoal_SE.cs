using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoal_SE : MonoBehaviour
{
    public AudioClip GoalSE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(GoalSE);
        }
    }
}
