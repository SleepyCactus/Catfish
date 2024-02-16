using LaneSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObstacleSystem
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Variables
        

        public float baseGameSpeed = 0.1f;
        [SerializeField] private LaneManager m_laneManager;
        [SerializeField] private GameObject m_objectToSpawn;
        [SerializeField] private Transform m_spawnParent;

        private Coroutine spawnRoutine;
        #endregion

        #region Private Functions
        IEnumerator SpawnCycle()
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Vector3 position = GetSpawnFromLane(m_laneManager.lanes[Random.Range(0,m_laneManager.lanes.Length)]);
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            Instantiate(m_objectToSpawn, position,rotation,m_spawnParent);
            StartCoroutine(SpawnCycle());
        }
        Vector3 GetSpawnFromLane(Lane _laneToSpawnOn)
        {
            Vector3 output = Vector3.zero;
            if(_laneToSpawnOn != null)
            { 
                output = _laneToSpawnOn.position;
            }
            output.z = 100;
            return output;
        }
        #endregion

        #region public Functions
        public void StartObstacles()
        {
            if(spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
            }
            spawnRoutine = StartCoroutine(SpawnCycle());
        }

        public void WipeObstacles()
        {
            List<GameObject> objectsToDelete = new List<GameObject>();
            for (int i = 0; i < m_spawnParent.childCount; i++)
            {
                objectsToDelete.Add(m_spawnParent.GetChild(i).gameObject);
            }

            foreach (var obstacle in objectsToDelete)
            {
                Destroy(obstacle);
            }
        }
        #endregion
    }
}

