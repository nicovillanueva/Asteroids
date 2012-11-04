using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CollisionGroup
{
    GroupA,
    GroupB
}

public class CollisionManager : MonoBehaviour 
{
    private List<CollisionObject> listA = new List<CollisionObject>();
    private List<CollisionObject> listB = new List<CollisionObject>();
    private List<CollisionObject> purge = new List<CollisionObject>();

    private bool checking = false;

    public void RegisterObject(CollisionObject obj)
    {
        if (obj.Group == CollisionGroup.GroupA)
        {
            listA.Add(obj);
        }
        else if (obj.Group == CollisionGroup.GroupB)
        {
            listB.Add(obj);
        }
    }

    public void UnregisterObject(CollisionObject obj)
    {
        if (!checking)
        {
            if (obj.Group == CollisionGroup.GroupA)
            {
                if (listA.Contains(obj))
                    listA.Remove(obj);
            }
            else if (obj.Group == CollisionGroup.GroupB)
            {
                if (listB.Contains(obj))
                    listB.Remove(obj);
            }
        }
        else
        {
            purge.Add(obj);
        }
    }

    void Update()
    {
        checking = true;

        foreach (CollisionObject obj1 in listA)
        {
            if (obj1.CollisionEnabled)
            {
                Vector3 pos1 = obj1.transform.position;
                float r1 = obj1.Radius;

                foreach (CollisionObject obj2 in listB)
                {
                    if (obj2.CollisionEnabled)
                    {
                        Vector3 pos2 = obj2.transform.position;
                        float r2 = obj2.Radius;

                        Vector3 dist = pos2 - pos1;
                        float minDist = r1 + r2;
                        float minDistSqr = Mathf.Pow(minDist, 2);

                        if (dist.sqrMagnitude < minDistSqr)
                        {
                            Vector3 dir = dist.normalized;
                            float penetration = minDist - dist.magnitude;
							
                            obj1.OnCollide(obj2, penetration, -dir);
                            obj2.OnCollide(obj1, penetration, dir);
                        }
                    }

                    if (!obj1.CollisionEnabled)
                        break;
                }
            }
        }

        checking = false;

        Purge();
    }

    void Purge()
    {
        if (purge.Count > 0)
        {
            foreach (CollisionObject obj in purge)
            {
                UnregisterObject(obj);
            }

            purge.Clear();
        }
    }
}
