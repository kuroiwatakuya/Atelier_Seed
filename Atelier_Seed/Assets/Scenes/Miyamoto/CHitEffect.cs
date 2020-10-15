
// //                        // //
// //   Author:宮本 早希     // //
// //   衝突エフェクト生成   // //
// //                        // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // クラス // //
public class CHitEffect : MonoBehaviour
{
    // 衝突エフェクト格納用ゲームオブジェクト
    private GameObject HitEffectObject;


    // エフェクトの座標
    private Vector3 Effpos;


    // // 初期化 // //
    void Start()
    {
        // 衝突エフェクトを検索して代入
        HitEffectObject = (GameObject)Resources.Load("Effect_Hit");
    }


    // // 更新 // //
    void Update()
    {
        Effpos = this.transform.position;
    }


    // // 当たった時 // //
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PlayerCrush")
        {
            Instantiate(HitEffectObject, new Vector3(Effpos.x, Effpos.y, Effpos.z), Quaternion.identity);
        }
    }
}
