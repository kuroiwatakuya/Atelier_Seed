using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//**************************************
// プレイヤー全般のスクリプト
//**************************************

public class CPlayerScript : MonoBehaviour
{
    //*********************
    //他ゲームオブジェクト
    //*********************

    //************
    //対象タグ
    //************
    public string StopBlockTag;
    public string GunTag;

    //********************
    // コンポーネント
    //********************
    private Rigidbody2D Rbody;

    //********
    // 変数
    //********
    //発射力
    public float Power;
    //最大付与力量
    public float MaxMagnitude;
    //速度最小値
    public int VelocityMin;

    public Vector2 Velocity;
    public bool GunFlag;

    //発射方向の力
    private Vector2 DirectionForce;
    //メインカメラ
    private Camera MainCamera;
    //メインカメラ座標
    private Transform MainCameraTransform;
    //ドラッグ開始点
    private Vector2 DragStart;
    //残りターン
    private int TurnCount = 3;
    //弾いてるか
    private bool PlayFlag;
    //クリックしたか
    private bool ClickFlag;

    //止まるブロックに接触してるか
    private bool StopFieldFlag;
    // Start is called before the first frame update
    void Start()
    {
        Rbody = this.GetComponent<Rigidbody2D>();
        this.MainCamera = Camera.main;
        this.MainCameraTransform = this.MainCamera.transform;

        PlayFlag = false;
        ClickFlag = false;
        StopFieldFlag = false;
        GunFlag = false;
    }

    //マウス座標をワールド座標に変換して取得
    private Vector3 GetMousePosition()
    {
        // マウスから取得できないZ座標を補完する
        var position = Input.mousePosition;
        position.z = this.MainCameraTransform.position.z;
        position = this.MainCamera.ScreenToWorldPoint(position);
        position.z = 0;

        return position;
    }

    //ドラック開始
    public void OnMouseDown()
    {
        if (!PlayFlag && !ClickFlag)
        {
            ClickFlag = true;
            DragStart = GetMousePosition();
        }
    }
    //ドラッグ中
    public void OnMouseDrag()
    {
        if (ClickFlag)
        {
            Vector2 position = GetMousePosition();
            DirectionForce = position - DragStart;
            if (DirectionForce.magnitude > MaxMagnitude * MaxMagnitude)
            {
                DirectionForce *= MaxMagnitude / DirectionForce.magnitude;
            }
        }
    }
    /// ドラッグ終了
    public void OnMouseUp()
    {
        if (ClickFlag)
        {
            ClickFlag = false;
            PlayFlag = true;

            StopFieldFlag = false;

            TurnCount--;
            //弾く
            Flip(DirectionForce * Power * -1);
        }
    }
    /// 力加える
    public void Flip(Vector2 force)
    {
        // 瞬間的に力を加えてはじく
        Rbody.AddForce(force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Velocity.y = Rbody.velocity.y;
        Velocity.x = Rbody.velocity.x;
        if (Velocity.y == 0 && Velocity.x <= 8 && Velocity.x >= -8 && PlayFlag)
        {
            Rbody.velocity = new Vector2(0, 0);
            PlayFlag = false;
        }

        if (StopFieldFlag)
        {
            Rbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            Rbody.constraints = RigidbodyConstraints2D.None;
            Rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == StopBlockTag && !StopFieldFlag)
        {
            StopFieldFlag = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == GunTag && !GunFlag)
        {
            GunFlag = true;
        }
    }

}
