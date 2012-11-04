using UnityEngine;
using System.Collections;

public class GUI_Button : _GUI_Manager {
	
	public bool BackToMain;
	public bool Credits;
	
	void OnGUI(){
		if(Tex){
			if (GUI.Button(new Rect(Left, Top, Width, Height), Tex)){
				// Script to exec if Texture is enabled
				Application.LoadLevel(1);
	        }
		}
		
		else if(BackToMain){
			if (GUI.Button(new Rect(Left, Top, Width, Height), Text)){
				// Script to exec if Texture is enabled
				Application.LoadLevel(0);
	        }
		}
		
		else if(Credits){
			if (GUI.Button(new Rect(Left, Top, Width, Height), Text)){
				// Script to exec if Texture is enabled
				Application.LoadLevel(2);
	        }
		}
		
		else{
			if (GUI.Button(new Rect(Left, Top, Width, Height), Text)){
				// Script to exec without Texture
				Application.LoadLevel(1);
	        }
		}
	}
	
}
