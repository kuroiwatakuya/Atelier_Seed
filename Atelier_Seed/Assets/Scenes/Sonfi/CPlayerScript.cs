﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject TrophyObject;
    //---宮本加筆ここから------------------------------
    [SerializeField] private GameObject ShotEffect;
    [SerializeField] private GameObject GunEnterEffect;
    [SerializeField] private GameObject GunShootEffect;
    public Quaternion GunRotate;
    private Vector3 EffectPosition;
    private GameObject GunFind;
    //---宮本加筆ここまで------------------------------


    //************
    //対象タグ
    //************
    public string StopBlockTag;
    public string GunTag;

    //他スクリプト
    public CGoal GoalScript;

    //********************
    // コンポーネント
    //********************
    private Rigidbody2D Rbody;
    private SpriteRenderer SpriteRenderer;

    //矢印引っ張り用
    [SerializeField]
    private LineRenderer Direction = null;

    //********
    // 変数
    //********
    //発射力
    public float Power;
    //最大付与力量
    public float MaxMagnitude;
    //最小付与力量
    public float MinMagnitude;
    //速度最小値
    public int VelocityMin;

    public Vector2 Velocity;
    public bool GunFlag;

    //回数制限
    public int PlayCount = 3;

    //止まるフラグ
    public bool StopFlag;

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
    public bool PlayFlag;
    //クリックしたか
    public bool ClickFlag;
    //大砲用タップ判定
    private bool TapFlag;

    private float Count = 2;
    public bool CoolTime = false;

    //止まるブロックに接触してるか
    private bool StopFieldFlag;
    public int StopFieldCount;
    private bool OnlyStopCount;

    //壊れるブロックあたった回数
    public int BreakBlockCount;

    //落ちてるトロフィー獲得したか
    public bool GetStageTrophy;

    //大砲入る条件用
    public bool GunTrophyFlag;

    // Start is called before the first frame update

    //アニメーション用変数
    private Animator anim = null;

    void Start()
    {
        Rbody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();

        //ラインレンダラー取得
        Direction = GameObject.Find("Direction").GetComponent<LineRenderer>();

        //子のアニメーション取得
        anim = GameObject.Find("PlayerSprite").GetComponent<Animator>();

        this.MainCamera = Camera.main;
        this.MainCameraTransform = this.MainCamera.transform;

        PlayFlag = false;
        ClickFlag = false;
        StopFieldFlag = false;
        GunFlag = false;
        TapFlag = false;
        StopFlag = false;

        StopFieldCount = 0;

        GetStageTrophy = false;

        //---宮本加筆ここから------------------------------
        ShotEffect = (GameObject)Resources.Load("Effect_PlayerShot");
        GunEnterEffect = (GameObject)Resources.Load("Effect_GunEnter");
        GunShootEffect = (GameObject)Resources.Load("Effect_GunShoot");
        if (GunFind == null)
        {
            GunFind = GameObject.FindWithTag("Gun");
        }
        //---宮本加筆ここまで------------------------------

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
    
    // Update is called once per frame
    void Update()
    {
        //---宮本加筆ここから------------------------------
        EffectPosition = this.transform.position;
        if (GunFind != null)
        {
            GunRotate = GunObject.transform.rotation;
        }
        //---宮本加筆ここまで------------------------------

        if (Input.GetMouseButtonDown(0) && !TapFlag && GunFlag)
        {
            //大砲用タップ
            TapFlag = true;
        }

        //マウス左クリック＆タップ
        if (Input.GetMouseButtonDown(0))
        {
            //動いてないかつクリックしてない
            if (!PlayFlag && !ClickFlag)
            {
                ClickFlag = true;
                DragStart = GetMousePosition();

                //矢印フラグ
                this.Direction.enabled = true;
                this.Direction.SetPosition(0, Rbody.position);  //矢印の位置
                this.Direction.SetPosition(1, Rbody.position);
            }
        }
        if (ClickFlag == true)
        {
            //ドラッグ処理
            if (ClickFlag)
            {
                Vector2 position = GetMousePosition();
                DirectionForce = position - DragStart;      

                if (DirectionForce.magnitude > MaxMagnitude )
                {
                    DirectionForce *= MaxMagnitude / DirectionForce.magnitude;
                }
                
                this.Direction.SetPosition(0, Rbody.position);//矢印の位置
                this.Direction.SetPosition(1, Rbody.position + DirectionForce * -1);  //矢印の向き

            }
        }
            
        //マウスを離したとき
        if (Input.GetMouseButtonUp(0))
        {
            //クリックフラグがオンなら
            if (ClickFlag)
            {
                ClickFlag = false;

                if (DirectionForce.magnitude >= MinMagnitude)
                {
                    PlayFlag = true;

                    StopFieldFlag = false;

                    TurnCount--;
                    //弾く
                    Flip(DirectionForce * Power * -1);

                    //---宮本加筆ここから------------------------------
                    // ショットエフェクト発生
                    Instantiate(ShotEffect, EffectPosition, Quaternion.identity);
                    //---宮本加筆ここまで------------------------------

                    //矢印オフ
                    this.Direction.enabled = false;


                    //回転アニメーションオン
                    anim.SetBool("Move", true);
                }
            }
        }
       
    }

    void FixedUpdate()
    {
        Velocity.y = Rbody.velocity.y;
        Velocity.x = Rbody.velocity.x;

        //遅くなったらとめる
        if (Velocity.y == 0 &&Velocity.x <= 12 && Velocity.x >= -12 && PlayFlag && !GunFlag)
        {
            Rbody.velocity = new Vector2(0, 0);
            
            //プレイヤ―を正しい向きで止める
            Rbody.rotation = 0.0f;

            //プレイフラグオフ
            PlayFlag = false;

            PlayCount--;

            //アニメーションを終了させる
            anim.SetBool("Move", false);

            //全てのリジッドボディを止める
            Rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        //********************************************************************
        //くっつくギミック
        //********************************************************************
        if (StopFieldFlag)
        {
            if(!OnlyStopCount)
            {
                StopFieldCount++;
            }
            OnlyStopCount = true;
            Rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            OnlyStopCount = false;
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
            //---宮本加筆ここから------------------------------
            // 大砲発射エフェクト発生
            Instantiate(GunShootEffect, EffectPosition, Quaternion.Euler(GunRotate.x, GunRotate.y, GunRotate.z * 100));
            //---宮本加筆ここまで------------------------------

            SpriteRenderer.color = new Color(1, 1, 1, 1);

            GunFlag = false;
            Rbody.constraints = RigidbodyConstraints2D.None;

            var Direction = GunPlayerPosition.transform.position - GunObject.transform.position;
            Direction *= Direction.magnitude;
            Flip(Direction * 5);

            //TapFlag = false;
        }

        //*****************************
        // 壊れるブロック
        //*****************************


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

        //ゲームオーバー
        if(PlayCount <= 0)
        {
            SceneManager.LoadScene("GameOver");
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
            GunTrophyFlag = true;

            //---宮本加筆ここから------------------------------
            // 大砲エントリーエフェクト発生
            Instantiate(GunEnterEffect, EffectPosition, Quaternion.Euler(GunRotate.x, GunRotate.y, GunRotate.z * 100));
            //---宮本加筆ここまで------------------------------
        }

        //トロフィーゲット
        if (collider.gameObject.name == "Trophy")
        {
            Destroy(TrophyObject.gameObject);
            GetStageTrophy = true;
        }
    }
}
