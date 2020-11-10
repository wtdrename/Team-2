using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class FieldOFView : MonoBehaviour
{
    public float viewRadius;
    [Range (0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstableMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetWithDelay", .2f);
    }
    private void Update()
    {
        if (getClosestEnemy())
            print("Enemy visible");
        else
            print("not visible");
    }
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisableTargets();
        }
    }

    void FindVisableTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(
            transform.position, viewRadius,targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstableMask)) // if no obstacles in the way
                {
                    visibleTargets.Add(target);
                }
            }

        }
    }

    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0,
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public Transform getClosestEnemy()
    {
        float closetDistance = viewRadius;
         Transform trans = null;
        foreach (Transform target in visibleTargets)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, target.transform.position);
            if (currentDistance < closetDistance)
            {
                closetDistance = currentDistance;
                trans = target.transform;
            }
        }
        
        return trans;
    }
}
