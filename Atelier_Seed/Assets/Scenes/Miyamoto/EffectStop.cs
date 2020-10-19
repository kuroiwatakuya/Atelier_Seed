
// //                                             // //
// //   Author:宮本早希                           // //
// //   エフェクトの再生が終了したら              // //
// //   ゲームオブジェクトを非アクティブにする    // //
// //                                             // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // クラス // //
public class EffectStop : MonoBehaviour
{
    // // パーティクルの再生が終了したら // //
    private void OnParticleSystemStopped()
    {
        // ゲームオブジェクトを非アクティブにする
        this.gameObject.SetActive(false);
    }
}
