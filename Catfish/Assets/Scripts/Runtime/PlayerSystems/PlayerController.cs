using LaneSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystems
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        public Lane CurrentLane;
        public bool IsImmune;
        

        [SerializeField] private LaneManager m_laneManager;
        [SerializeField] private float m_speed;
        [SerializeField] Material m_material;
        private float m_phaseCharge = 1;
        private float m_phaseDisplayValue = 0;

        private int m_laneIndex;
        private Coroutine m_moveCoroutine;
        #endregion

        #region Private Functions

        private void FixedUpdate()
        {
            m_material.SetFloat("_Phase", m_phaseDisplayValue);
        }
        private void Update()
        {
            BounceOffset();
            if (Input.GetKey(KeyCode.Space))
            {
                IsImmune = true;
                m_phaseDisplayValue = m_phaseCharge / 1;
                return;
            }
            else
            {
                m_phaseDisplayValue = 0;
                IsImmune = false;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                GetNextLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GetNextLane(1);
            }
            
            
        }
        private void GetNextLane(int _direction)
        {
            if (m_laneIndex + _direction < 0)
            {
                return;
            }
            if (m_laneIndex + _direction >= m_laneManager.Lanes.Length)
            {
                return;
            }
            m_laneIndex += _direction;
            MoveToNewLane(m_laneManager.Lanes[m_laneIndex]);
            return;
        }

        private void BounceOffset()
        {
            if(m_moveCoroutine == null)
            {
                Vector3 bouncePosition = transform.position;
                bouncePosition.y = Mathf.Sin(Time.fixedTime *15) * 0.15f;
                transform.position = bouncePosition;
            }
        }
        private IEnumerator PlayerLerp(Lane _targetLane)
        {
            float t = 0;
            Vector3 currentPos = transform.position;
            while (t <= 1)
            {
                t += Time.deltaTime * m_speed;
                Vector3 targetPosition = _targetLane.position;
                targetPosition.y = Mathf.Sin(1-t) * 5;
                transform.position = Vector3.Lerp(currentPos, targetPosition, t);
                yield return null;
            }
            CurrentLane.playerOccupied = false;
            CurrentLane = _targetLane;
            CurrentLane.playerOccupied = true;
            m_moveCoroutine = null;
        }
        #endregion

        #region Public functions
        public void ResetPlayer()
        {
            CurrentLane.playerOccupied = false;
            m_laneIndex = m_laneManager.MiddleIndex;
            CurrentLane = m_laneManager.Lanes[m_laneIndex];
            transform.position = CurrentLane.position;
            CurrentLane.playerOccupied = true;
        }
        public void MoveToNewLane(Lane _targetLane)
        {
            if (_targetLane == null) 
            {
                return; 
            }
            if (m_moveCoroutine != null)
            {
                StopCoroutine(m_moveCoroutine);
            }
            m_moveCoroutine = StartCoroutine(PlayerLerp(_targetLane));
        }
        #endregion
    }
}

