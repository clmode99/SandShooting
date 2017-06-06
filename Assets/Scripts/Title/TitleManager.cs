/*-------------------------------------
タイトル画面の管理(TitleManager.cs)

date  :2017.04.04
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;      // SceneManager

public class TitleManager : MonoBehaviour {
    /* 変数の宣言 */
    public GameObject m_Player;

    SoundManager m_sm;
    SpriteRenderer[] m_sr = new SpriteRenderer[(int)TITLE_BUTTON_TYPE.BUTTON_NUM];
    Transform[]      m_tf = new Transform[(int)TITLE_BUTTON_TYPE.BUTTON_NUM];

    // TITLE_BUTTON_TYPEのint版
    const int START = (int)TITLE_BUTTON_TYPE.START;
    const int END   = (int)TITLE_BUTTON_TYPE.END;

    Color m_select_color   = new Color(1.0f, 1.0f, 1.0f, 1.0f);       // 選択時
    Color m_noselect_color = new Color(1.0f, 1.0f, 1.0f, 0.747f);     // 未選択時
    Vector3 m_select_scale   = new Vector3(2.5f, 2.2f, 1.0f);
    Vector3 m_noselect_scale = new Vector3(2.5f * 0.75f, 2.2f * 0.75f, 1.0f);

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

        GameObject start_button = GameObject.FindGameObjectWithTag("StartButton");
        GameObject end_button = GameObject.FindGameObjectWithTag("EndButton");

        m_sr[START] = start_button.GetComponent<SpriteRenderer>();
        m_sr[END] = end_button.GetComponent<SpriteRenderer>();
        m_tf[START] = start_button.GetComponent<Transform>();
        m_tf[END] = end_button.GetComponent<Transform>();

        /* 最初はスタートが選択されてる状態 */
        m_sr[START].color = m_select_color;
        m_sr[END].color = m_noselect_color;

        m_tf[START].localScale = m_select_scale;
        m_tf[END].localScale = m_noselect_scale;

        /* 難易度選択時から遷移したとき */
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

        /* 難易度から遷移 */
        if (gm.GetOldScene() == SCENE.SCENE_LEVEL)
            Destroy(GameObject.FindGameObjectWithTag("LevelManager"));
        /* 通常時 */
        else
        {
            GameObject player = Instantiate(m_Player, m_Player.transform.position, Quaternion.identity);
            DontDestroyOnLoad(player);

            m_sm.PlayBGM(BGM_TYPE.TITLE);

            /* フェードイン処理有効 */
            //GameObject.FindGameObjectWithTag("TitlePanel").GetComponent<FadeIn>().enabled = true;
        }
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            // START
            if (m_sr[START].color == m_select_color)
            {
                SceneManager.LoadScene("Level");
                m_sm.PlaySubmitSE();
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

                m_tf[START].localScale = m_select_scale;
                m_tf[END].localScale = m_noselect_scale;

                m_sm.PlaySelectSE();

                break;

            case TITLE_BUTTON_TYPE.END:
                if (m_sr[START].color == m_noselect_color)
                    break;

                m_sr[START].color = m_noselect_color;
                m_sr[END].color   = m_select_color;

                m_tf[START].localScale = m_noselect_scale;
                m_tf[END].localScale   = m_select_scale;

                m_sm.PlaySelectSE();
                break;
        }

    }

}
