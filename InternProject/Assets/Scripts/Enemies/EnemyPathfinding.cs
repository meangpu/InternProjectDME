using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private Seeker seeker = null; //
    [SerializeField] private float pathScanInterval = 0.5f;

    private Path path;
    private int currentWaypoint = 0;
    private float currentTime = 1f;
    private bool isEnded = false;

    public Path Path { get { return path; } }

    private void Update()
    {
        if (path == null) { return; }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (isEnded) { goto Skip1; }
            isEnded = true;
            Skip1:
            return;
        }
        else
        {
            if (!isEnded) { return; }
            isEnded = false;
        }
    }

    public void TryGetNewPath(float deltaTime, Transform currentTarget)
    {
        currentTime += deltaTime;

        if (currentTime < pathScanInterval) { return; }

        seeker.StartPath(transform.position, currentTarget.position, OnPathComplete);
        currentTime = 0f;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void IncrementWaypoint()
    {
        currentWaypoint++;
    }

    public Vector3 GetCurrentWaypoint()
    {
        return path.vectorPath[currentWaypoint];
    }
}
