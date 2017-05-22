/*-------------------------------------
ゲーム全体の管理(GameManager.cs)

date  :2017.04.03
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   // Text
using UnityEngine.SceneManagement;      // SceneManager

public class GameManager : MonoBehaviour {
    /* 変数の宣言 */
    // スタートしてから生成するオブジェクトたち
    [SerializeField, HeaderAttribute("Game Object")]
    public GameObject m_EnemyCreater;
    public GameObject m_Player;
    public GameObject m_UI;
    public GameObject m_Result;

    Sprite m_info_bg;
    Text   m_text;

    // オブジェクトハンドル
    GameObject m_enemy_creater;
    GameObject m_player;
    GameObject m_ui;
    GameObject m_result;

    float      m_total_time;    // ゲームのトータル時間
    LimitTimer m_limit_time;    // 制限時間管理

    bool m_is_start;        // ゲームが始まったか
    bool m_is_result;       // 結果表示中か

    int[] m_score = new int[3];     // スコア。3は色の数(マジックナンバーごめんなさい)

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

        m_is_start  = false;       // 始まってない
        m_is_result = false;

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
        // TODO:コルーチンにすること(見た目すっきり)
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

        /* ゲーム終わりと結果 */
        if ((m_limit_time != null) && (m_is_result == false) && (m_limit_time.IsUpTime()))
            StartCoroutine("Result");

        /* タイトル画面へ */
        if (m_is_result && (Input.GetButtonDown("Submit")))
            StartCoroutine("ChangeTitleScene");
    }

    /*------------------------------------
    StartGame
    
    summary:ゲームスタート処理
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void StartGame()
    {
        /* スタートしてから生成 */
        m_enemy_creater = Instantiate(m_EnemyCreater, m_EnemyCreater.transform.position, Quaternion.identity);
        m_player        = Instantiate(m_Player, m_Player.transform.position, Quaternion.identity);
        m_ui            = Instantiate(m_UI, m_UI.transform.position, Quaternion.identity);

        /* インフォ背景削除 */
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        m_info_bg = sr.sprite;      // 画像を保存
        sr.sprite = null;

        /* 制限時間管理用取得(※UI生成後に行うこと!) */
        m_limit_time = GameObject.FindGameObjectWithTag("LimitTime").GetComponent<LimitTimer>();

        m_is_start = true;
    }


    /*------------------------------------
    Result
    
    summary:結果処理
    param  :なし(void)
    return :(IEnumerator)
    ------------------------------------*/
    IEnumerator Result()
    {
        /* 敵はもう出現させない */
        /*GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; ++i)
            Destroy(enemy[i]);*/
        /* ※↑ゲーム終わりの時に画面上から敵も消したかったら上のコメントを解除する↑ */
        Destroy(m_enemy_creater);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = m_info_bg;

        m_text.text = "T I M E  U P !";

        GetScore();     // ※UI削除前にすること!!

        yield return new WaitForSeconds(3.0f);      // 待機

        /* 結果 */
        Destroy(m_player);
        Destroy(m_ui);
        m_text.text = null;

        if (m_result == null)
            m_result = Instantiate(m_Result, transform.position, Quaternion.identity);     // 結果表示

        m_is_result = true;

        yield break;
    }

    /*------------------------------------
    GetScore(private版)
    
    summary:各スコアを取得
    param  :なし(void)
    return :なし(void)
    ------------------------------------*/
    void GetScore()
    {
        /* 赤スコア */
        ScoreControl sc = GameObject.FindGameObjectWithTag("RedScore").GetComponent<ScoreControl>();
        m_score[(int)COLOR.RED] = sc.GetScore();

        /* 緑スコア */
        sc = GameObject.FindGameObjectWithTag("GreenScore").GetComponent<ScoreControl>();
        m_score[(int)COLOR.GREEN] = sc.GetScore();

        /* 青スコア */
        sc = GameObject.FindGameObjectWithTag("BlueScore").GetComponent<ScoreControl>();
        m_score[(int)COLOR.BLUE] = sc.GetScore();
    }

    /*------------------------------------
    GetScore(public版)
    
    summary:各スコアを取得
    param  :取得する色のスコア(COLOR)
    return :スコア(int)
    ------------------------------------*/
    public int GetScore(COLOR color)
    {
        return m_score[(int)color];
    }

    /*------------------------------------
    GetTotalScore
    
    summary:トータルスコアを取得
    param  :なし(void)
    return :トータルスコア(int)
    ------------------------------------*/
    public int GetTotalScore()
    {
        int red_score   = GameObject.FindGameObjectWithTag("RedResult").GetComponent<RedResult>().GetScore();
        int green_score = GameObject.FindGameObjectWithTag("GreenResult").GetComponent<GreenResult>().GetScore();
        int blue_score  = GameObject.FindGameObjectWithTag("BlueResult").GetComponent<BlueResult>().GetScore();

        return red_score + green_score + blue_score;
    }

    /*------------------------------------
    ChangeTitleScene
    
    summary:タイトル画面に遷移
    param  :なし(void)
    return :(IEnumerator)
    ------------------------------------*/
    IEnumerator ChangeTitleScene()
    {
        yield return new WaitForSeconds(0.01f);

        SceneManager.LoadScene("Title");

        yield break;
    }
}
