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
    public GameObject GunObject;
    public GameObject GunPlayerPosition;

    //************
    //対象タグ
    //************
    public string StopBlockTag;
    public string GunTag;

    //********************
    // コンポーネント
    //********************
    private Rigidbody2D Rbody;
    private SpriteRenderer SpriteRenderer;

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
    //大砲用タップ判定
    private bool TapFlag;

    private float Count = 2;
    public bool CoolTime = false;

    //止まるブロックに接触してるか
    private bool StopFieldFlag;
    // Start is called before the first frame update
    void Start()
    {
        Rbody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        this.MainCamera = Camera.main;
        this.MainCameraTransform = this.MainCamera.transform;

        PlayFlag = false;
        ClickFlag = false;
        StopFieldFlag = false;
        GunFlag = false;
        TapFlag = false;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !TapFlag && GunFlag)
        {
            //大砲用タップ
            TapFlag = true;
        }
    }

    void FixedUpdate()
    {
        Velocity.y = Rbody.velocity.y;
        Velocity.x = Rbody.velocity.x;
        //遅くなったらとめる
        if (Velocity.y == 0 && Velocity.x <= 8 && Velocity.x >= -8 && PlayFlag)
        {
            Rbody.velocity = new Vector2(0, 0);
            PlayFlag = false;
        }

        //くっつくギミック
        if (StopFieldFlag)
        {
            Rbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            Rbody.constraints = RigidbodyConstraints2D.None;
        }

        //大砲の中に入ったら
        if (GunFlag)
        {
            SpriteRenderer.color = new Color(1, 1, 1, 0);

            Count = 0;

            transform.position = GunPlayerPosition.transform.position;
            Rbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }

        if (TapFlag)
        {

            SpriteRenderer.color = new Color(1, 1, 1, 1);

            GunFlag = false;
            Rbody.constraints = RigidbodyConstraints2D.None;

            var Direction = GunPlayerPosition.transform.position - GunObject.transform.position;
            Direction *= Direction.magnitude;
            Flip(Direction * 5);

            TapFlag = false;

        }



        TapFlag = false;
        CoolTime = true;

        if (CoolTime)
        {
            Count += 1 * Time.deltaTime;
            if (Count >= 1)
            {
                CoolTime = false;
            }
        }

    }

    //力加える
    public void Flip(Vector2 force)
    {
        // 力加える
        Rbody.AddForce(force, ForceMode2D.Impulse);
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
        if (collider.gameObject.tag == GunTag && !GunFlag && !CoolTime)
        {
            GunFlag = true;
        }
    }

}
