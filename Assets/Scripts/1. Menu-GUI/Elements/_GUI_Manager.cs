using UnityEngine;
using System.Collections;

public class _GUI_Manager : MonoBehaviour {
	
	public enum VerticalPositioning{
		None,
		Top,
		Center,
		Bottom
	};
	
	public enum HorizontalPositioning{
		None,
		Left,
		Center,
		Right
	};
	
	public float Width;
	public float Height;
	public float Left;
	public float Top;
	public string Text;
	public VerticalPositioning VerticalPosition;
	public HorizontalPositioning HorizontalPosition;
	public Texture Tex;
	
	public virtual void Awake(){
		if(VerticalPosition != VerticalPositioning.None){ // if not fixed vertical pos
			if(VerticalPosition == VerticalPositioning.Top){
				Top = 0;
			}
			else if(VerticalPosition == VerticalPositioning.Center){
				Top = Screen.height/2 - Height/2;
			}
			else if(VerticalPosition == VerticalPositioning.Bottom){
				Top = Screen.height - Height;
			}
		}
		
		if(HorizontalPosition != HorizontalPositioning.None){ // if not fixed vertical pos
			if(HorizontalPosition == HorizontalPositioning.Left){
				Left = 0;
			}
			else if(HorizontalPosition == HorizontalPositioning.Center){
				Left = Screen.width/2 - Width/2;
			}
			else if(HorizontalPosition == HorizontalPositioning.Right){
				Left = Screen.width - Width;
			}
		}
	}
}
