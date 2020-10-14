using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayCount : MonoBehaviour
{
    
    public CPlayerScript PlayerScript;

    public GameObject[] PlayCountUI;

    private int PlayCount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 9; i++)
        {
            PlayCountUI[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayCount = PlayerScript.PlayCount;

        if(PlayerScript.PlayCount == PlayCount)
        {
            PlayCountUI[PlayCount].SetActive(true);
            PlayCountUI[PlayCount + 1].SetActive(false);
        }
    }
}
