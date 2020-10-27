using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticCamera : MonoBehaviour
{
    private Camera camera;

    private float Height = 720;
    private float Width = 1440;

    private float PixelPerUnit = 100.0f;

    private void Awake()
    {
        float aspect = (float)Screen.width / (float)Screen.height;
        float bgAspect = Height / Width;

        //カメラのコンポーネント追加
        camera = GetComponent<Camera>();
        // カメラのorthographicSizeを設定
        camera.orthographicSize = (Height / 2f / PixelPerUnit);


        if (bgAspect > aspect)
        {
            // 倍率
            float bgScale = Height / Screen.height;
            // viewport rectの幅
            float camWidth = Width / (Screen.width * bgScale);
            // viewportRectを設定
            camera.rect = new Rect((1f - camWidth) / 2f, 0f, camWidth, 1f);
        }
        //else
        //{
        //    // 倍率
        //    float bgScale = Width / Screen.width;
        //    // viewport rectの幅
        //    float camHeight = Height / (Screen.height * bgScale);
        //    // viewportRectを設定
        //    camera.rect = new Rect(0f, (1f - camHeight) / 2f, 1f, camHeight);
        //}
    }
}