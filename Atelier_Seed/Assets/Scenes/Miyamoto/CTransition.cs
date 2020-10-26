using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTransition : UnityEngine.UI.Graphic, InterfaceFade
{
    // マスクするルール画像
    [SerializeField] private Texture MaskTexture = null;

    // マスク範囲
    [SerializeField, Range(0, 1)] private float CutoutRange;


    // マスク範囲の入出力
    public float Range
    {
        get
        {
            return CutoutRange;
        }

        set
        {
            CutoutRange = value;
            UpdateMaskCutout(CutoutRange);
        }
    }


    // 初期化
    protected override void Start()
    {
        // 親の初期化を呼ぶ
        base.Start();

        // マスク範囲の更新
        UpdateMaskTexture(MaskTexture);
    }


    // マスク範囲の更新
    private void UpdateMaskCutout(float range)
    {
        // 見えるようにする
        enabled = true;

        // マテリアルの範囲の設定
        material.SetFloat("_Range", 1 - range);


        // ゼロ以下で見えなくする
        if (range <= 0)
        {
            this.enabled = false;
        }
    }


    public void UpdateMaskTexture(Texture texture)
    {
        // テクスチャ設定
        material.SetTexture("_MaskTex", texture);

        // カラー設定
        material.SetColor("_Color", color);
    }


#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        UpdateMaskCutout(Range);
        UpdateMaskTexture(MaskTexture);
    }
#endif



    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
