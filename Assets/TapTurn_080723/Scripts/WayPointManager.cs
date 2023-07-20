using System.Collections.Generic;
using UnityEngine;


namespace TapTurn
{
    public class WayPointManager : MonoBehaviour
    {
        public static WayPointManager Instance { get; private set; }  
        public List<Transform> waypoints;
        public int currentWayPointIndex;
        public Vector2 CurrentMoveDirection { get; private set; }


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            currentWayPointIndex = 0;
        }

        public Vector2 GetNextTurnDirection()
        {          
            int nextWaypointIndex = (currentWayPointIndex + 1) % waypoints.Count;
            currentWayPointIndex = currentWayPointIndex % waypoints.Count;
            Vector2 direction = (waypoints[nextWaypointIndex].position - waypoints[currentWayPointIndex].position).normalized;
            CurrentMoveDirection = direction;
            return direction;
        }

    
    }
        
}
