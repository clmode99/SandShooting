/*-------------------------------------
トータルスコア(TotalScoreResult.cs)

date  :2017.04.04
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // Text

public class TotalScoreResult : MonoBehaviour {
    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        /* トータルスコア取得 */
        GameManager gm  = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        int total_score = gm.GetTotalScore();

        Text text = GetComponent<Text>();
        text.text = total_score.ToString();
    }
}
