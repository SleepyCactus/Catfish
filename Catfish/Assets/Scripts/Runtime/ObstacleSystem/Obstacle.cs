using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObstacleSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Obstacle : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool m_active;
        [SerializeField] private float m_speedScalar;

        private ObstacleManager m_obstaclesManager;
        private Rigidbody m_rb;
        #endregion

        #region Private Functions
        private void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
            m_rb.isKinematic = true;
            m_rb.useGravity = false;

            GameObject go = GameObject.FindGameObjectWithTag("GameManager");
            m_obstaclesManager = go.GetComponentInChildren<ObstacleManager>();
            
        }
        private void FixedUpdate()
        {
            if (m_active && m_obstaclesManager != null)
            {
                Tick();
            }

        }

        private void OnTriggerEnter(Collider _other)
        {
            if(_other.tag == "DeathCube")
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region Public Functions
        public void Tick()
        {
            if(m_rb == null)
            {
                return;
            }
            m_rb.MovePosition(transform.position + transform.forward * (m_obstaclesManager.baseGameSpeed * m_speedScalar));
            
        }

        #endregion
    }
}

