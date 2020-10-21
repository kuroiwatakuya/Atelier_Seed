using System.Collections;
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

    //かべぶつかったか
    public bool WallFlag;
    //壁にぶつかった回数
    public int WallCount;
    //飛ぶ
    public bool Fly;
    //飛び始めた座標
    private Vector2 StartPosition;
    //風衝突
    public bool Wind;

    //取得コイン
    public int GetCoin;

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
    AudioSource audioSource;            //オーディオソース

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
        var position = Input.mousePosition;
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
            }

            //マウス左クリック＆タップ
            if (Input.GetMouseButtonDown(0))
            {
                //動いてないかつクリックしてない
                if (!PlayFlag && !ClickFlag)
                {
                    ClickFlag = true;
                    DragStart = GetMousePosition();

                    //プレイヤーをタップしたときに鳴らす
                    audioSource.PlayOneShot(Player_Touch);

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

                    if (DirectionForce.magnitude > MaxMagnitude)
                    {
                        DirectionForce *= MaxMagnitude / DirectionForce.magnitude;
                        audioSource.PlayOneShot(Player_Pull);
                    }

                    this.Direction.SetPosition(0, Rbody.position);//矢印の位置
                    this.Direction.SetPosition(1, Rbody.position + DirectionForce * -1);  //矢印の向き

                }
            }

            //マウスを離したとき
            if (Input.GetMouseButtonUp(0))
            {
                //プレイヤーを飛ばすSE
                audioSource.PlayOneShot(Player_Jump);

                //クリックフラグがオンなら
                if (ClickFlag)
                {
                    StartPosition = transform.position;

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
                if (Input.GetMouseButtonDown(0))
                {
                    //動いてないかつクリックしてない
                    if (!PlayFlag && !ClickFlag)
                    {
                        Debug.Log("タップしてますuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");
                        ClickFlag = true;
                        //DragStart = GetMousePosition();
                        DragStart = touch.position;
                        
                        //プレイヤーをタップしたときに鳴らす
                        audioSource.PlayOneShot(Player_Touch);

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
                        Vector2 position = touch.position;
                        DirectionForce = position - DragStart;

                        Debug.Log("ドラッグキめてるuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");

                        if (DirectionForce.magnitude > MaxMagnitude)
                        {
                            DirectionForce *= MaxMagnitude / DirectionForce.magnitude;
                            audioSource.PlayOneShot(Player_Pull);
                        }

                        this.Direction.SetPosition(0, Rbody.position);//矢印の位置
                        this.Direction.SetPosition(1, Rbody.position + DirectionForce * -1);  //矢印の向き
                    }
                }

                if(touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("うべえああああああああああああああああああああああああああああああああああああああああああああ");
                }

                //マウスを離したとき
                if (Input.GetMouseButtonUp(0))
                {
                    //プレイヤーを飛ばすSE
                    audioSource.PlayOneShot(Player_Jump);

                    Debug.Log("離した");

                    //クリックフラグがオンなら
                    if (ClickFlag)
                    {
                        StartPosition = transform.position;
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

        //遅くなったらとめる
        if (Velocity.y == 0 && Velocity.x <= 12 && Velocity.x >= -12 && PlayFlag && !GunFlag)
        {
            //連続壁激突
            WallCount = 0;

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
        if (collision.gameObject.tag == "PlayerCrush")
        {
            WallFlag = true;
            WallCount++;
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
            GetGunEnterObject(GunEnterEffect, EffectPosition, Quaternion.Euler(GunRotate.x, GunRotate.y, GunRotate.z * 100));
            //---宮本加筆ここまで------------------------------
        }

        //トロフィーゲット
        if (collider.gameObject.name == "Trophy")
        {
            audioSource.PlayOneShot(Player_GetTrophy);      //トロフィーをとった時のSE
            GetStageTrophy = true;
        }

        //風
        if (collider.gameObject.name == "Wind")
        {
            Wind = true;
        }

        //コイン
        //風
        if (collider.gameObject.name == "Coin")
        {
            GetCoin++;
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
