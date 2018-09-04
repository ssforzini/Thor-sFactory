using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public GameObject[] windows;
	public GameObject[] whiteArcs;
	public GameObject[] whiteInstructionsArcs;
	public GameObject[] instructionsWindows;
	public GameObject[] whitePlayArcs;
	private int activeWindow;
	private int activeInstructionsWindow;

	private int actualAction;
	private int actualInstructionsAction;
	private int actualPlayAction;

	private bool movingHorizontal;
	private bool movingVertical;
	private bool pressingEnter;
	private bool pressingCancel;
	
	[HideInInspector]
	public int selectedLevel;


	// Use this for initialization
	void Start () {
		selectedLevel = 0;

		activeWindow = 0;
		activeInstructionsWindow = 0;

		actualAction = 0;
		actualInstructionsAction = 0;
		actualPlayAction = 0;

		activateWhiteArc ();
		activatePlayWhiteArc ();
		activeWindows ();
		activeInstructionsWindows ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalAction = Input.GetAxis ("Horizontal");
		float verticalAction = Input.GetAxis ("Vertical");
		float enterAction = Input.GetAxis ("Submit");
		float cancelAction = Input.GetAxis ("Cancel");

		if(horizontalAction == 0){ movingHorizontal = false; }
		if(verticalAction == 0){ movingVertical = false; }
		if(enterAction == 0){ pressingEnter = false; }
		if(cancelAction == 0){ pressingCancel = false; }

		// Movimiento horizontal en la pantalla principal
		if (horizontalAction < 0 && activeWindow == 0 && actualAction > 0 && !movingHorizontal) {
			actualAction--;
			movingHorizontal = true;
		} else if(horizontalAction > 0 && activeWindow == 0 && actualAction < 3 && !movingHorizontal){
			actualAction++;
			movingHorizontal = true;
		}

		// Movimiento vertical en la pantalla de instrucciones
		if (verticalAction < 0 && activeWindow == 2 && actualInstructionsAction < 2 && !movingVertical) {
			actualInstructionsAction++;
			movingVertical = true;
		} else if(verticalAction > 0 && activeWindow == 2 && actualInstructionsAction > 0 && !movingVertical){
			actualInstructionsAction--;
			movingVertical = true;
		}

		// Movimiento horizontal en la pantalla de niveles
		if (horizontalAction < 0 && activeWindow == 1 && (actualPlayAction != 0 && actualPlayAction != 10) && !movingHorizontal) {
			actualPlayAction--;
			movingHorizontal = true;
		} else if(horizontalAction > 0 && activeWindow == 1 && (actualPlayAction != 9 && actualPlayAction != 19) && !movingHorizontal){
			actualPlayAction++;
			movingHorizontal = true;
		}

		// Movimiento vertical en la pantalla de niveles
		if (verticalAction < 0 && activeWindow == 1 && actualPlayAction < 10 && !movingVertical) {
			actualPlayAction += 10;
			movingVertical = true;
		} else if(verticalAction > 0 && activeWindow == 1 && actualPlayAction > 9 && !movingVertical){
			actualPlayAction -= 10;
			movingVertical = true;
		}

		if(enterAction > 0 && !pressingEnter){
			pressingEnter = true;
			if(activeWindow == 0){
				if(actualAction == 4){
					Application.Quit ();
				} else {
					activeWindow = actualAction + 1;
					activeWindows ();
				}
			} else if(activeWindow == 1){
				selectedLevel = actualPlayAction + 1;
				activeWindows ();
			} else if(activeWindow == 2){
				activeInstructionsWindow = actualInstructionsAction;
				activeInstructionsWindows ();
			}
		}

		if (cancelAction > 0 && !pressingCancel) {
			pressingCancel = true;
			if (activeWindow != 0) {
				activeWindow = 0;
				activeWindows ();
			}
		}

		if(activeWindow == 0){
			activateWhiteArc ();
		} else if(activeWindow == 1){
			activatePlayWhiteArc ();
		} else if(activeWindow == 2) {
			activateInstructionsWhiteArc ();			
		}
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

	private void activeInstructionsWindows(){
		for(int i = 0; i < instructionsWindows.Length; i++){
			if (selectedLevel != 0) {
				instructionsWindows [i].SetActive (false);
			} else {
				if (activeInstructionsWindow == i) {
					instructionsWindows [i].SetActive (true);
				} else {
					instructionsWindows [i].SetActive (false);
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

	private void activateInstructionsWhiteArc(){
		for(int i = 0; i < whiteInstructionsArcs.Length; i++){
			if(i == actualInstructionsAction){
				whiteInstructionsArcs [i].SetActive (true);
			} else {
				whiteInstructionsArcs [i].SetActive (false);
			}
		}
	}

	private void activatePlayWhiteArc(){
		for(int i = 0; i < whitePlayArcs.Length; i++){
			if(i == actualPlayAction){
				whitePlayArcs [i].SetActive (true);
			} else {
				whitePlayArcs [i].SetActive (false);
			}
		}
	}
}