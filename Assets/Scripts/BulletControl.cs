/*-------------------------------------
弾の操作(BulletControl.cs)

date  :2017.03.22
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float m_speedY;  // 速さ

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
        Destroy(gameObject);
    }
}
