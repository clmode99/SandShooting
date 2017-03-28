/*-------------------------------------
プレイヤーの操作(PlayerControl.cs)

date  :2017.03.22
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    /* 変数の宣言 */
    public float m_speedX;      // 速さ
    public GameObject m_bullet; // 弾

    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {}

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        /* キー入力 */
       if (Input.GetKey(KeyCode.LeftArrow))  MoveLeft();
       if (Input.GetKey(KeyCode.RightArrow)) MoveRight();
       if (Input.GetKeyDown(KeyCode.Space))  Shot();
    }

    /*------------------------------------
    MoveLeft
    
    summary:左移動
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void MoveLeft()
    {
        float speedX = transform.position.x - m_speedX;
        Move(speedX);
    }

    /*------------------------------------
    MoveRight
    
    summary:右移動
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void MoveRight()
    {
        float speedX = transform.position.x + m_speedX;
        Move(speedX);
    }

    /*------------------------------------
    Move
    
    summary:移動
    param  :速度Ｘ(float)
    return :なし(void)
    ------------------------------------*/
    void Move(float spdX)
    {
        transform.position = new Vector3(spdX, transform.position.y, transform.position.z);
    }

    /*------------------------------------
    Shot
    
    summary:発射
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Shot()
    {
        /* 弾の発射位置調整 */
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float player_height_half = sr.bounds.size.y / 2.0f;  // プレイヤーの画像高さの半分

        sr = m_bullet.GetComponent<SpriteRenderer>();
        float bullet_height_half = sr.bounds.size.y / 2.0f;  // 弾の画像高さの半分

        float bullet_pos_adjust = player_height_half + bullet_height_half;  // 発射位置調整値

        /* 弾の生成(発射) */
        Instantiate(m_bullet, new Vector3(transform.position.x, transform.position.y + bullet_pos_adjust, transform.position.z), Quaternion.identity);
    }

}
