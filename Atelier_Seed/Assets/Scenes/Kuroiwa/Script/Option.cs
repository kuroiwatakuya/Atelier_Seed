﻿//=====================================================
//オプション画面
//
//説明:オプション画面の表示をさせるスクリプトです。
//更新:10/12(月)
//=====================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DotWeenを使用する

//=====================================================
//オプション表示クラス
//=====================================================
public class Option : MonoBehaviour
{
    public bool OptionFrag;     //オプションオンオフのフラグ

    public GameObject Pause;     //子のオブジェクト取得(UI画面取得

    public AudioClip OptionSE;              //オプションのSE用
    public AudioClip OptionCancelSE;    //オプションキャンセルのSE用
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        OptionFrag = false;
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //==================================================
    //オプション画面表示
    //==================================================
    public void OnMouseDown()
    {
        Pause.SetActive(!Pause.activeSelf);
        
        if (Pause.activeSelf)
        {
            audioSource.PlayOneShot(OptionSE);      //オプション押したとき
            Time.timeScale = 0f;
        }
        if (!Pause.activeSelf)
        {
            audioSource.PlayOneShot(OptionCancelSE);    //オプションをキャンセルしたとき
            Time.timeScale = 1f;
        }
    }
}
