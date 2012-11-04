using UnityEngine;
using System.Collections;

public class myGUI : MainGame {
	public float scrWidth = 1024;
	public float scrHeight = 768;
	
	protected float btnWidth = 120.0f;
	protected float btnHeight = 25.0f;
	private float btnHeight2 = 30.0f;
	
	private int timeToNext;
	private int nextLvlUp;
	
	public override void Start(){ // do not use parent's Start
	}
	
	public override void Update(){
	}
	
	void OnGUI(){
		
		timeToNext =  MainGame.createEvery - (int)MainGame.newAsteroidTime;
		switch(Pool.smallDestroyed){
		case 0:
			nextLvlUp = 5;
			break;
		case 1:
			nextLvlUp = 4;
			break;
		case 2:
			nextLvlUp = 3;
			break;
		case 3:
			nextLvlUp = 2;
			break;
		case 4:
			nextLvlUp = 1;
			break;
		}
		
		// ACTION PHASE GUI
		if(MainGame.gameStarted == true){
			GUI.Box(new Rect(0,btnHeight * 0, btnWidth, btnHeight),"Big: " + p.currentBig);
			GUI.Box(new Rect(0,btnHeight * 1, btnWidth, btnHeight),"Med: " + p.currentMed);
			GUI.Box(new Rect(0,btnHeight * 2, btnWidth, btnHeight),"Small: " + p.currentSma);
			GUI.Box(new Rect(0,btnHeight * 3, btnWidth, btnHeight),"Next in: " +  timeToNext.ToString() + " seconds");
			
			GUI.Box(new Rect(scrWidth-btnWidth,btnHeight * 0, btnWidth, btnHeight),"Level: " + MainGame.level);
			GUI.Box(new Rect(scrWidth-btnWidth,btnHeight * 1, btnWidth, btnHeight),"Next Lvl Up: " + nextLvlUp.ToString());
			GUI.Box(new Rect(scrWidth-btnWidth,btnHeight * 2, btnWidth, btnHeight),"Destroyed: " + kills);
			GUI.Box(new Rect(scrWidth-btnWidth,btnHeight * 3, btnWidth, btnHeight), "Bullets: " + bp.Avail + "/15");
		}
		
		// GAME OVER GUI
		else{
			GUI.Box(new Rect(scrWidth/2 -btnWidth/2-25, scrHeight/2 -btnHeight2 -50, btnWidth+50, btnHeight2+50), "Game Over!\n\n" + "Final Level: " + MainGame.level + "\nTotal Destroyed: " + kills + "\nSurvived: " + MainGame.totalTime.ToString() + " secs.");
			
			if (GUI.Button(new Rect(scrWidth/2 -btnWidth/2, scrHeight/2+2, btnWidth, btnHeight2), "(R)etry")){
				Application.LoadLevel(1);
	        }
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel(1);
			}
		}
		
		// PAUSED GUI
		if(MainGame.paused){
			if(GUI.Button(new Rect(scrWidth/2 -btnWidth/2, scrHeight/2+2, btnWidth, btnHeight2), "Continue")){
				Time.timeScale = 1.0f;
				paused = false;
			}
			GUI.Box(new Rect(scrWidth/2 -125, scrHeight/2+2+btnHeight2+5, 250, 165), "Instructions:\nLeft/Right: Aim\nUp: Thruster\nSpace/Ctrl: Shoot\n1/2/3: Create new asteroids\nF1: Suicide\nF2: Change Camera\nP: Pause\n\nLevel up by destroying SMALL asteroids!");
		}
	}
	
}
