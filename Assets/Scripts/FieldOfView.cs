using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    Debug.DrawLine(transform.position, target.position, Color.red);
                    visibleTargets.Add(target);
                }
            }

        }
    }

    public Vector3 DirFromAngle(float angleinDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleinDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleinDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleinDegrees * Mathf.Deg2Rad));
    }


}
