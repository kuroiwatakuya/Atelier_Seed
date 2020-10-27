using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common;

//*********************************************
// ステージ選択、タイトルへの遷移用スクリプト
//*********************************************

public class CScene_Change : MonoBehaviour
{
    bool OnlySelect = true;     // キーの多重処理防止

    bool FirstOp;
    
    // Start is called before the first frame update
    void Start()
    {
        FirstOp = CSaveBool.GetBool("Opening", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Go_StageSelect()
    {
        if (OnlySelect)
        {
            Time.timeScale = 1f;
            CFadeManager.FadeOut(CConst.SCENE_STAGE_SELECT);
            OnlySelect = false;
        }
    }

    public void Go_Title()
    {
        if (OnlySelect)
        {
            Time.timeScale = 1f;
            CFadeManager.FadeOut(CConst.SCENE_TITLE);
            OnlySelect = false;
        }
    }

    public void Go_End()
    {
        if (OnlySelect)
        {
            Time.timeScale = 1f;
            CFadeManager.FadeOut(CConst.SCENE_ENDING);
            OnlySelect = false;
        }
    }

    public void Go_Album()
    {
        if (OnlySelect)
        {
            Time.timeScale = 1f;
            CFadeManager.FadeOut(CConst.SCENE_COLLECTION);
            OnlySelect = false;
        }
    }

    public void TitleTap()
    {
        if (OnlySelect)
        {
            if(!FirstOp)
            {
                CFadeManager.FadeOut(CConst.SCENE_OPENING);
                FirstOp = true;
                CSaveBool.SetBool("Opening", FirstOp);
            }
            else
            {
                CFadeManager.FadeOut(CConst.SCENE_STAGE_SELECT);
            }
            
            OnlySelect = false;
        }
    }

}
