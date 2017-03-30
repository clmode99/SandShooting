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

        m_total_time = m_LimitTimeSecond;
    }

    // Update is called once per frame
    void Update()
    {
        m_total_time -= Time.deltaTime;

        if (m_total_time <= 10.0f)
            m_text.color = Color.red;

        m_text.text = ((int)m_total_time).ToString();
    }
}
