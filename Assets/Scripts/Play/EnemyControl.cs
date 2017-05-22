/*-------------------------------------
敵の操作(EnemyControl.cs)

date  :2017.03.23
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    /* 変数の宣言 */
    public GameObject[] m_Particle;

    public AudioClip   m_CollisionSe;
    public AudioClip   m_FireSe;
    public AudioClip   m_WingSe;
    public AudioClip[] m_IceSe;     // 氷は２つ

    SpriteRenderer m_sr;
    EnemyCreater   m_enemy_creater;

    COLOR m_current_color;   // 現在の色

    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        /* -----------------------------------------------------------
           ※m_srはSetEnemyAttribute()で初期化
           (Start()よりも先にSetEnemyAttribute()が呼び出されるため) 
        ------------------------------------------------------------*/
        GameObject enemy_creater_obj = GameObject.FindGameObjectWithTag("EnemyCreater");
        m_enemy_creater = enemy_creater_obj.GetComponent<EnemyCreater>();

        Debug.Assert(m_IceSe.Length == 2);      // ※IceSeチェック
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        Debug.Assert(m_Particle.Length == 3);        // パーティクルの数制限(色の数:3)
    }

    /*------------------------------------
    OnCollisionEnter2D
    
    summary:衝突処理
    param  :衝突したデータ(Collision2D)
    return :なし(void)
    ------------------------------------*/
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(m_CollisionSe, 0.75f);

            ChangeEnemyColor();
        }
    }

    /*------------------------------------
    ChangeEnemyColor
    
    summary:敵の色変更
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void ChangeEnemyColor()
    {
        switch (m_current_color)
        {
            case COLOR.RED:
                SetEnemyAttribute(m_enemy_creater.m_EnemyGreen, COLOR.GREEN);
                break;

            case COLOR.GREEN:
                SetEnemyAttribute(m_enemy_creater.m_EnemyBlue, COLOR.BLUE);
                break;

            case COLOR.BLUE:
            case COLOR.DUMMY:
                SetEnemyAttribute(m_enemy_creater.m_EnemyRed, COLOR.RED);
                break;

            case COLOR.NONE:
                break;

            default:
                Debug.Log("Error!!(EnemyControl.cs/OnCollisionEnter2D()");
                break;
        }
    }

    /*------------------------------------
    SetEnemyAttribute
    
    summary:敵の属性を設定
    param  :スプライト(Sprite),色(COLOR)
    return :なし(void)
    ------------------------------------*/
    public void SetEnemyAttribute(Sprite sprite, COLOR color)
    {
        /* 初期化 */
        if (m_sr == null)
            m_sr = GetComponent<SpriteRenderer>();

        m_sr.sprite     = sprite;
        m_current_color = color;
    }

    /*------------------------------------
    GetEnemyColor
    
    summary:敵の色を取得
    param  :なし(void)
    return :色(COLOR)
    ------------------------------------*/
    public COLOR GetEnemyColor()
    {
        return m_current_color;
    }

    /*------------------------------------
    CreateEffect
    
    summary:エフェクト生成
    param  :作成するエフェクトの色(COLOR)
    return :なし(void)
    ------------------------------------*/
    public void CreateEffect(COLOR color)
    {
        AudioSource audio = GetComponent<AudioSource>();
        GameObject  obj   = null;     // 一時的パーティクルオブジェクト

        switch(color)
        {
            case COLOR.RED:
                obj = m_Particle[(int)COLOR.RED];
                audio.PlayOneShot(m_FireSe);
                break;

            case COLOR.GREEN:
                obj = m_Particle[(int)COLOR.GREEN];
                audio.PlayOneShot(m_WingSe);
                break;

            case COLOR.BLUE:
                obj = m_Particle[(int)COLOR.BLUE];
                StartCoroutine(PlayIceSe(audio));
                break;

            default:
                Debug.Assert(false);        // エラー
                break;
        }

        Instantiate(obj, transform.position, Quaternion.identity);
    }

    IEnumerator PlayIceSe(AudioSource audio)
    {
        audio.PlayOneShot(m_IceSe[0]);

        yield return new WaitForSeconds(0.8f);

        audio.PlayOneShot(m_IceSe[1]);

        yield return null;
    }
}
