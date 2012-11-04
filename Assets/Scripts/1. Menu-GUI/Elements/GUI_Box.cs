using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class GUI_Box : _GUI_Manager {
	
	public bool Instructions;
	public bool Credits;
	
	void OnGUI(){
		if(Tex){
			GUI.Box(new Rect(Left, Top, Width, Height), Tex);
		}
		
		else if(Instructions){
			GUI.Box(new Rect(Left, Top, Width, Height+15), "Left/Right: Turn\nUp: Thruster\nSpace/Ctrl: Shoot\n1/2/3: Create new asteroids\nF1: Suicide\nF2: Change Camera\nP: Pause\n\nLevel up by destroying SMALL asteroids!");
		}
		
		else if(Credits){
			// FIX HEIGHTS
			GUI.Box(new Rect(Left, Top, Width, Height+15), "Programming:\nNico Villanueva\n\nMoral Support:\nÁngela Gravano\n\nPlaytesting:\nLucas \"a mi me gusta la piel de la mujer\" Amadei\n\nBee supervision:\nDébora Gavieiro\n\nArt & Sounds taken from OpenGameArt.org\nThanks to: BlackMoon Design, wuhu, yd, and others");
		}
		
		else{
			GUI.Box(new Rect(Left, Top, Width, Height), Text);
		}
	}
}
