using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public GameObject[] OpeningObj;
    private float flame = 0;

    public int NextScene;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            OpeningObj[i].SetActive(false);
        }

        flame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        flame += Time.deltaTime;
        OpeningObj[0].SetActive(true);

        if(flame >= 3)
        {
            OpeningObj[1].SetActive(true);
            OpeningObj[0].SetActive(false);
        }
        if(flame >= 6)
        {
            OpeningObj[2].SetActive(true);
            OpeningObj[1].SetActive(false);
        }
        if(flame >= 9)
        {
            OpeningObj[3].SetActive(true);
            OpeningObj[2].SetActive(false);
        }
        if (flame >= 12)
        {
            OpeningObj[3].SetActive(false);
 //           CFadeManager.FadeOut(NextScene);
            SceneManager.LoadScene(NextScene);
        }
    }
}
