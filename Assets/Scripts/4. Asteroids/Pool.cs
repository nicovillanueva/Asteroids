using UnityEngine;
using System.Collections;

public class Pool : MonoBehaviour {
	
	public GameObject Prefab;
	public GameObject[] poolBig;
	public GameObject[] poolMed; // arrays according to size
	public GameObject[] poolSma;
	public GameObject toBeDeleted;
	private Vector3 startPos;
	
	public int bigAmount;
	public int currentBig = 0;
	public int currentMed = 0;
	public int currentSma = 0;
	
	public float modifierMedium = 0.75f;
	public float modifierSmall = 0.50f;
	
	private Vector3 bigSize;
	private Vector3 medSize;
	private Vector3 smallSize;

	private int id;
	
	// Audios set from inspector
	public AudioClip bigSFX;
	public AudioClip medSFX;
	public AudioClip smallSFX;
	
	private ExplPool ep;
	
	static public int smallDestroyed;
	
	private enum Sizes{
		big,
		medium,
		small,
	}
	
	void Start(){
	}
	
	// ********* AWAKE *********
	void Awake () {
		
		// SET SIZES FOR EACH TYPE
		bigSize.x = 50;
		bigSize.y = 50;
		bigSize.z = 10;
		
		medSize.x = bigSize.x * modifierMedium;
		medSize.y = bigSize.y * modifierMedium;
		medSize.z = 10;
		
		smallSize.x = bigSize.x * modifierSmall;
		smallSize.y = bigSize.y * modifierSmall;
		smallSize.z = 10;
		
		// pool elements creation
		// BIG ASTEROIDS
		poolBig = new GameObject[bigAmount];
		for(int i = 0; i < poolBig.Length; i++){
			poolBig[i] = GameObject.Instantiate(Prefab) as GameObject;
			SetSize(poolBig[i],Sizes.big); // set name, sound and size. Radius set by itself
			poolBig[i].SetActiveRecursively(false);
		}
		
		// MEDIUM ASTEROIDS
		poolMed = new GameObject[bigAmount*2]; // 2 med asteroid, for each big one
		for(int i = 0; i < poolMed.Length; i++){
			poolMed[i] = GameObject.Instantiate(Prefab) as GameObject;
			SetSize(poolMed[i],Sizes.medium); // set name, sound and size. Radius set by itself
			poolMed[i].SetActiveRecursively(false);
		}
		
		// SMALL ASTEROIDS
		poolSma = new GameObject[bigAmount*4]; // 4 small asteroid, for each big one
		for(int i = 0; i < poolSma.Length; i++){
			poolSma[i] = GameObject.Instantiate(Prefab) as GameObject;
			SetSize(poolSma[i],Sizes.small); // set name, sound and size. Radius set by itself
			poolSma[i].SetActiveRecursively(false);
		}
				
		// Set starting position
		startPos.x = Screen.width;
		startPos.y = Screen.height;
		startPos.z = 0;
		
		// Explosions pool
		ep = MonoBehaviour.FindObjectOfType(typeof(ExplPool)) as ExplPool;
	}
	
	// ********* CREATE *********
	public GameObject Create(int id){
		// >>>>>>>>>>>>>>>>>>> Create a big one
		if(id == 1){
			if(currentBig < poolBig.Length){
				GameObject go = poolBig[currentBig];
				go.transform.position = startPos;
				
				go.SetActiveRecursively(true);
				
				currentBig++;
				return go;
			}
			else{
				Debug.LogWarning("No more BIG asteroids.");
				return null;
			}
		}
		// >>>>>>>>>>>>>>>>>>> Create a medium one
		else if(id == 2){
			if(currentMed < poolMed.Length){
				GameObject go = poolMed[currentMed];
				
				go.transform.position = startPos;
				
				go.SetActiveRecursively(true);
				
				currentMed++;
				return go;
			}
			else{
				Debug.LogWarning("No more MEDIUM asteroids");
				return null;
			}
		}
		// >>>>>>>>>>>>>>>>>>> Create a small one
		else if(id == 3){
			if(currentSma < poolSma.Length){
				GameObject go = poolSma[currentSma];
				go.transform.position = startPos;
				
				go.SetActiveRecursively(true);
				
				currentSma++;
				return go;
			}
			else{
				Debug.LogWarning("No more SMALL asteroids");
				return null;
			}
		}
		
		else{
			Debug.LogError("Something happened!");
			return null;
		}
	}
	
	//  ********* CREATE AN ASTEROID WITH A DETERMINED POSITION *********
	public GameObject Create(int id, Vector3 p){
		GameObject go = Create(id);
		go.transform.position = p;
		return go;
	}
	
	// ********* RECYCLE *********
	public void Recycle(GameObject go, Vector3 pos){
		if(go.name == "BigAsteroid"){
			if(currentBig > 0){
				currentBig--;
				poolBig[currentBig] = go;
				go.SetActiveRecursively(false);
			}
			else{
				Debug.LogWarning("No BIG asteroids active");
			}
		}
		
		else if(go.name == "MediumAsteroid"){
			if(currentMed > 0){
				currentMed--;
				poolMed[currentMed] = go;
				go.SetActiveRecursively(false);
			}
			else{
				Debug.LogWarning("No MEDIUM asteroids active");
			}
		}
		else if(go.name == "SmallAsteroid"){
			if(currentSma > 0){
				currentSma--;
				poolSma[currentSma] = go;
				go.SetActiveRecursively(false);
				smallDestroyed++;
			}
			else{
				Debug.LogWarning("No SMALL asteroids active");
			}
		}
		
		ep.Create(pos); // explosion pool creates an explosion
	}
	
	// Set names, sizes and radius on AWAKE
	private void SetSize(GameObject a, Sizes s){
		switch(s){
		case Sizes.big:
			a.transform.localScale = bigSize;
			a.name = "BigAsteroid";
			a.audio.clip = bigSFX;
			a.audio.playOnAwake = false;
			break;
			
		case Sizes.medium:
			a.transform.localScale = medSize;
			a.name = "MediumAsteroid";
			a.audio.clip = medSFX;
			a.audio.playOnAwake = false;
			break;
			
		case Sizes.small:
			a.transform.localScale = smallSize;
			a.name = "SmallAsteroid";
			a.audio.clip = smallSFX;
			a.audio.playOnAwake = false;
			break;
		}
	}

}
