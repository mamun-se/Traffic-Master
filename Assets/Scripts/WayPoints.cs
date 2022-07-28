using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [SerializeField] private float wireSphereRadius = 1f;
    public Transform[] wayPointList;
    void OnDrawGizmos()
    {
        foreach (Transform item in transform)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(item.position,wireSphereRadius);
        }
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }
    }

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPointList[i] = transform.GetChild(i);
        }
    }

    public Transform GetNextWayPoint(Transform currentWayPoint)
    {
        if (currentWayPoint == null)
        {
            return transform.GetChild(0);
        }
        else if(currentWayPoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWayPoint.GetSiblingIndex() + 1);
        }
        else
        {
            return transform.GetChild(transform.childCount - 1);
        }
    }


    public Vector3 GetFinalDestination()
    {
        return transform.GetChild(transform.childCount - 1).position;
    }
}
