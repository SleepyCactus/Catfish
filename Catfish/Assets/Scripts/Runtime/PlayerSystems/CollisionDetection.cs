using PlayerSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameManager m_gm;
    [SerializeField] private ScoreManager m_sm;
    [SerializeField] private PlayerController m_pm;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Obstacle")
        {
            if (m_gm == null)
            {return;}
            if (m_pm == null)
            { return;}
            if (m_pm.IsImmune)
            { return;}

            m_gm.EndGame();
        }
        else if (other.tag == "Coin")
        {
            if (m_sm != null)
            {
                Destroy(other.gameObject);
                m_sm.AddScore(5);
            }
        }
    }

    #endregion

    #region Private Function
    #endregion
}
