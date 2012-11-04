using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour 
{
    public Pool p; // asteroids pool
	public BulletPool bp;
	public Ship s;
	public Weapon wpn;
	
	
	
	static public float newAsteroidTime; // counts to 5 to release asteroid
	static public float totalTime; // total played time
	
	static public bool gameStarted;
	static public int kills;
	private bool bulletsDisabled = false;
	
	static public bool paused;
	
	private bool modifySpd;
	private bool modifyCreateTime;
	
	static public int createEvery;
	static public int level;
		
	// -----------------------------------------------------
	
	virtual public void Awake(){
		
		wpn = MonoBehaviour.FindObjectOfType(typeof(Weapon)) as Weapon;
		p = MonoBehaviour.FindObjectOfType(typeof(Pool)) as Pool;
		bp = MonoBehaviour.FindObjectOfType(typeof(BulletPool)) as BulletPool;
		s = MonoBehaviour.FindObjectOfType(typeof(Ship)) as Ship;
		gameStarted = true;
	}
	
	// -----------------------------------------------------
	
	public virtual void Start(){
		// reset variables on every Retry
		p.Create(1);
		paused = false;
		newAsteroidTime = 0;
		totalTime = 0;
		kills = 0;
		bulletsDisabled = false;
		paused = false;
		Time.timeScale = 1.0f;
		level = 1;
		Pool.smallDestroyed = 0;
		
		modifySpd = false;
		modifyCreateTime = false;
		createEvery = 7;
	}
	
	// -----------------------------------------------------
	
	public virtual void Update(){
		
		// levelup every 5 small kills
		if(Pool.smallDestroyed >= 5){
			level++;
			Pool.smallDestroyed = 0;
			modifySpd = true;
			modifyCreateTime = true; // leveled up, must modify fireSpd and interval between asteroids
		}
		
		if(modifySpd && level <= 5){ // beyond level 5, can't shoot faster
			wpn.fireSpeed -= 0.1f;
			modifySpd = false;
		}
		
		if(modifySpd && modifyCreateTime && level == 10){
			wpn.fireSpeed = 0.05f;
			createEvery = 1;
		}
		
		if(modifyCreateTime && createEvery > 3){
			createEvery--;
			modifyCreateTime = false;
		}
		
		
		// END LEVELING SYSTEM
		
		// Pause system
		if(Input.GetKeyDown(KeyCode.P)){
			if(Time.timeScale == 1.0f){
				Time.timeScale = 0.0f;
				paused = true;
			}
			else if(Time.timeScale == 0.0f){
				Time.timeScale = 1.0f;
				paused = false;
			}
		}
		if(!paused){
			this.audio.volume = 1.0f;
		}
		else{
			this.audio.volume = 0.5f;
		}
		// End pause system
		
		// While in the action phase, counters count
		if(gameStarted == true){
			newAsteroidTime += Time.deltaTime;
			totalTime += Time.deltaTime;
		}
		
		if(newAsteroidTime >= createEvery){
			p.Create(1); // create BIG asteroid every X secs
			newAsteroidTime = 0;
		}
		
		// Create asteroids on demand
		if(Input.inputString == "1"){
			p.Create(1);
		}
		if(Input.inputString == "2"){
			p.Create(2);
		}
		if(Input.inputString == "3"){
			p.Create(3);
		}
		
		// check ship's status
		if(s.CollisionEnabled == false){
			gameStarted = false;
			
			if(bulletsDisabled == false){
				bp.DisableAll(); // disable all bullets, if not already
				bulletsDisabled = true; // avoids post mortem kills
			}
			
		}
	}
}
