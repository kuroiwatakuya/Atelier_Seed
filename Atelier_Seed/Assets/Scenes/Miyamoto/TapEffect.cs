using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffect : MonoBehaviour
{
    private GameObject TapEffectObject;
    
    // Start is called before the first frame update
    void Start()
    {
        TapEffectObject = (GameObject)Resources.Load("TapEffect_RandS");
    }

    // Update is called once per frame
    void Update()
    {

        // マウスカーソル位置取得        
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 MousePos = Input.mousePosition;
            MousePos.z = 5.0f;

            Vector3 EffectPos = Camera.main.ScreenToWorldPoint(MousePos);


            Instantiate(TapEffectObject, new Vector3(EffectPos.x, EffectPos.y, EffectPos.z), Quaternion.identity);
        }
    }
}
