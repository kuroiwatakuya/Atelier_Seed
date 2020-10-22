using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_test : MonoBehaviour
{
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("押した瞬間");
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("離した瞬間");
                Destroy(test);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("押しっぱなし");
            }
        }
       
    }
}
