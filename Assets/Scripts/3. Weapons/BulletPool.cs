using UnityEngine;
using System.Collections;

public class BulletPool : MonoBehaviour {
	
	public Bullet Prefab; // Bullet. set through inspector.
	public Bullet[] pool;
	public Weapon wp;
		
	public int amount;
	public int current = 0;
	public int Avail; // number of available bullets (for GUI purposes)
	
	private Bullet[] bulletArr = new Bullet[3];
	
	// -----------------------------------------------------
	
	void Awake () {
		// pool elements creation
		pool = new Bullet[amount];
		for(int i = 0; i < pool.Length; i++){
			pool[i] = GameObject.Instantiate(Prefab) as Bullet;
			pool[i].gameObject.SetActiveRecursively(false);
			pool[i].name = "Bullet";
		}
		Avail = amount;
	}
		
	// -----------------------------------------------------
	
	public Bullet Create(){
		if(current < pool.Length){
			Bullet go = pool[current];
			go.transform.position = wp.transform.position; // weapon's position = bullet's starting position
			
			go.gameObject.SetActiveRecursively(true);
			current++;
			Avail--;
			return go;
		}
		else{
			Debug.LogWarning("Empty bullets pool");
			return null;
		}
	}
	
	// -----------------------------------------------------
	
	public Bullet[] Create(byte id){
		if(id == 1){ // creates 3 bullets
//			bulletArr = new Bullet[3];
			for(int i = 0; i < 3; i++){
				bulletArr[i] = Create();
			}
			return bulletArr;
		}
		else if(id == 2){
			// create a super bullet
			return null;
		}
		else{
			Debug.LogWarning("Empty bullets pool");
			return null;
		}
	}
	
	// -----------------------------------------------------
	
	// recycle the bullet	
	public void Recycle(Bullet b){
		if(current > 0){
			current--;
			
			pool[current] = b;
			Avail++;
			b.gameObject.SetActiveRecursively(false);
		}
		else{
			Debug.LogWarning("No objects active");
		}
	}
	
	// -----------------------------------------------------
	
	// disables bullets collision to avoid getting points after death
	public void DisableAll(){
		for(int i = 0; i < amount; i++){
			if(pool[i].CollisionEnabled == true){
				pool[i].CollisionEnabled = false;
			}
		}
	}
}
