/*-------------------------------------
ゲーム全体の管理(GameManager.cs)

date  :2017.04.03
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // Text

public class GameManager : MonoBehaviour {
    /* 変数の宣言 */
    // スタートしてから生成するオブジェクトたち
    [SerializeField, HeaderAttribute("Game Object")]
    public GameObject m_EnemyCreater;
    public GameObject m_Player;
    public GameObject m_UI;

    Sprite m_info_bg;
    Text m_text;

    GameObject m_enemy_creater;

    float m_total_time;         // ゲームのトータル時間
    LimitTimer m_limit_time;    // 制限時間管理

    bool m_is_start;        // ゲームが始まったか

    /* 関数の定義 */
    /*------------------------------------
    Start

    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        GameObject child_text = GameObject.Find("Canvas/Text");
        m_text = child_text.GetComponent<Text>();

        m_total_time = 0.0f;

        m_is_start = false;       // 始まってない
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        /* 時間更新 */
        m_total_time += Time.deltaTime;

        /* ゲームスタート！な演出 */
        if(!m_is_start)
        {
            /* 0秒は[READY] */
            if (m_total_time >= 0.0f)
                m_text.text = "R E A D Y";

            /* 1秒超えたら[GO!] */
            if (m_total_time >= 1.0f)
                m_text.text = "G O !";

            /* 2秒超えたらゲームスタート！ */
            if ((m_total_time >= 2.0f) && (!m_is_start))
            {
                m_text.text = null;     // 文字は消そう
                StartGame();
            }
        }

        /* ゲーム終わりな演出 */
        if ((m_limit_time != null) && (m_limit_time.IsUpTime()))
        {
            /* 敵はもう出現させない */
            Destroy(m_enemy_creater);

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = m_info_bg;

            m_text.text = "T I M E  U P !";
        }

    }

    void StartGame()
    {
        /* スタートしてから生成 */
        m_enemy_creater = Instantiate(m_EnemyCreater, m_EnemyCreater.transform.position, Quaternion.identity);
        Instantiate(m_Player, m_Player.transform.position, Quaternion.identity);
        Instantiate(m_UI, m_UI.transform.position, Quaternion.identity);

        /* インフォ背景削除 */
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        m_info_bg = sr.sprite;      // 画像を保存
        sr.sprite = null;

        /* 制限時間管理用取得(※UI生成後に行うこと!) */
        m_limit_time = GameObject.FindGameObjectWithTag("LimitTime").GetComponent<LimitTimer>();

        m_is_start = true;
    }

    bool IsStart()
    {
        return m_is_start;
    }
}
