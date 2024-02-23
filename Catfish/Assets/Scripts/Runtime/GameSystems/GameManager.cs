using LaneSystem;
using ObstacleSystem;
using PlayerSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private LaneManager m_laneManager;
    [SerializeField] private ObstacleManager m_obstacleManager;
    [SerializeField] private ScoreManager m_scoreManager;
    [SerializeField] PlayerController m_playerController;
    [SerializeField] private GameObject m_deathUI;
    [SerializeField] private GameObject m_mainMenuUI;
    #endregion

    #region Private Functions
    private void Start()
    {
        InitialiseGame();
    }

    //Temp 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InitialiseGame();
        }
    }
    //Temp 

    

   

    private void GameEnded()
    {
        Debug.Log("Ended");
    }

    private void ResetGameState()
    {
        Debug.Log("Reset");
        m_scoreManager.ResetScore();
        m_playerController.ResetPlayer();
        m_obstacleManager.WipeObstacles();
    }


    IEnumerator RestartGameDelay()
    {
        float t = 0f;
        while (t < 1)
        {
            t += 3f * Time.deltaTime;
            Time.timeScale = Mathf.Lerp(0.5f, 0.1f, t);
            yield return null;
        }
        InitialiseGame();

    }
    #endregion

    #region Public Functions
    public void InitialiseGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Init");
        m_deathUI.SetActive(false);
        m_mainMenuUI.SetActive(true);
        if (m_laneManager != null)
        {
            m_laneManager.InitialiseLanes();
        }
        ResetGameState();
        
        
    }

    public void GameStarted()
    {
        Debug.Log("Started");
        m_mainMenuUI.SetActive(false);
        if (m_obstacleManager != null)
        {
            m_obstacleManager.StartObstacles();
        }
        GameEnded();
    }

    public void EndGame()
    {
        m_deathUI.SetActive(true);
        StartCoroutine(RestartGameDelay());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
