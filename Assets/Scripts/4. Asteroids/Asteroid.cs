using UnityEngine;
using System.Collections;

public class Asteroid : CollisionObject
{
    public float Speed = 100.0f;

    private Vector3 pos;
    private Vector3 dir;
	private Pool p;
	public float RotationSpeed = 50.0f;
	private float rand;
		
	protected override void Awake () 
    {
        base.Awake();
		// random direction
        dir = Random.insideUnitSphere;
        dir.z = 0.0f;
        dir.Normalize();
		p = MonoBehaviour.FindObjectOfType(typeof(Pool)) as Pool;
	}
	
	void Start(){
		
	}

    void OnEnable()
    {
        this.CollisionEnabled = true;
		
		this.Radius = this.transform.localScale.x; // set radius
		
		rand = Random.Range(0.0f,0.9f);
		if(rand > 0.5f){
			this.RotationSpeed *= -1;
		}
    }

    void OnDisable()
    {
        this.CollisionEnabled = false;
    }
	
	void Update () 
    {
		// rotation animation
		this.transform.rotation *= Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.forward);
		
        pos = this.transform.position;

        pos += Speed * dir * Time.deltaTime;

        if (pos.x < -512)
            pos.x = 512;
        else if (pos.x > 512)
            pos.x = -512;

        if (pos.y < -384)
            pos.y = 384;
        else if (pos.y > 384)
            pos.y = -384;

        this.transform.position = pos;
	}

    public override void OnCollide(CollisionObject obj, float penetration, Vector3 dir){
		
		if(obj.name == "Ship"){
			this.audio.Play(); // play "ship's explosion". Ship destroys itself
		}
		else{
			// Destroyed. Recycle and give out new asteroids.			
			p.Recycle(this.gameObject, this.pos);
			MainGame.kills++;
			if(this.name == "BigAsteroid"){
				p.Create(2,this.pos);
				p.Create(2,this.pos);
			}
			else if(this.name == "MediumAsteroid"){
				p.Create(3,this.pos);
				p.Create(3,this.pos);
			}
			this.audio.Play(); // explosion sound
		}
    }
}
