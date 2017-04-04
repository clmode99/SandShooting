/*-------------------------------------
スコア管理(ScoreConrol.cs)

date  :2017.03.30
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // Text

public class ScoreControl : MonoBehaviour {
    /* 変数の宣言 */
    Text m_text;
    int  m_score;

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
        m_score = 0;
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        m_text.text = m_score.ToString();
    }

    /*------------------------------------
    AddScore
    
    summary:スコア追加
    param  :スコア(int)
    return :なし(void)
    ------------------------------------*/
    public void AddScore(int score)
    {
        m_score += score;
    }
}
