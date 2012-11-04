using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	public BulletPool bulletPool;
	
	private float timerLoadWpn;
	private bool Shooting;
	public float fireSpeed; // modif by MainGame
	private bool loadedWeapon;
	private int shots;
	private FireModes currentMode;
	private Bullet[] BulletGroup;
	
	private enum FireModes{
		single,
		triple,
		super,
	}
	
	// -----------------------------------------------------
	
	void Start () {
		Shooting = false;
		fireSpeed = 0.5f;
		currentMode = FireModes.triple; // DEBUG
	}
	// -----------------------------------------------------
	void Update () {
		// get if Fire is being pressed
		if(Input.GetButtonDown("Fire1")){
			Shooting = true;
		}
		if(Input.GetButtonUp("Fire1")){
			Shooting = false;
		}
		
		// check if weapon is ready to fire
		timerLoadWpn += Time.deltaTime;
		if(timerLoadWpn > fireSpeed){ // firespeed modified by MainGame
			loadedWeapon = true;
			timerLoadWpn = 0;
		}
		
		// if the gun is loaded and the button is being pressed, FIRE AWAY
		if(Shooting == true && loadedWeapon == true && Time.timeScale != 0.0f){
			// Fire
			if(Fire()){ // if it could fire, play sound, count the shot and unload
				this.audio.Play();
				shots++;
				loadedWeapon = false;
				timerLoadWpn = 0; // avoid initial 2-shots
			}
		}
		
	} // end update
	
	// -----------------------------------------------------
	
	private bool Fire(){
		switch (currentMode){
			case FireModes.single:
				if(bulletPool.Create() != null){ // get bullet. moves by itself.
					return true;
				}
				else return false;
			break;
			
			case FireModes.triple:
				if(bulletPool.Create((byte)FireModes.triple) != null){
					return true; // 3 bullets are created. see who modifies their angles.
				}
				else return false;
			break;
			
			default:
				return false;
			break;
		}
	}
	
	// -----------------------------------------------------
	
	public void SetMode(int id){
		currentMode = (FireModes)id;
	}
}
