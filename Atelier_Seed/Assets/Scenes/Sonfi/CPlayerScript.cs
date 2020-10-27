﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common;

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
    [SerializeField] private GameObject ShotEffect;         // プレイヤーショットのエフェクトオブジェクト
    [SerializeField] private GameObject GunEnterEffect;     // Gunに入った時のエフェクトオブジェクト
    [SerializeField] private GameObject GunShootEffect;     // Gunショットのエフェクトオブジェクト
    public Quaternion GunRotate;                            // Gunの回転値
    private Vector3 EffectPosition;                         // エフェクト生成座標
    private GameObject GunFind;                             // Gun発見用オブジェクト
    private Transform ShotPool;                             // オブジェクト保存用空オブジェクトのtransform
    private Transform GunEnterPool;                         // 同上（Gun進入用）
    private Transform GunShootPool;                         // 同上（Gun発射用）
    //---宮本加筆ここまで------------------------------


    //************
    //対象タグ
    //************
    public string StopBlockTag;
    public string GunTag;

    //他スクリプト
    public CGoal GoalScript;
    public Banner BannerScript;

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

    //かべぶつかったか
    public bool WallFlag;
    //壁にぶつかった回数
    public int WallCount;
    public bool WallCountBath;
    //飛ぶ
    public bool Fly;
    //飛び始めた座標
    private Vector2 StartPosition;
    //風衝突
    public bool Wind;

    //取得コイン
    public int GetCoin;

    //写真取得
    public bool GetPhoto;
    public bool[] StagePhoto = new bool[CConst.STAGENUM];
    public static int PhotoNum;
    public GameObject PhotoSprite;


    //バッチ獲得バナー済か
    private bool[] OnlyBanner = new bool[CConst.BATCH_NUM];

    // Start is called before the first frame update

    //アニメーション用変数
    private Animator anim = null;

    //SE変数
    [SerializeField] private AudioClip Player_Touch;          //プレイヤータッチ用SE変数
    [SerializeField] private AudioClip Player_Jump;           //プレイヤーを飛ばしたときのSE変数
    [SerializeField] private AudioClip Player_Hit;               //プレイヤーが壁に当たった時のSE変数
    [SerializeField] private AudioClip Player_Pull;              //プレイヤーを引っ張るときのSE変数
    [SerializeField] private AudioClip Player_Sit;               //プレイヤーがくっつく壁に当たった時のSE変数
    [SerializeField] private AudioClip Player_GetTrophy;    //プレイヤーがトロフィーを取得したときのSE変数
    [SerializeField] private AudioClip Player_Coin;             //コイン取得
    [SerializeField] private AudioClip Player_Gun_In;        //大砲入る
    [SerializeField] private AudioClip Player_Gun_Out;      //大砲発射

    AudioSource audioSource;            //オーディオソース
    AudioSource PlayerPull_audio;      //プレイヤーを引っ張った時のオーディオ取得
    AudioSource PlayerJump_audio;   //プレイヤーを離したときのSE
    AudioSource PlayerCoin_Source;

   
    void Start()
    {
        Rbody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();

        //ラインレンダラー取得
        Direction = GameObject.Find("Direction").GetComponent<LineRenderer>();

        //子のアニメーション取得
        anim = GameObject.Find("PlayerSprite").GetComponent<Animator>();

        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();
        PlayerPull_audio = GetComponent<AudioSource>();
        PlayerCoin_Source = GetComponent<AudioSource>();

        this.MainCamera = Camera.main;
        this.MainCameraTransform = this.MainCamera.transform;

        PlayFlag = false;
        ClickFlag = false;
        StopFieldFlag = false;
        GunFlag = false;
        TapFlag = false;
        StopFlag = false;

        StopFieldCount = 0;

        WallCount = 0;

        GetStageTrophy = false;

        Fly = false;

        WallCountBath = false;

        GetCoin = 0;

        for(int i = 0 ; i <= CConst.BATCH_NUM - 1 ; i++)
        {
            OnlyBanner[i] = false;
        }
        for (int i = 0; i <= CConst.STAGENUM - 1; i++)
        {
            StagePhoto[i] = CSaveBool.GetBool("Photo" + i, false);
        }
        if (StagePhoto[GoalScript.Now_StageNum - 1])
        {
            PhotoSprite.SetActive(false);
        }

        //---宮本加筆ここから------------------------------
        ShotEffect = (GameObject)Resources.Load("Effect_PlayerShot");   // プレイヤーショットエフェクトセット
        GunEnterEffect = (GameObject)Resources.Load("Effect_GunEnter"); // Gunインエフェクトセット
        GunShootEffect = (GameObject)Resources.Load("Effect_GunShoot"); // Gunショットエフェクトセット
        if (GunFind == null)
        {
            GunFind = GameObject.FindWithTag("Gun");                    // Gunオブジェクト検索
        }
        ShotPool = new GameObject("PlayerShot").transform;              // プレイヤーショットエフェクトオブジェクト生成
        GunEnterPool = new GameObject("GunEntry").transform;            // Gunに入った時のエフェクトオブジェクト生成
        GunShootPool = new GameObject("GunShoot").transform;            // Gun射出時のエフェクトオブジェクト生成
        //---宮本加筆ここまで------------------------------

    }

    //マウス座標をワールド座標に変換して取得
    private Vector3 GetMousePosition()
    {
        // マウスから取得できないZ座標を補完する
        Vector3 position = Input.mousePosition;
        position.z = this.MainCameraTransform.position.z;
        position = this.MainCamera.ScreenToWorldPoint(position);
        position.z = 0;

        return position;
    }
    //タッチ座標をワールド座標に変換して取得
    private Vector3 GetTouchPosition()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 position = touch.position;
        position.z = this.MainCameraTransform.position.z;
        position = this.MainCamera.ScreenToWorldPoint(position);
        position.z = 0;
        return position;
    }

    // Update is called once per frame
    void Update()
    {
        //エディター上の設定
        if (Application.isEditor)
        {
            //---宮本加筆ここから------------------------------
            EffectPosition = this.transform.position;       // 現在座標をエフェクト座標として取得
            if (GunFind != null)
            {
                GunRotate = GunObject.transform.rotation;   // Gunオブジェクトの回転値取得
            }
            //---宮本加筆ここまで------------------------------

            if (Input.GetMouseButtonDown(0) && !TapFlag && GunFlag)
            {
                //大砲用タップ
                TapFlag = true;
                audioSource.PlayOneShot(Player_Gun_Out);
            }

            //マウス左クリック＆タップ
            if (Input.GetMouseButtonDown(0))
            {
                //動いてないかつクリックしてない
                if (!PlayFlag && !ClickFlag)
                {
                    ClickFlag = true;
                    //マウスを左クリックした位置の取得
                    DragStart = GetMousePosition();

                    //矢印フラグ
                    this.Direction.enabled = true;
                    this.Direction.SetPosition(0, Rbody.position);  //矢印の位置
                    this.Direction.SetPosition(1, Rbody.position);
                }
            }
            //ドラッグ処理
            if (ClickFlag)
            {
                Vector2 position = GetMousePosition();
                DirectionForce = position - DragStart;

                //プレイヤーをタップしたときに鳴らす
                //audioSource.PlayOneShot(Player_Touch);


                if (DirectionForce.magnitude > MaxMagnitude)
                {
                    DirectionForce *= MaxMagnitude / DirectionForce.magnitude;
                    PlayerPull_audio.PlayOneShot(Player_Pull);
                }

                this.Direction.SetPosition(0, Rbody.position);//矢印の位置
                this.Direction.SetPosition(1, Rbody.position + DirectionForce * -1);  //矢印の向き

            }

            //マウスを離したとき
            if (Input.GetMouseButtonUp(0))
            {
                //引っ張るSEを止める
                PlayerPull_audio.Stop();
                
                //クリックフラグがオンなら
                if (ClickFlag)
                {
                    StartPosition = transform.position;

                    ClickFlag = false;

                    if (DirectionForce.magnitude >= MinMagnitude)
                    {
                        //プレイヤーを飛ばすSE
                        audioSource.PlayOneShot(Player_Jump);

                        PlayFlag = true;

                        StopFieldFlag = false;
                        //弾く
                        Flip(DirectionForce * Power * -1);

                        //---宮本加筆ここから------------------------------
                        // ショットエフェクト発生
                        GetPlayerShotObject(ShotEffect, EffectPosition, Quaternion.identity);
                        //---宮本加筆ここまで------------------------------

                        //矢印オフ
                        this.Direction.enabled = false;


                        //回転アニメーションオン
                        anim.SetBool("Move", true);
                    }
                }
            }
        }
        //=================================
        //実機デバッグ
        //=================================
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                //---宮本加筆ここから------------------------------
                EffectPosition = this.transform.position;       // 現在座標をエフェクト座標として取得
                if (GunFind != null)
                {
                    GunRotate = GunObject.transform.rotation;   // Gunオブジェクトの回転値取得
                }
                //---宮本加筆ここまで------------------------------

                if (Input.GetMouseButtonDown(0) && !TapFlag && GunFlag)
                {
                    //大砲用タップ
                    TapFlag = true;
                }

                //マウス左クリック＆タップ
                if (touch.phase == TouchPhase.Moved)
                {
                    //動いてないかつクリックしてない
                    if (!PlayFlag && !ClickFlag)
                    {
                        Debug.Log("タップしてますuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");
                        ClickFlag = true;
                        //DragStart = GetMousePosition();
                        //タップ開始した位置
                        DragStart = GetTouchPosition();
                        
                        //プレイヤーをタップしたときに鳴らす
                        audioSource.PlayOneShot(Player_Touch);

                        //矢印フラグ
                        this.Direction.enabled = true;
                        this.Direction.SetPosition(0, Rbody.position);  //矢印の位置
                        this.Direction.SetPosition(1, Rbody.position);
                    }
                }
                //ドラッグ処理
                if (ClickFlag)
                {
                    //ドラッグした後のポジション取得
                    Vector2 position = GetTouchPosition();
                    DirectionForce = position - DragStart;

                    Debug.Log("ドラッグキめてるuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");

                    if (DirectionForce.magnitude > MaxMagnitude)
                    {
                        DirectionForce *= MaxMagnitude / DirectionForce.magnitude;
                        PlayerPull_audio.PlayOneShot(Player_Pull);
                    }

                    this.Direction.SetPosition(0, Rbody.position);//矢印の位置
                    this.Direction.SetPosition(1, Rbody.position + DirectionForce * -1);  //矢印の向き
                }
                

                if(touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("うべえああああああああああああああああああああああああああああああああああああああああああああ");
                }

                //指を離したとき
                if (touch.phase == TouchPhase.Ended)
                {
                    //クリックフラグがオンなら
                    if (ClickFlag)
                    {
                        PlayerPull_audio.Stop();
                        audioSource.PlayOneShot(Player_Jump);

                        StartPosition = transform.position;
                        ClickFlag = false;

                        if (DirectionForce.magnitude >= MinMagnitude)
                        {
                            //プレイヤーを飛ばすSE
                            PlayerJump_audio.PlayOneShot(Player_Jump);

                            PlayFlag = true;

                            StopFieldFlag = false;

                            //弾く
                            Flip(DirectionForce * Power * -1);

                            //---宮本加筆ここから------------------------------
                            // ショットエフェクト発生
                            GetPlayerShotObject(ShotEffect, EffectPosition, Quaternion.identity);
                            //---宮本加筆ここまで------------------------------

                            //矢印オフ
                            this.Direction.enabled = false;


                            //回転アニメーションオン
                            anim.SetBool("Move", true);
                        }
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        Velocity.y = Rbody.velocity.y;
        Velocity.x = Rbody.velocity.x;

        //***********************
        //遅くなったらとめる
        //***********************
        if (Velocity.y == 0 && Velocity.x <= 12 && Velocity.x >= -12 && PlayFlag && !GunFlag)
        {
            if (!WallCountBath)
            {
                //連続壁激突
                WallCount = 0;
            }

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

            //飛び始めと比較
            Vector2 NowPosition = transform.position;
            var diff = NowPosition - StartPosition;
            if (diff.magnitude > CConst.FLY_DISTANCE)
            {
                Fly = true;
            }
        }

        //********************************************************************
        //くっつくギミック
        //********************************************************************
        if (StopFieldFlag)
        {
            if (!OnlyStopCount)
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
            GetGunShootObject(GunShootEffect, EffectPosition, Quaternion.Euler(GunRotate.x, GunRotate.y, GunRotate.z * 100));
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
        if (PlayCount <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        //*****************
        // 壁衝突カウント
        //*****************
        if(WallCount >= 2)
        {
            WallCountBath = true;
            WallCount = 2;
        }

        //***********************************************
        //バッチの初獲得
        //***********************************************

        if (PhotoSprite.activeSelf)
        {
            if (StagePhoto[GoalScript.Now_StageNum - 1])
            {
                PhotoSprite.SetActive(false);
            }
        }

        if (WallFlag && !GoalScript.GetBatch[0] && !OnlyBanner[0])
        {
            OnlyBanner[0] = true;
            BannerScript.Get = true;
        }
        if (WallCount >= 2 && !GoalScript.GetBatch[1] && !OnlyBanner[1])
        {
            OnlyBanner[1] = true;
            BannerScript.Get = true;
        }
        if (Fly && !GoalScript.GetBatch[2] && !OnlyBanner[2])
        {
            OnlyBanner[2] = true;
            BannerScript.Get = true;
        }
        if (StopFieldCount >= 1 && GoalScript.Now_StageNum == 1 && !GoalScript.GetBatch[3] && !OnlyBanner[3])
        {
            OnlyBanner[3] = true;
            BannerScript.Get = true;
        }
        if (StopFieldCount >= 1 && GoalScript.Now_StageNum == 2 && !GoalScript.GetBatch[4] && !OnlyBanner[4])
        {
            OnlyBanner[4] = true;
            BannerScript.Get = true;
        }
        if (StopFieldCount >= 1 && GoalScript.Now_StageNum == 3 && !GoalScript.GetBatch[5] && !OnlyBanner[5])
        {
            OnlyBanner[5] = true;
            BannerScript.Get = true;
        }
        if (StopFieldCount >= 1 && GoalScript.Now_StageNum == 4 && !GoalScript.GetBatch[6] && !OnlyBanner[6])
        {
            OnlyBanner[6] = true;
            BannerScript.Get = true;
        }
        if (Wind && !GoalScript.GetBatch[7] && !OnlyBanner[7])
        {
            OnlyBanner[7] = true;
            BannerScript.Get = true;
        }
        if (GunTrophyFlag && !GoalScript.GetBatch[8] && !OnlyBanner[8])
        {
            OnlyBanner[8] = true;
            BannerScript.Get = true;
        }
        if (BreakBlockCount >= 1 && !GoalScript.GetBatch[9] && !OnlyBanner[9])
        {
            OnlyBanner[9] = true;
            BannerScript.Get = true;
        }
        if (GetStageTrophy && !GoalScript.GetBatch[10] && !OnlyBanner[10])
        {
            OnlyBanner[10] = true;
            BannerScript.Get = true;
        }
        if (GetCoin >= GoalScript.MaxCoin && !GoalScript.GetBatch[12] && !OnlyBanner[12])
        {
            GoalScript.AllCoin[GoalScript.Now_StageNum - 1] = true;
            if (GoalScript.AllCoin[0] && GoalScript.AllCoin[1] && GoalScript.AllCoin[2] && GoalScript.AllCoin[3] && GoalScript.AllCoin[4])
            {
                OnlyBanner[12] = true;
                BannerScript.Get = true;
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
        audioSource.PlayOneShot(Player_Hit);        //プレイヤーが壁に当たったとき

        if (collision.gameObject.tag == StopBlockTag && !StopFieldFlag)
        {
            StopFieldFlag = true;
            audioSource.PlayOneShot(Player_Sit);        //くっつくSE
        }

        // 壁にぶつかる
        if (collision.gameObject.tag == "Wall")
        {
            WallFlag = true;
            if(!WallCountBath)
            {
                WallCount++;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == GunTag && !GunFlag && !CoolTime)
        {
            //ここにSE*********************************
            GunFlag = true;
            GunTrophyFlag = true;

            //---宮本加筆ここから------------------------------
            // 大砲エントリーエフェクト発生
            GetGunEnterObject(GunEnterEffect, EffectPosition, Quaternion.Euler(GunRotate.x, GunRotate.y, GunRotate.z * 100));
            //---宮本加筆ここまで------------------------------
        }

        //トロフィーゲット
        if (collider.gameObject.name == "Trophy")
        {
            audioSource.PlayOneShot(Player_GetTrophy);      //トロフィーをとった時のSE
            GetStageTrophy = true;
            TrophyObject.SetActive(false);

        }

        //風
        if (collider.gameObject.name == "Wind")
        {
            Wind = true;
        }

        //コイン
        if (collider.gameObject.tag == "Coin")
        {
            audioSource.PlayOneShot(Player_Coin);
        }
    }

    //---宮本加筆ここから------------------------------
    // // ゲームオブジェクトのアクティブ判別と生成 // //
    void GetPlayerShotObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (Transform transform in ShotPool)
        {
            // オブジェクトが非アクティブなら使いまわし
            if (!transform.gameObject.activeSelf)
            {
                transform.SetPositionAndRotation(pos, qua);
                transform.gameObject.SetActive(true);
                return;
            }
        }

        // 非アクティブなオブジェクトがなければ生成する
        Instantiate(obj, pos, qua, ShotPool);

    }
    void GetGunEnterObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (Transform transform in GunEnterPool)
        {
            // オブジェクトが非アクティブなら使いまわし
            if (!transform.gameObject.activeSelf)
            {
                transform.SetPositionAndRotation(pos, qua);
                transform.gameObject.SetActive(true);
                return;
            }
        }

        // 非アクティブなオブジェクトがなければ生成する
        Instantiate(obj, pos, qua, GunEnterPool);
    }

    void GetGunShootObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (Transform transform in GunShootPool)
        {
            // オブジェクトが非アクティブなら使いまわし
            if (!transform.gameObject.activeSelf)
            {
                transform.SetPositionAndRotation(pos, qua);
                transform.gameObject.SetActive(true);
                return;
            }
        }

        // 非アクティブなオブジェクトがなければ生成する
        Instantiate(obj, pos, qua, GunShootPool);
    }
    //---宮本加筆ここまで------------------------------
}
