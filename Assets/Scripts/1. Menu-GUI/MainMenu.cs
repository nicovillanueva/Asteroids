using UnityEngine;
using System.Collections;

public class MainMenu : myGUI {
	
    private int menuWidth;
    private int menuHeight;
    private int X;
    private int Y;
	
	public Texture texMain;
	public Texture texGo;
	
	public float bgWidth;
	public float bgHeight;
	public float bgX;
	public float bgY;
	public string str;
	
	public override void Update(){
	}
	
	public override void Start () {
		btnWidth = 120.0f;
		btnHeight = 25.0f; // inherited
		
	    menuWidth = 400;
        menuHeight = 50;
		
        X = 1024 / 2 - menuWidth/2;
        Y = 768 / 2 - menuHeight / 2;
	}
	
	void OnGUI(){
		// background
		GUI.Box(new Rect(bgX, bgY, bgWidth, bgHeight), "");
		
		// "Asteroids!"
		GUI.Box(new Rect(X, Y, menuWidth, menuHeight), texMain);
		
		// "GO!"
        if (GUI.Button(new Rect(X, Y + menuHeight+5, menuWidth, menuHeight), texGo))
        {
			Application.LoadLevel(1);
        }
		
		// instructions & version
		GUI.Box(new Rect(X+75, Y + (menuHeight*2)+10, menuWidth-150, menuHeight+60), "Left/Right: Aim\nUp: Thruster\nSpace/Ctrl: Shoot\n1/2/3: Create new asteroids\nF1: Suicide\nF2: Change Camera\nP: Pause");
		GUI.Box(new Rect(X+75, Y + (menuHeight*2)+10 +menuHeight+65, menuWidth-150, 25), "Version 1.0.2");
	}
	
}