/*-------------------------------------
敵の生成(EnemyCreater.cs)

date  :2017.03.23
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour {
    /* 変数の宣言 */
    public GameObject[] m_Enemy;     // 敵
    [Range(2,10)]
    public int   m_EnemyNum;               // 敵の数
    public float m_EnemyIntervalSecond;    // 敵の出現間隔(秒)

    // スプライト
    [SerializeField, HeaderAttribute("Enemy Sprite")]
    public Sprite m_EnemyRed;      // 赤
    public Sprite m_EnemyGreen;    // 緑
    public Sprite m_EnemyBlue;     // 青
    public Sprite m_EnemyDummy;    // ダミー

    // スコア(１つにつき)
    [SerializeField, HeaderAttribute("Enemy Score")]
    public int m_RedScore;      // 赤
    public int m_GreenScore;    // 緑
    public int m_BlueScore;     // 青

    GameObject m_Enemy_obj;        // 敵オブジェクト
    GameObject[] m_Enemy_list;     // 敵の管理
    float m_Enemy_size_width;      // 敵の画像の幅
    bool  m_is_init;               // 初期化

    /* 関数の定義 */
    /*------------------------------------
    Start
    
    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        System.Array.Resize(ref m_Enemy_list, m_EnemyNum);     // 配列サイズ変更

        m_is_init = true;           // 最初だけ

        /* 敵の画像幅取得 */
        SpriteRenderer sr = m_Enemy[0].GetComponent<SpriteRenderer>();
        m_Enemy_size_width = sr.bounds.size.x;        // 敵の画像幅
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        /* 初期化してないならだめ */
        if (!m_is_init)
            return;

        /* 敵が画面内にないなら生成 */
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            m_is_init = false;
            StartCoroutine(CreateEnemy(m_EnemyNum, m_Enemy_size_width, m_EnemyIntervalSecond));
        }

        /* はさんでるか判定 */
        for (int i = 0; i < m_EnemyNum - 1; ++i)
        {
            /* 物理的に消えてるのは対象外 */
            if (m_Enemy_list[i] == null)
                continue;

            /* 最初の端の色設定 */
            EnemyControl ec = m_Enemy_list[i].GetComponent<EnemyControl>();
            COLOR first_color = ec.GetEnemyColor();

            /* ダミーと消えてるのは対象外 */
            if ((first_color == COLOR.DUMMY) || (first_color==COLOR.NONE))
                continue;

            /* 色探索 */
            for (int j = i + 1; j < m_EnemyNum; ++j)
            {
                /* 対象の色取得 */
                ec = m_Enemy_list[j].GetComponent<EnemyControl>();
                COLOR target_color = ec.GetEnemyColor();

                if (target_color == COLOR.NONE)     // もう消えてるのは挟んでも無意味
                    break;

                /* 色比較 */
                if (first_color == target_color)
                {
                    DestroyEnemy(i, j);
                    i = m_EnemyNum;        // 二重ループ脱出のため
                    break;
                }
            }
        }

    }

    /*---------------------------------------------
    CreateEnemy
    
    summary:敵の生成
    param  :数(int),画像幅(float),出現秒数(float)
    return :(IEnumerator)
    ---------------------------------------------*/
    IEnumerator CreateEnemy(int num, float width, float second)
    {
        /* どの敵を生成するか、どの位置に色をつけるか決定 */
        m_Enemy_obj               = m_Enemy[Random.Range(0, m_Enemy.Length)];         // どの敵？
        int change_pos            = Random.Range(0, m_EnemyNum);                      // どの位置変える？
        COLOR[] change_color_list = new COLOR[2];                                     // どの色変えるかリスト
        while (true)                    // 色が被らないようにする処理
        {
            change_color_list[0] = (COLOR)(Random.Range(1, 4));      // 緑、青、ダミー
            change_color_list[1] = (COLOR)(Random.Range(1, 4));

            if (change_color_list[0] != change_color_list[1])
                break;
        }
        COLOR change_color = change_color_list[Random.Range(0, change_color_list.Length)];      // どの色変える？

        Sprite[] enemy_sprite_list = { null, m_EnemyGreen, m_EnemyBlue, m_EnemyDummy };   // 敵のスプライトリスト(nullはダミー)

        for (int i = 0; i < num; ++i)
        {
            /* 敵の出現間隔を空ける */
            yield return new WaitForSeconds(second);

            /* 敵の生成 */
            m_Enemy_list[i] = Instantiate(m_Enemy_obj, new Vector3(transform.position.x + (width * i), transform.position.y, transform.position.z), Quaternion.identity);

            /* 色決め */
            EnemyControl ec = m_Enemy_list[i].GetComponent<EnemyControl>();
            if (i == change_pos)
                ec.SetEnemyAttribute(enemy_sprite_list[(int)change_color], change_color);
            else
                ec.SetEnemyAttribute(m_EnemyDummy, COLOR.DUMMY);
        }

        m_is_init = true;       // 初期化完了！
    }

    /*-------------------------------------------
    DestroyEnemy
    
    summary:敵の消滅
    param  :消滅したい敵の最初(int),終わり(int)
    return :なし(void)
    -------------------------------------------*/
    void DestroyEnemy(int first,int end)
    {
        int enemy_num = 0;  // 倒した敵の数
        EnemyControl ec;

        /* スコア加算(※敵消滅より先に処理すること!!) */
        for (int i = first; i <= end; ++i)      // 敵の数計算
        {
            ++enemy_num;
        }

        ec = m_Enemy_list[first].GetComponent<EnemyControl>();      // 適当に最初
        switch (ec.GetEnemyColor())
        {
            case COLOR.RED:
                ScoreControl red_score = GameObject.Find("UI/Text/RedScore").GetComponent<ScoreControl>();
                red_score.AddScore(enemy_num);
                break;

            case COLOR.GREEN:
                ScoreControl green_score = GameObject.Find("UI/Text/GreenScore").GetComponent<ScoreControl>();
                green_score.AddScore(enemy_num);
                break;

            case COLOR.BLUE:
                ScoreControl blue_score = GameObject.Find("UI/Text/BlueScore").GetComponent<ScoreControl>();
                blue_score.AddScore(enemy_num);
                break;
        }

        /* 敵の消滅処理 */
        for (int i = first; i <= end; ++i)
        {
            /* 消滅風(実際には消してない) */
            ec = m_Enemy_list[i].GetComponent<EnemyControl>();
            ec.SetEnemyAttribute(null, COLOR.NONE);

            /* 物理計算は行わない */
            PolygonCollider2D bc = m_Enemy_list[i].GetComponent<PolygonCollider2D>();
            bc.isTrigger = true;
        }

    }
}
