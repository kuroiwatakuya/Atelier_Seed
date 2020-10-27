using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;

public class CEnding : MonoBehaviour
{
    private float flame;
    public GameObject[] Endcard;

    public int NextScene;

    public int FlameCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Endcard[i].SetActive(false);
        }

        flame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        flame += Time.deltaTime;

        if (flame >= 0 && flame < FlameCount)
        {
            Endcard[0].SetActive(true);
        }

        if(flame >= FlameCount && flame < FlameCount*2)
        {
            Endcard[0].SetActive(false);
            Endcard[1].SetActive(true);
        }
        if (flame >= FlameCount * 2 && flame < FlameCount * 3)
        {
            Endcard[1].SetActive(false);
            Endcard[2].SetActive(true);
        }
        if(flame >= FlameCount * 3 && flame < FlameCount * 4)
        {
            Endcard[2].SetActive(false);
            Endcard[3].SetActive(true);
        }
        if(flame >= FlameCount * 4 && flame < FlameCount * 5)
        {
            Endcard[3].SetActive(false);
            Endcard[4].SetActive(true);
        }
        if(flame >= FlameCount * 5)
        {
            Endcard[4].SetActive(false);
            SceneManager.LoadScene(NextScene);
        }
    }
}
