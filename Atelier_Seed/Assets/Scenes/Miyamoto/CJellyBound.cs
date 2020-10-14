
// //                                       // //
// //   Author：宮本 早希                   // //
// //   ゼリーみたいにむにゅってなるやつ    // //
// //                                       // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // クラス // //
public class CJellyBound : MonoBehaviour
{
    // 今回は使わない人たち
    void Start() {}   
    void Update() {}


    //----------------------------------------------------------------------------------------------------

    // //                                                // //
    // //   横に潰れる（横長になる）関数                 // //
    // //                                                // //
    // //   〇 引数 〇                                   // //
    // //   scale    ：変形させるもののScale             // //
    // //   min_scale：変形後の大きさの値                // //
    // //   change   ：一回の処理でどれくらい潰すかの量  // //
    // //                                                // //
    // //   〇 戻り値 〇                                 // //
    // //   変形した後のScale                            // //
    // //                                                // //

    //----------------------------------------------------------------------------------------------------

    public static Vector3 Crush_Height(Vector3 scale, float min_scale, float change)
    {
        // 変形させる拡大縮小値を取得
        Vector3 tempScale = scale;


        // ｙ値を変化量分潰す
        tempScale.y -= change * Time.deltaTime;

        // 潰しすぎないように調整
        if (tempScale.y < min_scale)
        {
            tempScale.y = min_scale;
        }


        // 変形させた拡大縮小値を返す
        return tempScale;
    }



    //----------------------------------------------------------------------------------------------------

    // //                                                 // //
    // //   縦に潰れる（縦長になる）関数                  // //
    // //                                                 // //
    // //   〇 引数 〇                                    // //
    // //   scale    ：変形させるもののScale              // //
    // //   min_scale：変形後の大きさの値                 // //
    // //   change 　：一回の処理でどれくらい潰すかの量   // //
    // //                                                 // //
    // //   〇 戻り値 〇                                  // //
    // //   変形した後のScale                             // //
    // //                                                 // //

    //----------------------------------------------------------------------------------------------------

    public static Vector3 Crush_Width(Vector3 scale, float min_scale, float change)
    {
        // 変形させる拡大縮小値を取得
        Vector3 tempScale = scale;


        // ｘ値を変化量分潰す
        tempScale.x -= change * Time.deltaTime;

        // 潰しすぎないように調整
        if (tempScale.x < min_scale)
        {
            tempScale.x = min_scale;
        }


        // 変形させた拡大縮小値を返す
        return tempScale;
    }



    //----------------------------------------------------------------------------------------------------

    // //                                                  // //
    // //   縦に伸ばす関数                                 // //
    // //                                                  // //
    // //   〇 引数 〇                                     // //
    // //   scale    ：変形させるもののScale               // //
    // //   max_scale：変形後の大きさの値                  // //
    // //   change   ：一回の処理でどれくらい伸ばすかの量  // //
    // //                                                  // //
    // //   〇 戻り値 〇                                   // //
    // //   変形した後のScale                              // //
    // //                                                  // //

    //----------------------------------------------------------------------------------------------------

    public static Vector3 Expand_Height(Vector3 scale, float max_scale, float change)
    {
        // 変形させる拡大縮小値を取得
        Vector3 tempScale = scale;


        // ｙ値を変化量分伸ばす
        tempScale.y += change * Time.deltaTime;

        // 伸ばしすぎないように調整
        if (tempScale.y > max_scale)
        {
            tempScale.y = max_scale;
        }


        // 変形させた拡大縮小値を返す
        return tempScale;
    }



    //----------------------------------------------------------------------------------------------------

    // //                                                     // //
    // //   横に伸ばす関数                                    // //
    // //                                                     // //
    // //   〇 引数 〇                                        // //
    // //   scale    ：変形させるもののScale                  // //
    // //   max_scale：変形後の大きさの値                     // // 
    // //   change   ：一回の処理でどれくらい伸ばすかの量     // //
    // //                                                     // //
    // //   〇 戻り値 〇                                      // //
    // //   変形した後のScale                                 // //
    // //                                                     // //

    //----------------------------------------------------------------------------------------------------

    public static Vector3 Expand_Width(Vector3 scale, float max_scale, float change)
    {
        // 変形させる拡大縮小値を取得
        Vector3 tempScale = scale;


        // ｘ値を変化量分伸ばす
        tempScale.x += change * Time.deltaTime;

        // 伸ばしすぎないように調整
        if (tempScale.x > max_scale)
        {
            tempScale.x = max_scale;
        }


        // 変形させた拡大縮小値を返す
        return tempScale;
    }
    
}
