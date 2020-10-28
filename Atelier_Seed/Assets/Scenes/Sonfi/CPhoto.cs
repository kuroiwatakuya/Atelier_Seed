using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPhoto : MonoBehaviour
{

    public CPlayerScript PlayerScript;
    public CGoal GoalScript;
    
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
        if (collider.gameObject.tag == "Player")
        {
            PlayerScript.StagePhoto[GoalScript.Now_StageNum - 1] = true;
            this.gameObject.SetActive(false);
        }
    }
}
