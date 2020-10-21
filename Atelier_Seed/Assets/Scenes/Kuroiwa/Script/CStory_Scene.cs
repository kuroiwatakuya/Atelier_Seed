using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//=================================================
//ストーリーシーンスクリプト
//=================================================
public class CStory_Scene : MonoBehaviour
{
    //名前格納
    public string StageName;

    // Start is called before the first frame update
    void Start()
    {
        //2秒後にシーンチェンジ関数を呼ぶ
        Invoke("Scene_Change", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scene_Change()
    {
        SceneManager.LoadScene(StageName);
    }
}
