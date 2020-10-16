using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResult : MonoBehaviour
{

    public GameObject[] NewTrophySprite;
    public GameObject[] TrophySprite;
    public GameObject[] NotTrophySprite;

    public GameObject[] StageConditions;

    public bool[] GetTrophy;
    public bool[] OldGetTrophy;

    private int StageNum;

    // Start is called before the first frame update
    void Start()
    {
        //クリア時ステージ番号
        StageNum = PlayerPrefs.GetInt("STAGENUM", 1);

        for (int i = 0; i <= 14; i++)
        {
            GetTrophy[i] = CSaveBool.GetBool("Trophy" + i, false);
        }
        for (int i = 0; i <= 14; i++)
        {
            OldGetTrophy[i] = CSaveBool.GetBool("OldTrophy" + i, false);
        }

        for (int i = 0; i <= 4; i++)
        {
            StageConditions[i].SetActive(false);
        }

        //取得ステージと同じなら条件表示
        for (int i = 0; i <= 4; i++)
        {
            int Stage = i + 1;
            if (Stage == StageNum)
            {
                StageConditions[i].SetActive(true);
            }
        }

        for (int i = 0; i <= 14; i++)
        {
            if(GetTrophy[i])
            {
                if(OldGetTrophy[i] != GetTrophy[i])
                {
                    NewTrophySprite[i].SetActive(true);
                    TrophySprite[i].SetActive(false);
                    NotTrophySprite[i].SetActive(false);
                    OldGetTrophy[i] = GetTrophy[i];
                }
                else
                {
                    NewTrophySprite[i].SetActive(false);
                    TrophySprite[i].SetActive(true);
                    NotTrophySprite[i].SetActive(false);
                }
            }
            else
            {
                NewTrophySprite[i].SetActive(false);
                TrophySprite[i].SetActive(false);
                NotTrophySprite[i].SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
