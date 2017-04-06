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

    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {

    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        // TODO:フェードアウトしてシーン遷移したい
        if (Input.GetKey(KeyCode.Space))
            SceneManager.LoadScene("Play");
    }
}
