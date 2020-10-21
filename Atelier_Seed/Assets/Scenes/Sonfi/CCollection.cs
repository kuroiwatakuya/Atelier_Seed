using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CCollection : MonoBehaviour
{

    public GameObject[] BatchSprite = new GameObject[CConst.BATCH_NUM];
    public GameObject[] NewBatchSprite = new GameObject[CConst.BATCH_NUM];
    public GameObject[] NotBatchSprite = new GameObject[CConst.BATCH_NUM];

    //コレクション
    public bool[] GetBatch = new bool[CConst.BATCH_NUM];
    public bool[] OldGetBatch = new bool[CConst.BATCH_NUM];

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i <= CConst.BATCH_NUM - 1; i++)
        {
            GetBatch[i] = CSaveBool.GetBool("Batch" + i, false);
            OldGetBatch[i] = CSaveBool.GetBool("OldBatch" + i, false);
        }

        for (int i = 0; i <= CConst.BATCH_NUM - 1; i++)
        {
            BatchSprite[i].SetActive(false);
            NewBatchSprite[i].SetActive(false);
            NotBatchSprite[i].SetActive(false);
        }

        for (int i = 0; i <= CConst.BATCH_NUM - 1; i++)
        {
            if (GetBatch[i])
            {
                if (OldGetBatch[i] != GetBatch[i])
                {
                    NewBatchSprite[i].SetActive(true);

                    OldGetBatch[i] = GetBatch[i];
                }
                else
                {
                    BatchSprite[i].SetActive(true);
                }
            }
            else
            {
                NotBatchSprite[i].SetActive(true);
            }

            CSaveBool.SetBool("OldBatch" + i, OldGetBatch[i]);
        }

    }

}
