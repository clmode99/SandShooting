/*-------------------------------------
制限時間(LimitTimer.cs)

date  :2017.03.30
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // Text

public class LimitTimer : MonoBehaviour {
    /* 変数の宣言 */
    public uint m_LimitTimeSecond;        // 制限時間(秒)

    float m_total_time;     // トータル時間
    Text  m_text;

    bool m_is_start;
    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        m_text = GetComponent<Text>();
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        m_total_time = (Mathf.Max(0.0f, m_total_time - Time.deltaTime));

        /* 10秒切ったら赤にする */
        if (m_total_time <= 10.0f)
            m_text.color = Color.red;

        m_text.text = ((int)m_total_time).ToString();
    }

    /*------------------------------------------
    IsUpTime
    
    summary:時間切れか
    param  :なし(void)
    return :時間切れ(true)、切れてない(false)
    ------------------------------------------*/
    public bool IsUpTime()
    {
        /* トータル時間初期化(Start()前でも大丈夫なように対策) */
        if (!m_is_start)
        {
            m_total_time = m_LimitTimeSecond;
            m_is_start = true;
        }

        return (m_total_time <= 0.0f) ? true : false;
    }
}
