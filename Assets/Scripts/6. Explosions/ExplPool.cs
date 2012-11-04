using UnityEngine;
using System.Collections;

public class ExplPool : MonoBehaviour {
	
	public GameObject prefab;
	public GameObject[] pool;
	public int amount;
	public int current;
	
	// Use this for initialization
	void Awake () {
		// Create all explosions
		pool = new GameObject[amount];
		for (int i = 0; i < amount; i++){
			pool[i] = GameObject.Instantiate(prefab) as GameObject;
			pool[i].SetActiveRecursively(false);
		}
	}
	
	public GameObject Create(Vector3 pos){
		if(current < pool.Length){
			GameObject go = pool[current];
			go.transform.position = pos;
			go.SetActiveRecursively(true);
			current++;
			return go;
		}
		else{
			Debug.LogWarning("Explosions limit reached.");
			return null;
		}
	}
	
	public void Recycle(GameObject go){
		if(current > 0){
			current--;
			pool[current] = go;
			go.SetActiveRecursively(false);
		}
		else{
			Debug.LogWarning("No explosions active.");
		}
	}
}
