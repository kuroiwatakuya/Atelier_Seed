using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    public GameObject[] OpeningObj;
   public float flame = 1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            OpeningObj[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        flame += Time.deltaTime;
        OpeningObj[0].SetActive(true);

        if(flame >= 2)
        {
            OpeningObj[1].SetActive(true);
            OpeningObj[0].SetActive(false);
        }
        if(flame >= 4)
        {
            OpeningObj[2].SetActive(true);
            OpeningObj[1].SetActive(false);
        }
        if(flame >= 6)
        {
            OpeningObj[3].SetActive(true);
            OpeningObj[2].SetActive(false);
        }
    }
}
