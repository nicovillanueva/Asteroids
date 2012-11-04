using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	public float numFrames = 5.0f;
	public int curFrame = 1;
	
	public float animSpeed;
	private float tempTime;
	
	public Vector2 texOffset;
	public Vector2 defaultOffset;
	
	private ExplPool ep;
	
	void Awake () {
		ep = MonoBehaviour.FindObjectOfType(typeof(ExplPool)) as ExplPool;
		texOffset.x = 0.0f;
		texOffset.y = 0.0f;
	}
	
	void Update () {
		
		tempTime += Time.deltaTime;
		
		if(tempTime > animSpeed){
			if(texOffset.x >= 0.8f){
				texOffset.y += 0.2f;
				texOffset.x = -0.2f;
			}
			texOffset.x += 0.2f;
			tempTime = 0.0f;
		} // end timer
		
		this.renderer.material.mainTextureOffset = texOffset;
		
		if(texOffset.x >= 0.8f && texOffset.y >= 0.8f){
			ep.Recycle(this.gameObject);
			texOffset.x = 0.0f;
			texOffset.y = 0.0f;
			this.renderer.material.mainTextureOffset = texOffset;
		}
	}
		
}
