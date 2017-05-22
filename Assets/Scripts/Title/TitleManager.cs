/*-------------------------------------
タイトル画面の管理(TitleManager.cs)

date  :2017.04.04
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      // SceneManager

public class TitleManager : MonoBehaviour {
    public Sprite m_BgKey;
    public Sprite m_bgButton;

    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        int controller_num = Input.GetJoystickNames().Length;       // 接続されてるコントローラーの数
        SpriteRenderer sr = GameObject.Find("Bg").GetComponent<SpriteRenderer>();

        if (controller_num > 0)     // コントローラー版
            sr.sprite = m_bgButton;
        else                        // キーボード版
            sr.sprite = m_BgKey;
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        FadeOut fo = GameObject.Find("Canvas/Panel").GetComponent<FadeOut>();

        if (Input.GetButtonDown("Start"))
        {
            StartCoroutine("ChangePlayScene");
            fo.StartFadeout();
        }
    }


    /*------------------------------------
    ChangePlayScene

    summary:プレイ画面に遷移
    param  :なし(void)
    return :(IEnumerator)
------------------------------------*/
    IEnumerator ChangePlayScene()
    {
        yield return new WaitForSeconds(1.7f);      // この間にフェードアウト処理

        SceneManager.LoadScene("Play");

        yield break;
    }
}
