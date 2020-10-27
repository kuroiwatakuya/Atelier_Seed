
// //                                   // //
// //   Author:宮本早希                 // //
// //   シーン開始時自動フェードイン    // //
// //                                   // //


// // インクルードファイル // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // クラス // //
public class FadeIn : MonoBehaviour
{
    // // 初期化 // //
    void Start()
    {
        // フェードインする
        CFadeManager.FadeIn();
    }    
}
