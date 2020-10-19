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
    public int BeforeStage;
    public int Liberation_StageNum = 1;   //解放されたステージ

    bool OnlySelect = true;     // キーの多重処理防止

    //ステージ決定SE変数
    public AudioClip SelectSE;
    AudioSource AudioSource;

    //ボタン
    public Button Stage1;
    public Button Stage2;
    public Button Stage3;
    public Button Stage4;
    public Button Stage5;

    // Start is called before the first frame update
    void Start()
    {
        BeforeStage = PlayerPrefs.GetInt("STAGENUM", 0);
        Liberation_StageNum = PlayerPrefs.GetInt("LIBERATION_STAGENUM", 1);
        if (BeforeStage >= Liberation_StageNum)
        {
            Liberation_StageNum = BeforeStage;
            Liberation_StageNum++;
        }

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

        //ステージ選択SE取得
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //**********************************
    // ボタンが呼ばれたら呼び出す関数
    //**********************************
    public void StageSelect(string StageNumber)
    {
        if (OnlySelect == true)
        {
            //SEを流す処理
            AudioSource.PlayOneShot(SelectSE);

            PlayerPrefs.SetInt("LIBERATION_STAGENUM", Liberation_StageNum);
            SceneManager.LoadScene(StageNumber);
            OnlySelect = false;

            //SEを破壊しないようにする
            DontDestroyOnLoad(SelectSE);
        }
    }
}
