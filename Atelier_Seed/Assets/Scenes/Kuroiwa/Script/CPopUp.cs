using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPopUp : MonoBehaviour
{
    public GameObject Damage_Hint;
    public bool tap = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //=====================================
        //エディター上
        //=====================================
        if(Application.isEditor)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Damage_Hint.SetActive(true);
            }
            if(Input.GetMouseButtonUp(0))
            {
                Damage_Hint.SetActive(false);
            }
        }
        //=====================================
        //実機デバッグ
        //=====================================
        else
        {
            //if (Input.touchCount > 0)
            //{
            //    Touch touch = Input.GetTouch(0);

            //    //タッチしてる間表示
            //    if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            //    {
            //        Damage_Hint.SetActive(true);
            //    }
            //    if(touch.phase == TouchPhase.Ended)
            //    {
            //        Damage_Hint.SetActive(false);
            //    }
            //}
        }
    }

    //======================================================
    //タップしたときの処理
    //======================================================
    void Tap()
    {
        //指一本で触れているとき
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //触った&指を動かしているとき
            if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Damage_Hint.SetActive(true);
            }
        }
    }

    //=====================================================
    //指を離す処理
    //=====================================================
    void Release()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Ended)
            {
                Damage_Hint.SetActive(false);
            }
        }
    }
}
