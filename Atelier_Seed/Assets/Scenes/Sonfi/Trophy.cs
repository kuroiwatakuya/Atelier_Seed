using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

//**************************
// オプションのトロフィー
//**************************

public class Trophy : MonoBehaviour
{

    //トロフィーのスプライト
    public GameObject[] TrophySprite = new GameObject[CConst.TROPHY_STAGE];
    public GameObject[] NotTrophySprite = new GameObject[CConst.TROPHY_STAGE];

    public CGoal GoalScript;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= CConst.TROPHY_STAGE - 1; i++)
        {
            TrophySprite[i].SetActive(false);
            NotTrophySprite[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i <= CConst.TROPHY_STAGE - 1; i++)
        {
            TrophySprite[i].SetActive(false);
            NotTrophySprite[i].SetActive(false);
        }*/

        for (int i = CConst.TROPHY_STAGE * (GoalScript.Now_StageNum - 1); i <= CConst.TROPHY_STAGE * (GoalScript.Now_StageNum - 1) + 2; i++)
        {
            if (GoalScript.GetTrophy[i])
            {
                if (i == CConst.TROPHY_STAGE * (GoalScript.Now_StageNum - 1))
                {
                    TrophySprite[0].SetActive(true);
                }
                else if (i == CConst.TROPHY_STAGE * (GoalScript.Now_StageNum - 1) + 1)
                {
                    TrophySprite[1].SetActive(true);
                }
                else if (i == CConst.TROPHY_STAGE * (GoalScript.Now_StageNum - 1) + 2)
                {
                    TrophySprite[2].SetActive(true);
                }
            }
            else
            {
                if (i == CConst.TROPHY_STAGE * (GoalScript.Now_StageNum - 1))
                {
                    NotTrophySprite[0].SetActive(true);
                }
                else if (i == CConst.TROPHY_STAGE * (GoalScript.Now_StageNum - 1) + 1)
                {
                    NotTrophySprite[1].SetActive(true);
                }
                else if (i == CConst.TROPHY_STAGE * (GoalScript.Now_StageNum - 1) + 2)
                {
                    NotTrophySprite[2].SetActive(true);
                }
            }
        }
    }
}
