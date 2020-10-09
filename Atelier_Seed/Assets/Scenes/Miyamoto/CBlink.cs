
// //                                     // //
// //   Author:宮本 早希                  // //
// //   点滅させるスクリプト              // //
// //   ※ Text / Image のみ対応          // //
// //                                     // //
// //   参考                              // //
// //   https://vend9520-lab.net/?p=382   // //
// //                                     // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// // クラス // //
public class CBlink : MonoBehaviour
{
    // // public // //

    // 点滅スピード
    public float Speed = 1.0f;          // 点滅するスピード
    public float AlphaInterval = 0.5f;  // 一回の処理でどれくらいα値が変化するか

//-------------------------------------------------------------------

    // // private // //

    // テキストのコンポーネント格納用
    private Text ThisText;

    // イメージのコンポーネント格納用
    private Image ThisImage;


    // 点滅させるための時間
    private float Timer;


    // オブジェクトの種類一覧
    private enum ObjectType
    {
        TEXT,
        IMAGE
    };


    // 現在のオブジェクトの種類（初期値はテキスト）
    private ObjectType ThisObjectType = ObjectType.TEXT;

//-------------------------------------------------------------------

    // // 初期化 // //
    void Start()
    {
        // // アタッチしているオブジェクトの判別 // //

        // Imageだったとき
        if(this.gameObject.GetComponent<Image>())
        {
            ThisObjectType = ObjectType.IMAGE;                  // オブジェクトの種類をImageに設定
            ThisImage = this.gameObject.GetComponent<Image>();  // イメージのコンポーネントを取得
        }

        // Textだったとき
        else if(this.gameObject.GetComponent<Text>())
        {
            ThisObjectType = ObjectType.TEXT;                   // オブジェクトの種類をTextに設定
            ThisText = this.gameObject.GetComponent<Text>();    // テキストのコンポーネントを取得
        }
    }

//-------------------------------------------------------------------

    // // 更新 // //
    void Update()
    {
        // // オブジェクトのα値を更新 // //

        // Imageだったとき
        if (ThisObjectType == ObjectType.IMAGE)
        {
            ThisImage.color = GetAlphaColor(ThisImage.color);   // α値調整
        }

        // Textだったとき
        else if (ThisObjectType == ObjectType.TEXT)
        {
            ThisText.color = GetAlphaColor(ThisText.color);     // α値調整
        }
    }

//-------------------------------------------------------------------

    // // α値を更新してColorを返す // //
    Color GetAlphaColor(Color color)
    {
        Timer += Time.deltaTime * Speed;                     // 点滅時間
        color.a = Mathf.Sin(Timer) * AlphaInterval + 0.5f;   // α値変更

        return color;   // αを変更したカラーを返す
    }
}
