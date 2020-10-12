using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//*********************************
// ステージ選択用スクリプト
//*********************************

public class CStageSelect : MonoBehaviour
{
    public int Liberation_StageNum = 1;   //解放されたステージ

    bool OnlySelect = true;     // キーの多重処理防止

    //ボタン
    public Button Stage1;
    public Button Stage2;
    public Button Stage3;
    public Button Stage4;
    public Button Stage5;

    // Start is called before the first frame update
    void Start()
    {
        Liberation_StageNum = PlayerPrefs.GetInt("SCORE", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Liberation_StageNum >= 2)
        {
            Stage2.interactable = true;
            Stage2.image.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            Stage2.image.color = new Color32(39, 39, 39, 255);
        }

        if (Liberation_StageNum >= 3)
        {
            Stage3.interactable = true;
            Stage3.image.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            Stage3.image.color = new Color32(39, 39, 39, 255);
        }

        if (Liberation_StageNum >= 4)
        {
            Stage4.interactable = true;
            Stage4.image.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            Stage4.image.color = new Color32(39, 39, 39, 255);

        }

        if (Liberation_StageNum >= 5)
        {
            Stage5.interactable = true;
            Stage5.image.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            Stage5.image.color = new Color32(39, 39, 39, 255);

        }
    }

    //**********************************
    // ボタンが呼ばれたら呼び出す関数
    //**********************************
    public void StageSelect(string StageNumber)
    {
        if (OnlySelect == true)
        {
            SceneManager.LoadScene(StageNumber);
            OnlySelect = false;
        }
    }
}
