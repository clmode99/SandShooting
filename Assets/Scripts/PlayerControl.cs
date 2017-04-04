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
    public float m_SpeedX;              // 速さ
    public GameObject m_Bullet;         // 弾
    public uint       m_BulletMax;      // 弾の最大数

    [SerializeField, Space(15)]
    public float m_FloatIntervalSecond;    // 浮遊時間(秒)
    public float m_FloatDistance;          // 浮遊距離
    public Sprite m_PlayerUp;
    public Sprite m_PlayerDown;

    SpriteRenderer m_sr;
    bool  m_is_float;            // 浮いてるか
    float m_passage_time_ms;     // 経過時間(ミリ秒)

    int m_bullet_num;       // 画面内の弾の数

    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
        m_sr.sprite = m_PlayerDown;

        m_is_float = false;     // 下がってる
        m_passage_time_ms = 0.0f;

        m_bullet_num = 0;
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        LimitTimer lt = GameObject.FindGameObjectWithTag("LimitTime").GetComponent<LimitTimer>();

        /* キー入力 */
        if (Input.GetKey(KeyCode.LeftArrow))  MoveLeft();
        if (Input.GetKey(KeyCode.RightArrow)) MoveRight();
        if ((Input.GetKeyDown(KeyCode.Space)) && (!(lt.IsUpTime()))) Shot();       // ゲーム終了後は撃てない

        /* 浮遊処理 */
        m_passage_time_ms += Time.deltaTime;

        if (m_passage_time_ms >= m_FloatIntervalSecond)
        {
            if (m_is_float) MoveDown();     // 上がってるなら下がる
            else MoveUp();                  // 下がってるなら上げる

            m_passage_time_ms = 0.0f;
        }

        /* 画面内の弾の数更新 */
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Bullet");
        m_bullet_num = obj.Length;
    }

    /*------------------------------------
    MoveLeft
    
    summary:左移動
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void MoveLeft()
    {
        float speedX = transform.position.x - m_SpeedX;
        Move(speedX, transform.position.y);
    }

    /*------------------------------------
    MoveRight
    
    summary:右移動
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void MoveRight()
    {
        float speedX = transform.position.x + m_SpeedX;
        Move(speedX, transform.position.y);
    }

    /*------------------------------------
    MoveUp
    
    summary:上移動
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void MoveUp()
    {
        float speedY = transform.position.y + m_FloatDistance;
        Move(transform.position.x, speedY);

        m_sr.sprite = m_PlayerUp;
        m_is_float = true;
    }

    /*------------------------------------
    MoveDown
    
    summary:下移動
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void MoveDown()
    {
        float speedY = transform.position.y - m_FloatDistance;
        Move(transform.position.x, speedY);

        m_sr.sprite = m_PlayerDown;
        m_is_float = false;
    }

    /*------------------------------------
    Move
    
    summary:移動
    param  :速度Ｘ(float)、速度Ｙ(float)
    return :なし(void)
    ------------------------------------*/
    void Move(float spdX, float spdY)
    {
        transform.position = new Vector3(spdX, spdY, transform.position.z);
    }

    /*------------------------------------
    Shot
    
    summary:発射
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Shot()
    {
        /* 弾の最大数超えてたら発射不可 */
        if (m_bullet_num >= m_BulletMax)
            return;

        /* 弾の発射位置調整 */
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float player_height_half = sr.bounds.size.y / 2.0f;  // プレイヤーの画像高さの半分

        sr = m_Bullet.GetComponent<SpriteRenderer>();
        float bullet_height_half = sr.bounds.size.y / 2.0f;  // 弾の画像高さの半分

        float bullet_pos_adjust = player_height_half + bullet_height_half;  // 発射位置調整値

        /* 弾の生成(発射) */
        Instantiate(m_Bullet, new Vector3(transform.position.x, transform.position.y + bullet_pos_adjust, transform.position.z), Quaternion.identity);
    }

}
