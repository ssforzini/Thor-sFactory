using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public GameObject[] windows;
	public GameObject[] whiteArcs;
	private int activeWindow;
	private int actualAction;
	private bool moving;
	
	[HideInInspector]
	public int selectedLevel;


	// Use this for initialization
	void Start () {
		selectedLevel = 0;
		activeWindow = 0;
		actualAction = 0;
		activateWhiteArc ();
	}
	
	// Update is called once per frame
	void Update () {
		float action = Input.GetAxis ("Horizontal");
		if(action == 0){
			moving = false;
		}
		if (action < 0 && activeWindow == 0 && actualAction > 0 && !moving) {
			actualAction--;
			moving = true;
		} else if(action > 0 && activeWindow == 0 && actualAction < 3 && !moving){
			actualAction++;
			moving = true;
		}

		activateWhiteArc ();
	}

	private void activeWindows(){
		for (int i = 0; i < windows.Length; i++) {
			if (selectedLevel != 0) {
				windows [i].SetActive (false);
			} else {
				if(activeWindow == i){
					windows [i].SetActive (true);
				} else {
					windows [i].SetActive (false);
				}
			}
		}
	}

	private void activateWhiteArc(){
		for(int i = 0; i < whiteArcs.Length; i++){
			if(i == actualAction){
				whiteArcs [i].SetActive (true);
			} else {
				whiteArcs [i].SetActive (false);
			}
		}
	}
}
