using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CResult : MonoBehaviour
{
    public GameObject[] TrophySprite = new GameObject[CConst.TROPHY_STAGE];
    public GameObject[] NewTrophySprite = new GameObject[CConst.TROPHY_STAGE];
    public GameObject[] NotTrophySprite = new GameObject[CConst.TROPHY_STAGE];

    public GameObject[] StageConditions = new GameObject[CConst.STAGENUM];
    public bool[] GetTrophy = new bool[CConst.TROPHY_MAX];
    public bool[] OldGetTrophy = new bool[CConst.TROPHY_MAX];

    private int StageNum;

    private int Score;
    private int CoinNum;
    private int CountNum;
    public GameObject[] ScoreSprite1000 = new GameObject[CConst.NUMBER];
    public GameObject[] ScoreSprite100 = new GameObject[CConst.NUMBER];
    public GameObject[] ScoreSprite10 = new GameObject[CConst.NUMBER];
    public GameObject[] ScoreSprite1 = new GameObject[CConst.NUMBER];

    public GameObject[] Button = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {

        //クリア時ステージ番号
        StageNum = PlayerPrefs.GetInt("STAGENUM", 1);

        for (int i = 0; i <= CConst.TROPHY_MAX - 1; i++)
        {
            GetTrophy[i] = CSaveBool.GetBool("Trophy" + i, false);
            OldGetTrophy[i] = CSaveBool.GetBool("OldTrophy" + i, false);
        }

        for (int i = 0; i <= CConst.STAGENUM - 1; i++)
        {
            StageConditions[i].SetActive(false);


            //取得ステージと同じなら条件表示
            int Stage = i + 1;
            if (Stage == StageNum)
            {
                StageConditions[i].SetActive(true);
            }

            if(Stage == 5)
            {
                Button[1].SetActive(true);
            }
            else
            {
                Button[0].SetActive(true);
            }
        }

        for (int i = 0; i <= CConst.TROPHY_STAGE - 1; i++)
        {
            TrophySprite[i].SetActive(false);
            NewTrophySprite[i].SetActive(false);
            NotTrophySprite[i].SetActive(false);
        }

        for(int i = CConst.TROPHY_STAGE * (StageNum-1); i <= CConst.TROPHY_STAGE * (StageNum - 1) + 2; i++)
        {
            if (GetTrophy[i])
            {
                if (OldGetTrophy[i] != GetTrophy[i])
                {
                    if (i == CConst.TROPHY_STAGE * (StageNum - 1))
                    {
                        NewTrophySprite[0].SetActive(true);
                    }
                    else if (i == CConst.TROPHY_STAGE * (StageNum - 1) + 1)
                    {
                        NewTrophySprite[1].SetActive(true);
                    }
                    else if (i == CConst.TROPHY_STAGE * (StageNum - 1) + 2)
                    {
                        NewTrophySprite[2].SetActive(true);
                    }
                    OldGetTrophy[i] = GetTrophy[i];
                }
                else
                {
                    if (i == CConst.TROPHY_STAGE * (StageNum - 1))
                    {
                        TrophySprite[0].SetActive(true);
                    }
                    else if (i == CConst.TROPHY_STAGE * (StageNum - 1) + 1)
                    {
                        TrophySprite[1].SetActive(true);
                    }
                    else if (i == CConst.TROPHY_STAGE * (StageNum - 1) + 2)
                    {
                        TrophySprite[2].SetActive(true);
                    }
                }
            }
            else
            {
                if (i == CConst.TROPHY_STAGE * (StageNum - 1))
                {
                    NotTrophySprite[0].SetActive(true);
                }
                else if (i == CConst.TROPHY_STAGE * (StageNum - 1) + 1)
                {
                    NotTrophySprite[1].SetActive(true);
                }
                else if (i == CConst.TROPHY_STAGE * (StageNum - 1) + 2)
                {
                    NotTrophySprite[2].SetActive(true);
                }
            }
        }

        for (int i = 0; i <= 9; i++)
        {
            ScoreSprite1000[i].SetActive(false);
            ScoreSprite100[i].SetActive(false);
            ScoreSprite10[i].SetActive(false);
            ScoreSprite1[i].SetActive(false);
        }

        CoinNum = CGoal.GetCoinNum();
        CountNum = CGoal.GetPlayCount();

        Score = CoinNum * 100 + CountNum * 200;

        int Score1000 = (Score - (Score % 1000)) / 1000;
        int Score100 = ((Score % 1000) - (Score % 100)) / 100;
        int Score10 = (((Score % 1000) % 100) - (Score % 10)) / 10;
        int Score1 = Score % 10;

        for (int i = 0; i <= 9; i++)
        {
            if (i == Score1000)
            {
                ScoreSprite1000[i].SetActive(true);
            }
            if (i == Score100)
            {
                ScoreSprite100[i].SetActive(true);
            }
            if (i == Score10)
            {
                ScoreSprite10[i].SetActive(true);
            }
            if (i == Score1)
            {
                ScoreSprite1[i].SetActive(true);
            }
        }
    }
}
