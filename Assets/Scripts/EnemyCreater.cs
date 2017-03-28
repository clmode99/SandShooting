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
    public GameObject m_enemy;     // 敵オブジェクト
    [Range(2,15)]
    public int m_enemy_num;        // 敵の数

    // スプライト
    [SerializeField, HeaderAttribute("Enemy Sprite")]
    public Sprite m_enemy_red;      // 赤
    public Sprite m_enemy_green;    // 緑
    public Sprite m_enemy_blue;     // 青
    public Sprite m_enemy_dummy;    // ダミー

    GameObject[] m_enemy_list;     // 敵の管理

    /* 関数の定義 */
    /*------------------------------------
    Start
    
    summary:初期化
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Start()
    {
        /* 配列サイズ変更 */
        System.Array.Resize(ref m_enemy_list, m_enemy_num);

        /* 敵の生成 */
        SpriteRenderer sr = m_enemy.GetComponent<SpriteRenderer>();
        float size_width = sr.bounds.size.x;        // 敵の画像幅
        CreateEnemy(m_enemy_num, size_width);
    }

    /*------------------------------------
    Update
    
    summary:更新
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void Update()
    {
        /* はさんでるか判定 */
        for (int i = 0; i < m_enemy_num - 1; ++i)
        {
            /* 最初の端の色設定 */
            EnemyControl ec = m_enemy_list[i].GetComponent<EnemyControl>();
            COLOR first_color = ec.GetEnemyColor();

            if ((first_color == COLOR.DUMMY) || (first_color==COLOR.NONE))     // ダミーと消えてるのは比較対象外
                continue;

            /* 色探索 */
            for (int j = i + 1; j < m_enemy_num; ++j) 
            {
                /* 対象の色取得 */
                ec = m_enemy_list[j].GetComponent<EnemyControl>();
                COLOR target_color = ec.GetEnemyColor();

                if (target_color == COLOR.NONE)     // もう消えてるのは挟んでも無意味
                    break;

                /* 色比較 */
                if (first_color == target_color) 
                {
                    DestroyEnemy(i, j);
                    i = m_enemy_num;        // 二重ループ脱出のため
                    break;
                }
            }
        }

    }

    /*------------------------------------
    CreateEnemy
    
    summary:敵の生成
    param  :数(int),画像幅(float)
    return :なし(void)
    ------------------------------------*/
    void CreateEnemy(int num, float width)
    {
        for (int i = 0; i < num; ++i)
        {
            m_enemy_list[i] = Instantiate(m_enemy, new Vector3(transform.position.x + (width * i) + (i * 0.1f), transform.position.y, transform.position.z), Quaternion.identity);

            /* 色決め。最初はダミー */
            EnemyControl ec = m_enemy_list[i].GetComponent<EnemyControl>();
            ec.SetEnemyAttribute(m_enemy_dummy, COLOR.DUMMY);
        }
    }

    /*-------------------------------------------
    DestroyEnemy
    
    summary:敵の消滅
    param  :消滅したい敵の最初(int),終わり(int)
    return :なし(void)
    -------------------------------------------*/
    void DestroyEnemy(int first,int end)
    {
        for (int i = first; i <= end; ++i)
        {
            /* 消滅風(実際には消してない) */
            EnemyControl ec = m_enemy_list[i].GetComponent<EnemyControl>();
            ec.SetEnemyAttribute(null, COLOR.NONE);

            /* 衝突判定も消す */
            Destroy(m_enemy_list[i].GetComponent<BoxCollider2D>());
        }

    }
}
