using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameOver : MonoBehaviour
{
    public GameObject[] StageConditions = new GameObject[CConst.STAGENUM];

    private int StageNum;

    // Start is called before the first frame update
    void Start()
    {

        //クリア時ステージ番号
        StageNum = CGoal.GetNowStage();

        for (int i = 0; i <= CConst.STAGENUM - 1; i++)
        {
            StageConditions[i].SetActive(false);


            //取得ステージと同じなら条件表示
            int Stage = i + 1;
            if (Stage == StageNum)
            {
                StageConditions[i].SetActive(true);
            }
        }

    }
}
