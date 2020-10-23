using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;

public class CGoal : MonoBehaviour
{

    public int Now_StageNum;
    public int MaxCoin;

    public bool Clear_Flag;

    public CPlayerScript PlayerScript;

    private int Liberation_StageNum;

    public static int PlayCount;
    public static int Coin;

    //トロフィー獲得済みか
    public bool[] GetTrophy = new bool[CConst.TROPHY_MAX];
    public bool[] OldTrophy = new bool[CConst.TROPHY_MAX];

    //コレクション
    public bool[] GetBatch = new bool[CConst.BATCH_NUM];
    public bool[] OldBatch = new bool[CConst.BATCH_NUM];

    //コイン全ゲット
    public bool[] AllCoin = new bool[CConst.STAGENUM];

    private bool EndFrag;

    // Start is called before the first frame update
    void Start()
    {
        Clear_Flag = false;
        EndFrag = false;

        for (int i = 0; i <= CConst.TROPHY_MAX - 1; i++)
        {
            GetTrophy[i] = CSaveBool.GetBool("Trophy" + i, false);
            OldTrophy[i] = GetTrophy[i];
        }
        for (int i = 0; i <= CConst.BATCH_NUM - 1; i++)
        {
            GetBatch[i] = CSaveBool.GetBool("Batch" + i, false);
            OldBatch[i] = GetBatch[i];
        }

        for (int i = 0; i <= CConst.STAGENUM - 1; i++)
        {
            AllCoin[i] = CSaveBool.GetBool("Coin" + i, false);
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

            PlayCount = PlayerScript.PlayCount;
            Coin = PlayerScript.GetCoin;

            //********************
            // トロフィー、コイン
            //********************
            //ステージ１クリア条件
            if (Now_StageNum == 1)
            {
                //クリアしたらゲット
                GetTrophy[0] = true;
                //３回以内にクリア
                if(PlayCount >= 3)
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
                if (PlayCount >= 3)
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
                if (PlayCount >= 1)
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
                if (PlayCount >= 3)
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
                if (PlayCount >= 4)
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

            //***************
            // バッチ
            //***************
            if (PlayerScript.WallFlag)
            {
                GetBatch[0] = true;
            }
            else if (PlayerScript.WallCount >= 2)
            {
                GetBatch[1] = true;
            }
            else if (PlayerScript.Fly)
            {
                GetBatch[2] = true;
            }
            else if (PlayerScript.StopFieldCount >= 1 && Now_StageNum == 1)
            {
                GetBatch[3] = true;
            }
            else if (PlayerScript.StopFieldCount >= 1 && Now_StageNum == 2)
            {
                GetBatch[4] = true;
            }
            else if (PlayerScript.StopFieldCount >= 1 && Now_StageNum == 3)
            {
                GetBatch[5] = true;
            }
            else if (PlayerScript.StopFieldCount >= 1 && Now_StageNum == 4)
            {
                GetBatch[6] = true;
            }
            else if (PlayerScript.Wind)
            {
                GetBatch[7] = true;
            }
            else if (PlayerScript.GunTrophyFlag)
            {
                GetBatch[8] = true;
            }
            else if (PlayerScript.BreakBlockCount >= 1)
            {
                GetBatch[9] = true;
            }
            else if (PlayerScript.GetStageTrophy)
            {
                GetBatch[10] = true;
            }
            else if (Now_StageNum == 1)
            {
                if (!GetBatch[11])
                {

                }
                GetBatch[11] = true;
            }
            else if (Coin >= MaxCoin)
            {
                AllCoin[Now_StageNum - 1] = true;
                if(AllCoin[0]&& AllCoin[1]&& AllCoin[2]&& AllCoin[3]&& AllCoin[4])
                {
                    GetBatch[12] = true;
                }
            }
            else if (Now_StageNum == 5)
            {
                if(!GetBatch[13])
                {

                }
                GetBatch[13] = true;
            }
            /*else if (Now_StageNum == 5)写真とる
            {
                GetBatch[14] = true;
            }*/

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

            for (int i = 0; i <= CConst.BATCH_NUM - 1; i++)
            {
                CSaveBool.SetBool("Batch" + i, GetBatch[i]);
                CSaveBool.SetBool("OldBatch" + i, OldBatch[i]);
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

    public static int GetCoinNum()
    {
        return Coin;
    }
    public static int GetPlayCount()
    {
        return PlayCount;
    }
}
