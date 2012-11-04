using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
	
	public enum Modes{
		mMoving,
		mFixed,
		mAligned,
	};
	
	public Ship target;
	Vector3 pos;
	public Modes currentCam;
	private Vector3 defaultPos;
	private Quaternion defaultRot;
	
	private Vector3 targetPos;
		
	void Start () {
		target = MonoBehaviour.FindObjectOfType(typeof(Ship)) as Ship;
		
		defaultPos.x = 0;
		defaultPos.y = 0;
		defaultPos.z = -660;
		
		defaultRot.x = 0;
		defaultRot.y = 0;
		defaultRot.z = 0;
	}
	
	// -----------------------------------------------------
	
	void Update () {
		// change current camera if F2 is pressed
		if(Input.GetKeyDown(KeyCode.F2)){
			ChangeCam();
		}
		
		// -750
		
		// Moving camera
		if(currentCam == Modes.mMoving){
			// always look at ship
			this.transform.LookAt(target.transform);
			
			pos = this.transform.position; // keep previous Z
			
			if(pos.z != defaultPos.z){
				pos.z = defaultPos.z;
			}
			
			// follow ship position, but slower
			pos.x = target.transform.position.x / 2;
			pos.y = target.transform.position.y / 2;
			
			this.transform.position = pos;
		}
		
		// static camera
		else if(currentCam == Modes.mFixed){
			if(target.transform.position.x != 0){ // 1 variable check to avoid continuous reassignment
				this.transform.position = defaultPos;
				this.transform.rotation = defaultRot;
			}
		}
		
		// aligned, rotating camera
		else if(currentCam == Modes.mAligned){
			if(target.transform.position.x != 0){
				targetPos.x = target.transform.localPosition.x;
				targetPos.y = target.transform.localPosition.y;
				targetPos.z = -350;
								
				this.transform.position = targetPos;
				this.transform.rotation = target.transform.rotation;
			}
		}
	}
	
	// -----------------------------------------------------
	
	// switch camera
	private void ChangeCam(){
		if(currentCam != Modes.mAligned){ // if different than last one
			currentCam++;
		}
		else{
			currentCam = 0;
		}
	}
}
