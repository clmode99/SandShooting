﻿/*-------------------------------------
赤スコアの結果(RedResult.cs)

date  :2017.04.04
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // Text

public class RedResult : MonoBehaviour {
    /* 変数の宣言 */
    public int m_Unit;      // 単位

    int m_score;
    /* 関数の定義 */
    /*------------------------------------
    Start
    
    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        /* 赤スコア取得 */
        PlayManager pm = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
        int score = pm.GetScore(COLOR.RED);

        /* 赤スコア計算 */
        m_score = score * m_Unit;

        Text text = GetComponent<Text>();

        /* スコアによって表記変える */
        if (score < 10)     // 10以下
            text.text = "  " + score + " ×  " + m_Unit + "   = " + m_score;
        else
            text.text = score + " ×  " + m_Unit + "   = " + m_score;

    }

    /*------------------------------------
    GetScore
    
    summary:スコアを取得
    param  :なし(void)
    return :スコア(int)
    ------------------------------------*/
    public int GetScore()
    {
        return m_score;
    }
}
