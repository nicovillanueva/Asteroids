using UnityEngine;
using System.Collections;

public class Bullet : CollisionObject {
	
	private Vector3 dir;
	private Vector3 pos;
	public float Speed = 250.0f;
	
	private Weapon w;
	private BulletPool bp;
	
	// -----------------------------------------------------
	
	protected override void Awake () 
    {
        base.Awake(); // register in List A or B
		w = MonoBehaviour.FindObjectOfType(typeof(Weapon)) as Weapon;
		bp = MonoBehaviour.FindObjectOfType(typeof(BulletPool)) as BulletPool;
		this.Radius = 1;
	}
	
	// -----------------------------------------------------

    void OnEnable()
    {
        this.CollisionEnabled = true;
		this.transform.rotation = w.transform.rotation;
		
        dir = w.transform.up;
		dir.z = 0.0f;
        dir.Normalize(); // identity vector3
    }
	
	// -----------------------------------------------------

    void OnDisable()
    {
        this.CollisionEnabled = false;
    }
	
	// -----------------------------------------------------
	
	void Update () {
        pos = this.transform.position;
		
        pos += Speed * dir * Time.deltaTime;

        if (pos.x < -512)
			RecycleMe();
			
        else if (pos.x > 512)
			RecycleMe();
		
        if (pos.y < -384)
			RecycleMe();
		
        else if (pos.y > 384)
			RecycleMe();
		
        this.transform.position = pos;
	}
	
	// -----------------------------------------------------
	
	// on collide, get recycled by Pool
	public override void OnCollide(CollisionObject obj, float penetration, Vector3 dir){
		bp.Recycle(this);
	}
	
	// -----------------------------------------------------
	
	// used when reaching a limit
	public override void RecycleMe(){
		bp.Recycle(this);
	}
}
