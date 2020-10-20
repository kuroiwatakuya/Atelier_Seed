
// //                        // //
// //   Author:宮本 早希     // //
// //   衝突エフェクト生成   // //
// //                        // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// // クラス // //
public class CHitEffect : MonoBehaviour
{
    // 衝突エフェクト格納用ゲームオブジェクト
    private GameObject HitEffectObject;
    private GameObject HitSplashObject;


    // エフェクトの座標
    private Vector3 Effpos;


    // オブジェクト保存用空オブジェクトのtransform
    private Transform EffectPool;


    // // 初期化 // //
    void Start()
    {
        // 衝突エフェクトを検索して代入（普通の壁）
        HitEffectObject = (GameObject)Resources.Load("Effect_Hit");


        // 粘着質ブロックへの衝突エフェクトを取得（ステージごとに色分け）
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            HitSplashObject = (GameObject)Resources.Load("Effect_Splash1");
        }

        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            HitSplashObject = (GameObject)Resources.Load("Effect_Splash2");
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            HitSplashObject = (GameObject)Resources.Load("Effect_Splash3");
        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            HitSplashObject = (GameObject)Resources.Load("Effect_Splash4");
        }


        // 衝突エフェクトのオブジェクトを生成する
        EffectPool = new GameObject("Hit").transform;        
    }


    // // 更新 // //
    void Update()
    {
        // エフェクトを生成する場所を取得（プレイヤー座標）
        Effpos = this.transform.position;
    }


    // // 当たった時 // //
    void OnCollisionEnter2D(Collision2D coll)
    {
        // 普通の壁とか
        if (coll.gameObject.tag == "PlayerCrush")
        {
            GetHitObject(HitEffectObject, Effpos, Quaternion.identity);            
        }

        // 粘着質ブロック
        if (coll.gameObject.tag == "StopFieldTag")
        {
            GetSplashObject(HitSplashObject, Effpos, Quaternion.identity);
        }
    }


    // // ゲームオブジェクトのアクティブ判別と生成 // //
    void GetHitObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (Transform transform in EffectPool)
        {
            // オブジェクトが非アクティブなら使いまわし
            if (!transform.gameObject.activeSelf)
            {
                transform.SetPositionAndRotation(pos, qua);
                transform.gameObject.SetActive(true);
                return;
            }
        }

        // 非アクティブなオブジェクトがなければ生成する
        Instantiate(obj, pos, qua, EffectPool);
    }

    void GetSplashObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (Transform transform in EffectPool)
        {
            // オブジェクトが非アクティブなら使いまわし
            if (!transform.gameObject.activeSelf)
            {
                transform.SetPositionAndRotation(pos, qua);
                transform.gameObject.SetActive(true);
                return;
            }
        }

        // 非アクティブなオブジェクトがなければ生成する
        Instantiate(obj, pos, qua, EffectPool);
    }
}
