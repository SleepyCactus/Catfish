using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaneSystem
{
    public class LaneManager : MonoBehaviour
    {
        #region Variables
        public static LaneManager Instance;
        public int MiddleIndex;
        [SerializeField] public Lane[] Lanes;
        [SerializeField] private float m_laneSpacing = 1;
        [SerializeField] private int m_laneNumber;
        #endregion


        #region Private Functions

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < Lanes.Length; i++)
            {
                if (Lanes[i].playerOccupied)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.grey;
                }
                Gizmos.DrawCube(Lanes[i].position, new Vector3(0.25f, 0.25f, 100));
            }
        }
        #endregion


        #region Public Functions
        // Used to create x amount of lanes & Set middle index to middle lane 
        public void InitialiseLanes()
        {
            MiddleIndex = Mathf.FloorToInt(m_laneNumber / 2.0f);
            Lanes = new Lane[m_laneNumber];
            for (int i = 0; i < Lanes.Length; i++)
            {
                Lanes[i] = new Lane(true, new Vector3((i - (MiddleIndex)) * m_laneSpacing, 0, 0));
            }
        }
        #endregion

    }
}

