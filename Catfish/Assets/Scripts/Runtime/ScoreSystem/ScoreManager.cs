using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private int m_score;
    [SerializeField] private int m_highScore;
    [SerializeField] private TMPro.TMP_Text m_scoreUI;
    [SerializeField] private TMPro.TMP_Text m_highScoreUI;

    #endregion

    #region Private Functions
    private void Update()
    {
        if(m_scoreUI == null)
        {
            return;
        }

        m_scoreUI.text = m_score.ToString();

        if (m_highScoreUI == null)
        {
            return;
        }

        m_highScoreUI.text = m_highScore.ToString();
    }
    #endregion

    #region Public Functions
    public void AddScore(int _n)
    {
        m_score += _n;
        m_score = (int)Mathf.Clamp(m_score, 0, Mathf.Infinity);
        m_highScore = (m_score > m_highScore)? m_score : m_highScore;
    }

    public void ResetScore()
    {
        m_score = 0;
    }
    #endregion
}

