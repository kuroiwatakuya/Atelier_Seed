using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CEnding : MonoBehaviour
{
    public float flame;
    public GameObject[] Endcard;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<5;i++)
        {
            Endcard[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        flame += Time.deltaTime;

        Endcard[0].SetActive(true);

        if(flame >= 3)
        {
            Endcard[0].SetActive(false);
            Endcard[1].SetActive(true);
        }
        if(flame >= 6)
        {
            Endcard[1].SetActive(false);
            Endcard[2].SetActive(true);
        }
        if(flame >= 8)
        {
            Endcard[2].SetActive(true);
            Endcard[3].SetActive(true);
        }
        if(flame >= 10)
        {
            Endcard[3].SetActive(false);
            Endcard[4].SetActive(true);
        }
        if(flame >= 12)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
