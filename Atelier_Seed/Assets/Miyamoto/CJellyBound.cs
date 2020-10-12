
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
    // //   transform：変形させるもののtransform         // //
    // //   min_scale：変形後の大きさの値                // //
    // //   change   ：一回の処理でどれくらい潰すかの量  // //
    // //                                                // //
    // //   〇 戻り値 〇                                 // //
    // //   変形した後のtransform                        // //
    // //                                                // //

    //----------------------------------------------------------------------------------------------------

    public static Transform Crash_Height(Transform transform, float min_scale, float change)
    {
        // 拡大縮小値を取得
        Vector3 scale = transform.localScale;


        // 拡大縮小のｙ値を変化量分潰す
        scale.y -= change;


        // 変形後の大きさの値（最低値）より小さくならないように設定する
        if(scale.y < min_scale)
        {
            scale.y = min_scale;
        }


        // 取得したトランスフォームの拡大縮小値を変形後の値に設定
        transform.localScale = scale;


        // 変形したトランスフォームを返す
        return transform;
    }



    //----------------------------------------------------------------------------------------------------

    // //                                                 // //
    // //   縦に潰れる（縦長になる）関数                  // //
    // //                                                 // //
    // //   〇 引数 〇                                    // //
    // //   transform：変形させるもののtransform          // //
    // //   min_scale：変形後の大きさの値                 // //
    // //   change 　：一回の処理でどれくらい潰すかの量   // //
    // //                                                 // //
    // //   〇 戻り値 〇                                  // //
    // //   変形した後のtransform                         // //
    // //                                                 // //

    //----------------------------------------------------------------------------------------------------

    public static Transform Crash_Width(Transform transform, float min_scale, float change)
    {
        // 拡大縮小値を取得
        Vector3 scale = transform.localScale;


        // 拡大縮小のｘ値を変化量分潰す
        scale.x -= change;

        
        // 変形後の大きさの値（最低値）より小さくならないように設定する
        if (scale.x < min_scale)
        {
            scale.x = min_scale;
        }


        // 取得したトランスフォームの拡大縮小値を変形後の値に設定
        transform.localScale = scale;


        // 変形したトランスフォームを返す
        return transform;
    }



    //----------------------------------------------------------------------------------------------------

    // //                                                  // //
    // //   縦に伸ばす関数                                 // //
    // //                                                  // //
    // //   〇 引数 〇                                     // //
    // //   transform：変形させるもののtransform           // //
    // //   max_scale：変形後の大きさの値                  // //
    // //   change   ：一回の処理でどれくらい伸ばすかの量  // //
    // //                                                  // //
    // //   〇 戻り値 〇                                   // //
    // //   変形したtransform                              // //
    // //                                                  // //

    //----------------------------------------------------------------------------------------------------

    public static Transform Expand_Height(Transform transform, float max_scale, float change)
    {
        // 拡大縮小値を取得
        Vector3 scale = transform.localScale;


        // 拡大縮小のｙ値を変化量分伸ばす
        scale.y += change;


        // 変形後の大きさの値（最大値）より大きくならないように設定する
        if (scale.y > max_scale)
        {
            scale.y = max_scale;
        }


        // 取得したトランスフォームの拡大縮小値を変形後の値に設定
        transform.localScale = scale;


        // 変形したトランスフォームを返す
        return transform;
    }



    //----------------------------------------------------------------------------------------------------

    // //                                                     // //
    // //   横に伸ばす関数                                    // //
    // //                                                     // //
    // //   〇 引数 〇                                        // //
    // //   transform：変形させるもののtransform              // //
    // //   max_scale：変形後の大きさの値                     // // 
    // //   change   ：一回の処理でどれくらい伸ばすかの量     // //
    // //                                                     // //
    // //   〇 戻り値 〇                                      // //
    // //   変形した後のtransform                             // //
    // //                                                     // //

    //----------------------------------------------------------------------------------------------------

    public static Transform Expand_Width(Transform transform, float max_scale, float change)
    {
        // 拡大縮小値を取得
        Vector3 scale = transform.localScale;


        // 拡大縮小のｘ値を変化量分伸ばす
        scale.x += change;


        // 変形後の大きさの値（最大値）より大きくならないように設定する
        if (scale.x > max_scale)
        {
            scale.x = max_scale;
        }


        // 取得したトランスフォームの拡大縮小値を変形後の値に設定
        transform.localScale = scale;


        // トランスフォームを返す
        return transform;
    }
    
}
