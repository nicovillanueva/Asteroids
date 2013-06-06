using UnityEngine;
using System.Collections;

public class Ship : CollisionObject {
	
	public float accel = 400.0f;
	public float RotationSpeed = 200.0f;
	public Vector3 vel;
	private Vector3 pos;
	public float spdLimit = 500.0f;
	
	private int width = 540;
	private int height = 960;
	
	private float horizontal;
	
	private ExplPool ep;
	
	public bool GodMode;
	
	protected override void Awake(){
		base.Awake();
		this.Radius = 25;
		ep = MonoBehaviour.FindObjectOfType(typeof(ExplPool)) as ExplPool;
	}

	
	// Update is called once per frame
	void Update () {
		pos = this.transform.position;
		
		horizontal = Input.GetAxis("Horizontal");
		
		// VERTICAL MOVEMENT
		if(Input.GetAxisRaw("Vertical") > 0.0f){
			
			// Accelerated velocity
			vel += this.transform.up * accel * Time.deltaTime;
			
			// Speed limiting
			if(Mathf.Abs(vel.x) > spdLimit){
				if(vel.x > 0){
					vel.x = spdLimit;
				}
				else{
					vel.x = -spdLimit;
				}
			}
			if(Mathf.Abs(vel.y) > spdLimit){
				if(vel.y > 0){
					vel.y = spdLimit;
				}
				else{
					vel.y = -spdLimit;
				}
			}
			// End speed limiting
		} // END VERTICAL MOVEMENT
		
		// Rotation
		if(horizontal != 0.0f){
			this.transform.rotation *= Quaternion.AngleAxis(-horizontal * RotationSpeed * Time.deltaTime, Vector3.forward);
		}
		
		// Movement
		pos += vel * Time.deltaTime;
		
		// Screen limit warping
		// SCREEN: 1024x768
		if(pos.x > width/2-this.transform.localScale.x/2 || pos.x < -width/2+this.transform.localScale.x/2){
			pos.x = -pos.x;
		}
		if(pos.y > height/2-this.transform.localScale.y/2 || pos.y < -height/2+this.transform.localScale.y/2){
			pos.y = -pos.y;
		}
				
		this.transform.position = pos;
		
		// DEBUG
		if(Input.GetKeyUp(KeyCode.F1)){
			if(Time.timeScale == 1.0f){
				Death();
			}
		}
		if(Input.GetKeyUp(KeyCode.F12)){
			if(GodMode) GodMode = false;
			else GodMode = true;
		}

	}
	
	public override void OnCollide(CollisionObject obj, float penetration, Vector3 dir){
		if(!GodMode){
			Death();
		}
	}
	
	private void Death(){
		this.gameObject.SetActiveRecursively(false);
		this.CollisionEnabled = false;
		ep.Create(pos);
	}
}