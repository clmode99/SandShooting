/*-------------------------------------
背景の管理(BgControler.cs)

date  :2017.03.30
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgControler : MonoBehaviour {
    /* 変数の宣言 */
    public GameObject m_Bg;           // 背景
    public float m_ScrollSpeedY;      // スクロール速さＹ

    bool m_is_create;           // 背景を生成したか？

    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        m_is_create = false;        // 生成してない
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        /* 移動 */
        transform.position = new Vector3(transform.position.x, transform.position.y - (m_ScrollSpeedY * Time.deltaTime), transform.position.z);

        /* 新しい背景生成 */
        if (transform.position.y <= -6.0f && m_is_create == false)      // -6.0は画像の上が画面の上にぴったりくるとき
        {
            Transform start_trans = transform.parent.gameObject.transform;       // 背景のスタート座標取得
            Instantiate(m_Bg, start_trans.position, Quaternion.identity, start_trans);
            m_is_create = true;
        }

        /* 背景削除 */
        if (transform.position.y <= -16.1f)     // -16.1は画像の上が画面の下にくるとき
            Destroy(gameObject);
    }
}
