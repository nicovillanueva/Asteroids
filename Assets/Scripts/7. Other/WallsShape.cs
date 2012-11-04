using UnityEngine;
using System.Collections;

public class WallsShape : MonoBehaviour {
	
	float scrWidth;
	float scrHeight;
	
	// Use this for initialization
	void Start () {
//		scrWidth = Screen.width;
//		scrHeight = Screen.height;
//		scrWidth = 800.0f;
//		scrHeight = 600.0f;
		scrWidth = 1024.0f;
		scrHeight = 768.0f;
		
		Vector3 size;
		Vector3 pos;
		
		size.z = 100;
		pos.z = 0;
		
		if(this.name == "TopWall"){
			size.x = scrWidth;
			size.y = 5;
				
			pos.x = 0;
			pos.y = scrHeight/2;
			
			this.transform.localScale = size;
			this.transform.position = pos;
		}
		
		else if(this.name == "BottomWall"){
			size.x = scrWidth;
			size.y = 5;
			
			pos.x = 0;
			pos.y = -scrHeight/2;
			
			this.transform.localScale = size;
			this.transform.position = pos;
		}
		
		else if(this.name == "RightWall"){
			size.x = 5;
			size.y = scrHeight;
			
			pos.x = scrWidth/2;
			pos.y = 0;
			
			this.transform.localScale = size;
			this.transform.position = pos;
		}
		
		else if(this.name == "LeftWall"){
			size.x = 5;
			size.y = scrHeight;
			
			pos.x = -scrWidth/2;
			pos.y = 0;
			
			this.transform.localScale = size;
			this.transform.position = pos;
		}
		

	}
}
