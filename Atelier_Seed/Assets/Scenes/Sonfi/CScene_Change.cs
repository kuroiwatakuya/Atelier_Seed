using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

//*********************************************
// ステージ選択、タイトルへの遷移用スクリプト
//*********************************************

public class CScene_Change : MonoBehaviour
{
    bool OnlySelect = true;     // キーの多重処理防止

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Go_StageSelect()
    {
        if (OnlySelect == true)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Stage_Select");
            OnlySelect = false;
        }
    }

    public void Go_Title()
    {
        if (OnlySelect == true)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Title");
            OnlySelect = false;
        }
    }
    public void Go_Album()
    {
        if (OnlySelect == true)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Collection");
            OnlySelect = false;
        }
    }

}
