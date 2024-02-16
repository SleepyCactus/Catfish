using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    /*
     * Old Version that a little less fancy but works just as well - for those who understand this better
    [SerializeField] private GameManager m_gm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            if(m_gm == null)
            {
                return
            }
            m_gm.InitaliseGame();
        }
    }*/

    [SerializeField] private string m_targetTag;
    [SerializeField] private UnityEvent m_event;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == m_targetTag)
        {
            m_event.Invoke();
        }
    }
}
