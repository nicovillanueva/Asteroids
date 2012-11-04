using UnityEngine;
using System.Collections;

public class CollisionObject : MonoBehaviour 
{
    public CollisionGroup Group = CollisionGroup.GroupA;
    public float Radius = 50;
    public bool CollisionEnabled = true;

    private CollisionManager mgr;

    protected virtual void Awake()
    {
        mgr = MonoBehaviour.FindObjectOfType(typeof(CollisionManager)) as CollisionManager;
		mgr.RegisterObject(this);
    }

    public virtual void OnCollide(CollisionObject obj, float penetration, Vector3 dir)
    {

    }
	
	public virtual void RecycleMe(){
		// overriden by bullet
	}
}
