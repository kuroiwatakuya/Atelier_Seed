
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
    public float speed = 1.0f;

//-------------------------------------------------------------------

    // // private // //

    // テキスト用変数
    private Text text;

    // イメージ用変数
    private Image image;


    // 点滅させるための計測用
    private float time;


    // オブジェクトの種類一覧
    private enum ObjectType
    {
        TEXT,
        IMAGE
    };


    // 現在のオブジェクトの種類（初期値はテキスト）
    private ObjectType thisObjectType = ObjectType.TEXT;

//-------------------------------------------------------------------

    // // 初期化 // //
    void Start()
    {
        // // アタッチしているオブジェクトの判別 // //

        // Imageだったとき
        if(this.gameObject.GetComponent<Image>())
        {
            thisObjectType = ObjectType.IMAGE;
            image = this.gameObject.GetComponent<Image>();
        }

        // Textだったとき
        else if(this.gameObject.GetComponent<Text>())
        {
            thisObjectType = ObjectType.TEXT;
            text = this.gameObject.GetComponent<Text>();
        }
    }

//-------------------------------------------------------------------

    // // 更新 // //
    void Update()
    {
        // // オブジェクトのα値を更新 // //

        // Imageだったとき
        if (thisObjectType==ObjectType.IMAGE)
        {
            image.color = GetAlphaColor(image.color);
        }

        // Textだったとき
        else if(thisObjectType==ObjectType.TEXT)
        {
            text.color = GetAlphaColor(text.color);
        }
    }

//-------------------------------------------------------------------

    // α値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
