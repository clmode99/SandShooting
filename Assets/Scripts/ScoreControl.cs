using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour {
    /* 変数の宣言 */
    Text m_text;
    int  m_score;
    
    /* 関数の定義 */
    // Use this for initialization
    void Start()
    {
        m_text = GetComponent<Text>();
        m_score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_text.text = m_score.ToString();
    }

    public void AddScore(int score)
    {
        m_score += score;
    }
}
