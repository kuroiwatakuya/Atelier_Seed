using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveCamera : MonoBehaviour
{

    //ドラッグ開始点
    private Vector2 DragStart;

    //カメラ座標
    private Transform CameraTransform;
    //メインカメラ
    private Camera MainCamera;

    //クリックしたか
    public bool ClickFlag;

    //タップし始めた位置と現在の位置の距離Y座標のみ
    private Vector2 Direction;
    //前の距離
    private Vector2 OldDirection;

    // Start is called before the first frame update
    void Start()
    {

        this.MainCamera = Camera.main;
        this.CameraTransform = this.MainCamera.transform;

        Direction = new Vector2(0.0f, 0.0f);
        OldDirection = new Vector2(0.0f, 0.0f);

        ClickFlag = false;
    }

    //マウス座標をワールド座標に変換して取得
    private Vector3 GetMousePosition()
    {
        // マウスから取得できないZ座標を補完する
        var position = Input.mousePosition;
        position.z = transform.position.z;
        position = this.MainCamera.ScreenToWorldPoint(position);
        position.z = 0;

        return position;
    }

    // Update is called once per frame
    void Update()
    {


    // マウス左クリック＆タップ
        if (Input.GetMouseButtonDown(0))
        {
            //動いてないかつクリックしてない
            if (!ClickFlag)
            {
                ClickFlag = true;
                DragStart = GetMousePosition();
            }
        }
        if (ClickFlag == true)
        {
            //ドラッグ処理
            if (ClickFlag)
            {
                Vector2 position = GetMousePosition();
                Direction = position - DragStart;
                var Posy = transform.position.y;

                Posy += Direction.y * Time.deltaTime;
                Direction.y = 0;

            }
        }

        //マウスを離したとき
        if (Input.GetMouseButtonUp(0))
        {

            //クリックフラグがオンなら
            if (ClickFlag)
            {
                OldDirection = Direction;
                ClickFlag = false;
            }
        }

    }
}
