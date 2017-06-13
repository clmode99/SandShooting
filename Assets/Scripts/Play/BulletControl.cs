/*-------------------------------------
弾の操作(BulletControl.cs)

date  :2017.03.22
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    // 速さ
    public float m_EasySpeedY;
    public float m_NormalSpeedY;
    public float m_HardSpeedY;

    float m_speedY;

    Rigidbody2D m_rb;

    /* 関数の定義 */
    /*------------------------------------
    Start
    
    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        /* 難易度によって速度変化 */
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        switch (gm.GetLevel())
        {
            case LEVEL.EASY:
                m_speedY = m_EasySpeedY;
                break;

            case LEVEL.NORMAL:
                m_speedY = m_NormalSpeedY;
                break;

            case LEVEL.HARD:
                m_speedY = m_HardSpeedY;
                break;
        }
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {}

    /*------------------------------------
    FixedUpdate
    
    summary:特定のFPSでの更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void FixedUpdate()
    {
        m_rb.velocity = new Vector2(0.0f, m_speedY);
    }

    /*------------------------------------
    OnCollisionEnter2D
    
    summary:衝突処理
    param  :衝突したデータ(Collision2D)
    return :なし(void)
    ------------------------------------*/
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            Destroy(gameObject);
    }

    /*------------------------------------
    OnTriggerEnter2D
    
    summary:トリガー処理
    param  :衝突したデータ(Collision2D)
    return :なし(void)
    ------------------------------------*/
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Border")
            Destroy(gameObject);
    }
}
