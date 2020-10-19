using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;

public class CGoal : MonoBehaviour
{

    public int Now_StageNum;

    public bool Clear_Flag;

    public CPlayerScript PlayerScript;

    //トロフィー獲得済みか
    public bool[] GetTrophy = new bool[CConst.TROPHY_MAX];
    public bool[] OldTrophy = new bool[CConst.TROPHY_MAX];

    // Start is called before the first frame update
    void Start()
    {
        Clear_Flag = false;

        for (int i = 0; i <= CConst.TROPHY_MAX - 1; i++)
        {
            GetTrophy[i] = CSaveBool.GetBool("Trophy" + i, false);
            OldTrophy[i] = GetTrophy[i];
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

                if (PlayerScript.GetStageTrophy)
                {
                    GetTrophy[2] = true;
                }
            }
            //ステージ2クリア条件
            if (Now_StageNum == 2)
            {

                if (PlayerScript.GetStageTrophy)
                {
                    GetTrophy[3] = true;
                }
                //３回以内にクリア
                if (PlayerScript.PlayCount >= 3)
                {
                    GetTrophy[4] = true;
                }
                if (PlayerScript.StopFieldCount <= 0)
                {
                    GetTrophy[5] = true;
                }
            }
            //ステージ3クリア条件
            if (Now_StageNum == 3)
            {
                //３回以内にクリア
                if (PlayerScript.PlayCount >= 1)
                {
                    GetTrophy[6] = true;
                }
                if (PlayerScript.BreakBlockCount <= 0)
                {
                    GetTrophy[7] = true;
                }

                if (PlayerScript.GetStageTrophy)
                {
                    GetTrophy[8] = true;
                }
            }
            //ステージ4クリア条件
            if (Now_StageNum == 4)
            {

                if (PlayerScript.GetStageTrophy)
                {
                    GetTrophy[9] = true;
                }
                //5回以内にクリア
                if (PlayerScript.PlayCount >= 3)
                {
                    GetTrophy[10] = true;
                }
                if (PlayerScript.BreakBlockCount >= 2)
                {
                    GetTrophy[11] = true;
                }
            }
            //ステージ5クリア条件
            if (Now_StageNum == 5)
            {
                //4回以内にクリア
                if (PlayerScript.PlayCount >= 4)
                {
                    GetTrophy[12] = true;
                }
                if (PlayerScript.BreakBlockCount <= 0)
                {
                    GetTrophy[13] = true;
                }
                if (PlayerScript.GunTrophyFlag)
                {
                    GetTrophy[14] = true;
                }
            }

            //PlayerPrefsの解放ステージ数に現在クリアしたステージをいれる
            PlayerPrefs.SetInt("STAGENUM", Now_StageNum);

            for(int i = 0; i <= CConst.TROPHY_MAX - 1; i++)
            {
                CSaveBool.SetBool("Trophy"+i, GetTrophy[i]);
            }
            for (int i = 0; i <= CConst.TROPHY_MAX - 1; i++)
            {
                CSaveBool.SetBool("OldTrophy" + i, OldTrophy[i]);
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
