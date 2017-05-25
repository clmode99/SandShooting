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
    /* 変数の宣言 */
    SoundManager m_sm;
    SpriteRenderer[]      m_sr = new SpriteRenderer[(int)TITLE_BUTTON_TYPE.BUTTON_NUM];

    // TITLE_BUTTON_TYPEのint版
    const int START = (int)TITLE_BUTTON_TYPE.START;
    const int END   = (int)TITLE_BUTTON_TYPE.END;

    Color m_select_color   = new Color(1.0f, 1.0f, 1.0f, 1.0f);       // 選択時
    Color m_noselect_color = new Color(1.0f, 1.0f, 1.0f, 0.747f);     // 未選択時

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

        m_sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        m_sm.PlayBGM(BGM_TYPE.TITLE);

        m_sr[START] = GameObject.FindGameObjectWithTag("StartButton").GetComponent<SpriteRenderer>();
        m_sr[END]   = GameObject.FindGameObjectWithTag("EndButton").GetComponent<SpriteRenderer>();

        /* 最初はスタートが選択されてる状態 */
        m_sr[START].color = m_select_color;
        m_sr[END].color   = m_noselect_color;
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        SoundManager sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        FadeOut      fo  = GameObject.Find("Canvas/Panel").GetComponent<FadeOut>();

        if (Input.GetButtonDown("Submit"))
        {
            // START
            if (m_sr[0].color == m_select_color)
            {
                StartCoroutine("ChangePlayScene");
                sm.PlaySubmitSE();
                sm.FadeStop();
                fo.StartFadeout();
            }
            // END
            else
                Application.Quit();
        }

        /* ボタン制御 */
        float move = Input.GetAxisRaw("Vertical");
        if     (move > 0.0f) SelectButton(TITLE_BUTTON_TYPE.START);     // 上
        else if(move < 0.0f) SelectButton(TITLE_BUTTON_TYPE.END);       // 下
    }

    /*--------------------------------
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

    /*-----------------------------------------
    SelectButton
    
    summary:選択してるボタンを変更する
    param  :変更対象ボタン(TITLE_BUTTON_TYPE)
    return :なし(void)
    -----------------------------------------*/
    void SelectButton(TITLE_BUTTON_TYPE type)
    {
        // TODO:処理が冗長な気がする。コンパクトにしたい(変更してるところとか)
        switch (type)
        {
            case TITLE_BUTTON_TYPE.START:
                if (m_sr[START].color == m_select_color)
                    break;

                m_sr[START].color = m_select_color;
                m_sr[END].color   = m_noselect_color;
                m_sm.PlaySelectSE();
                break;

            case TITLE_BUTTON_TYPE.END:
                if (m_sr[START].color == m_noselect_color)
                    break;

                m_sr[START].color = m_noselect_color;
                m_sr[END].color   = m_select_color;
                m_sm.PlaySelectSE();
                break;
        }

    }
}
