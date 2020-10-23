
// //                        // //
// //   Author:宮本早希      // //
// //   シーン間のフェード   // //
// //                        // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// // クラス // //
public class CFadeManager : MonoBehaviour
{
    // フェード用の Canvas と Image
    private static Canvas FadeCanvas;
    private static Image FadeImage;


    // フェード用の Image の透明度
    private static float Alpha = 0.0f;


    // フェードイン, フェードアウトのフラグ
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;


    // フェード時間（単位：杪）
    public static float FadeTime = 1.0f;
    

    // 遷移先のシーン番号
    private static int NextScene;



    // // フェード用の Canvas と Image の生成
    static void Initialize()
    {
        // フェード用の Canvas 生成
        GameObject FadeCanvasObject = new GameObject("FadeCanvas");     // フェードキャンバスオブジェクトを生成
        FadeCanvas = FadeCanvasObject.AddComponent<Canvas>();           // キャンバスを生成
        FadeCanvasObject.AddComponent<GraphicRaycaster>();              // グラフィックレイキャスタを生成
        FadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;          // キャンバスのレンダモードを設定
        FadeCanvasObject.AddComponent<CFadeManager>();                  // フェードマネージャースクリプトをアタッチ

        // 最前面になるようにソートオーダー設定
        FadeCanvas.sortingOrder = 100;



        // フェード用の Image 生成
        FadeImage = new GameObject("FadeImage").AddComponent<Image>();  // フェード用イメージ生成
        FadeImage.transform.SetParent(FadeCanvas.transform, false);     // フェードキャンバスを親に設定
        FadeImage.rectTransform.anchoredPosition = Vector3.zero;        // 位置座標を決定


        // Image サイズを設定
        FadeImage.rectTransform.sizeDelta = new Vector2(2000, 2000);


        // フェードアウトフラグがＯＮのとき
        if (isFadeOut)
        {
            Alpha = 0.0f;       // α値をゼロに
            isFadeOut = false;  // フラグＯＦＦ
        }


        // フェードインフラグがＯＮのとき
        if (isFadeIn)
        {
            Alpha = 0.0f;
            isFadeIn = false;
        }
    }


    // // フェードイン開始 // //
    public static void FadeIn()
    {
        if (FadeImage == null) Initialize();    // フェードイメージがなければ初期化
        FadeImage.color = Color.black;          // フェードカラー設定
        FadeCanvas.enabled = true;              // フェードキャンバス有効化
        isFadeIn = true;                        // フェードインフラグＯＮ
        Alpha = 1.0f;                           // α値を1.0fにして開始
    }


    // // フェードアウト開始 // //
    public static void FadeOut(int nextscene)
    {
        if (FadeImage == null) Initialize();    // フェードイメージがなければ初期化
        NextScene = nextscene;                  // 遷移先のシーン番号を設定
        FadeImage.color = Color.black;          // フェードカラー設定
        FadeCanvas.enabled = true;              // フェードキャンバス有効化
        isFadeOut = true;                       // フェードアウトフラグＯＮ
    }


    // // 更新 // //
    void Update()
    {
        // フェードイン
        if (isFadeIn)
        {
            // 経過時間から透明度計算
            Alpha -= Time.deltaTime / FadeTime;

            // フェードイン終了判定
            if (Alpha <= 0.0f)
            {
                isFadeIn = false;           // フェードインフラグＯＦＦ
               // Alpha = 0.0f;               // α値０設定
                FadeCanvas.enabled = false;  // フェードキャンバス無効化
            }

            // フェード用 Imgae のカラー設定
            FadeImage.color = new Color(0.0f, 0.0f, 0.0f, Alpha);
        }


        // フェードアウト
        else if (isFadeOut)
        {
            // 時間経過から透明度計算
            Alpha += Time.deltaTime / FadeTime;

            // フェードアウト終了判定
            if (Alpha >= 1.0f)
            {
                isFadeOut = false;  // フェードアウト
               // Alpha = 1.0f;       // α値１設定

                // 次のシーンへ遷移
                SceneManager.LoadScene(NextScene);
            }

            // フェード用 Image のカラー設定
            FadeImage.color = new Color(0.0f, 0.0f, 0.0f, Alpha);
        }
    }
}
