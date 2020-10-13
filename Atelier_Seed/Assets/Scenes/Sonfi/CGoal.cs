﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGoal : MonoBehaviour
{

    public int Now_StageNum;

    public bool Clear_Flag;

    // Start is called before the first frame update
    void Start()
    {
        Clear_Flag = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Clear_Flag)
        {
            //PlayerPrefsの解放ステージ数に現在クリアしたステージをいれる
            PlayerPrefs.SetInt("STAGENUM", Now_StageNum + 1);
            //PlayerPrefsをセーブする         
            PlayerPrefs.Save();
            SceneManager.LoadScene("Result");

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {

            Clear_Flag = true;

        }
    }
}
