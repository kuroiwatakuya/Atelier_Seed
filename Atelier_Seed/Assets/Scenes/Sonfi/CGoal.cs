using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGoal : MonoBehaviour
{

    public int Now_StageNum;

    public bool Clear_Flag;

    public CPlayerScript PlayerScript;

    //トロフィー獲得済みか
    public bool[] GetTrophy;

    // Start is called before the first frame update
    void Start()
    {
        Clear_Flag = false;

        for (int i = 0; i <= 14; i++)
        {
            GetTrophy[i] = CSaveBool.GetBool("Trophy" + i, false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Clear_Flag)
        {

            //ステージ１クリア条件
            if(Now_StageNum == 1)
            {
                //クリアしたらゲット
                GetTrophy[0] = true;
                //３回以内にクリア
                if(PlayerScript.PlayCount >= 3)
                {
                    GetTrophy[1] = true;
                }
            }
            //ステージ2クリア条件
            if (Now_StageNum == 2)
            {

                if(PlayerScript.StopFieldCount <= 0)
                {
                    GetTrophy[5] = true;
                }

                //３回以内にクリア
                if (PlayerScript.PlayCount >= 3)
                {
                    GetTrophy[4] = true;
                }
            }
            //ステージ3クリア条件
            if (Now_StageNum == 3)
            {
                if(!PlayerScript.BreakFlag)
                {
                    GetTrophy[7] = true;
                }

                //３回以内にクリア
                if (PlayerScript.PlayCount >= 1)
                {
                    GetTrophy[6] = true;
                }
            }

            //PlayerPrefsの解放ステージ数に現在クリアしたステージをいれる
            PlayerPrefs.SetInt("STAGENUM", Now_StageNum);

            for(int i = 0; i <= 14; i++)
            {
                CSaveBool.SetBool("Trophy"+i, GetTrophy[i]);
            }

            //PlayerPrefsをセーブする         
            PlayerPrefs.Save();

            SceneManager.LoadScene("Result");

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {

            Clear_Flag = true;

        }
    }
}
