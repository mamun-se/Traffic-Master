using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarWayPointMover : MonoBehaviour
{
    [SerializeField] private WayPoints waypointInstance;
    [SerializeField] private float moveSpeed;
    private Transform currentWayPoint;
    private Vector3 finalDestination;
    public bool shallMovedWithWayPoint = false;
    void Start()
    {
        finalDestination = waypointInstance.GetFinalDestination();
    }

    void Update()
    {
        if (shallMovedWithWayPoint)
        {
            if (Vector3.Distance(transform.position, finalDestination) < 0.1f)
            {
                shallMovedWithWayPoint = false;
                GameManager.gameManagerInstance.SetPendingCarAmount();
                transform.gameObject.SetActive(false);
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentWayPoint.position) < 0.1f)
            {
                currentWayPoint = waypointInstance.GetNextWayPoint(currentWayPoint);
                transform.DOLookAt(currentWayPoint.position,0.25f);
            }
        }
    }

    public Transform GetClosestWayPoint()
    {
        Transform[] wayPoints = waypointInstance.wayPointList;
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in wayPoints)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void SetStartingWayPoint(Transform startPoint)
    {
        currentWayPoint = startPoint;
    }
}
