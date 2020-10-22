using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPopUp : MonoBehaviour
{
    public GameObject Damage_Hint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //============================================
    //ヒントをタップ
    //============================================
    public void Tap()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Damage_Hint.SetActive(true);
            }
        }
    }

    //=============================================
    //ヒントから指を離す
    //=============================================
    public void Release()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Damage_Hint.SetActive(false);
            }
        }
    }
}
