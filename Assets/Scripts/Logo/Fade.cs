/*-------------------------------------
フェード系処理(Fade.cs)

date  :2017.03.14
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   // Image
using UnityEngine.SceneManagement;      // SceneManager

public class Fade : MonoBehaviour {
    /* 変数の宣言 */
    [Range(0.005f, 0.015f)]
    public float m_speed = 0.01f;
    [Range(0.0f,5.0f)]
    public float m_delayTime;

    float m_red, m_green, m_blue;
    float m_alpha;

    bool m_is_fadein  = true;   // フェードイン中か
    bool m_is_fadeout = false;  // フェードアウト中か
    bool m_is_jump    = false;  // フェード中にキーを押して飛ばしたか

    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        m_red   = GetComponent<Image>().color.r;
        m_green = GetComponent<Image>().color.g;
        m_blue  = GetComponent<Image>().color.b;
        m_alpha = GetComponent<Image>().color.a;
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        StartCoroutine("FadeInOut");
    }

    /*------------------------------------
    FadeInOut
    
    summary:フェード処理
    param  :なし(void)
    return :(IEnumerator)
    ------------------------------------*/
    IEnumerator FadeInOut()
    {
        if (m_is_fadein)
        {
            if (Input.anyKey)       // 強制フェードアウト
            {
                m_is_fadein = false;
                m_is_fadeout = true;

                m_is_jump = true;
            }
            else
                FadeIn();
        }

        if (!m_is_jump)     // 強制時は待たない
            yield return new WaitForSeconds(m_delayTime);

        if (m_is_fadeout)
            FadeOut();

        yield return null;
    }

    /*------------------------------------
    FadeIn
    
    summary:フェードイン処理
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void FadeIn()
    {
        GetComponent<Image>().color = new Color(m_red, m_green, m_blue, m_alpha);
        m_alpha -= m_speed;

        if (m_alpha <= 0.0f)
        {
            m_is_fadein = false;
            m_is_fadeout = true;
        }
    }

    /*------------------------------------
    FadeOut
    
    summary:フェードアウト処理
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void FadeOut()
    {
        GetComponent<Image>().color = new Color(m_red, m_green, m_blue, m_alpha);
        m_alpha += m_speed;

        /* タイトル画面へ */
        if (m_alpha >= 1.0f)
            StartCoroutine("ChangeTitleScene");
    }

    /*------------------------------------
    ChangeTitleScene
    
    summary:タイトルシーン遷移
    param  :なし(void)
    return :(IEnumerator)
    ------------------------------------*/
    IEnumerator ChangeTitleScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Title");
    }
}
