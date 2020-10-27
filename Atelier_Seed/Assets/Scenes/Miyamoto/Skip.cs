
// //                      // // 
// //   Author:宮本早希    // //
// //   スキップボタン     // //
// //                      // //


// // インクルードファイル // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Skip : MonoBehaviour
{

    // 次のシーン番号
    public int NextScene;

    // // クリック or タップ // //
    public void OnMouseDown()
    {
        // 次のシーンへ
        SceneManager.LoadScene(NextScene);
    }
}
