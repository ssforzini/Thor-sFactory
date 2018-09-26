using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public bool finalConfirmationLevelComplete;

	//public int containers;
	[HideInInspector]
	public string[] containerStepArray;

	public GameObject car;
	public GameObject[] energyContainers;

	private Vector3 carInitialTransformPosition;
	private Vector3[] energyContainerInitialtransformPosition;
	private Quaternion carInitialTransformRotation;
	private Quaternion[] energyContainerInitialtransformRotation;

	private float resetButton;
	private bool resetActivate;

	private float pauseButton;
	private bool pauseActivate;

	//PAUSE BUTTONS
	public GameObject pauseWindow;
	public Image playArc, mainArc;
	private int pauseAction = 0;
	private float horizontalMovement;
	private bool movingHorizontal, pressingEnter;

	[HideInInspector] public bool exitActivate;


	void Start(){
		finalConfirmationLevelComplete = false;
		resetActivate = false;

		carInitialTransformPosition = car.transform.position;
		carInitialTransformRotation = car.transform.rotation;
		energyContainerInitialtransformPosition = new Vector3[energyContainers.Length];
		energyContainerInitialtransformRotation = new Quaternion[energyContainers.Length];

		for(int k = 0; k < energyContainers.Length; k++){
			energyContainerInitialtransformPosition [k] = energyContainers [k].transform.position;
			energyContainerInitialtransformRotation [k] = energyContainers [k].transform.rotation;
		}

		containerStepArray = new string[energyContainers.Length];
		for(int i = 0; i < containerStepArray.Length; i++){
			containerStepArray [i] = "-";
		}
	}

	void Update(){

		//RESET MOVEMENT
		resetButton = Input.GetAxis ("Reset");
		if(resetButton > 0f && !resetActivate){
			resetLevel ();
		}
		if(resetButton == 0f && resetActivate) {
			resetActivate = false;
		}

		//PAUSE MOVEMENT
		pauseButton = Input.GetAxis ("Pause");
		if(pauseButton > 0f && !pauseActivate){
			pauseActivate = true;
		}
		pauseWindowManager ();
	}

	//AGREGA UN TRUE AL SIGUIENTE VALOR DEL ARRAY DE ENERGIA
	public void addStepWithEnergy(string obj){
		if(!checkExistance(obj)){
			bool setted = false;
			for(int i = 0; i < containerStepArray.Length; i++){
				if(containerStepArray[i] == "-" && !setted){
					containerStepArray [i] = obj;
					setted = true;
				}
			}
		}

	}

	//RESETEA EL ARRAY DE ENERGÍA Y EL VALOR PROPIO DE TODOS LOS CONTENEDORES
	public void resetStepsWithEnergy(){
		for(int i = 0; i < containerStepArray.Length; i++){
			containerStepArray [i] = "-";
		}
	}


	public void decreaseStepWithEnergy(int position){
		if(position != 0){
			position--;
			for(int i = position; i < containerStepArray.Length; i++){
				containerStepArray [i] = "-";
			}
		}
	}

	public bool checkLevelWin(){
		bool result = false;
		if(containerStepArray[energyContainers.Length-1] != "-"){
			result = true;
		}
		return result;
	}

	public void openWinWindow(){
		Debug.Log ("NIVEL GANADO!!!!");
		//ACA IRIA LA VENTANA QUE OFRECE REINICIAR EL NIVEL O PASAR AL SIGUIENTE
	}

	public int getContainerEnergyPosition(string obj){
		int value = 0;
		for (int i = 0; i < containerStepArray.Length; i++) {
			if (obj == containerStepArray [i]) {
				value = i;
			}
		}
		return value;
	}

	public bool checkExistance(string obj){
		bool value = false;
		for (int i = 0; i < containerStepArray.Length; i++) {
			if (obj == containerStepArray [i]) {
				value = true;
			}
		}
		return value;
	}

	public string getFirstNameInArray(){
		return containerStepArray [0];
	}

	public bool checkLastOne(int position){
		bool result = false;
		if (position != 0) {
			position--;
			if (position == containerStepArray.Length) {
				result = true;
			} else if (position + 1 != containerStepArray.Length) {
				if(containerStepArray [position] != "-" && containerStepArray [position + 1] == "-"){
					result = true;
				}
			} else {
				result = false;
			}
		}

		return result;
	}

	public void resetLevel() {
		resetActivate = true;
		car.transform.position = carInitialTransformPosition;
		car.transform.rotation = carInitialTransformRotation;
		car.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		car.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);

		for(int i = 0; i < energyContainerInitialtransformPosition.Length; i++){
			energyContainers [i].transform.position = energyContainerInitialtransformPosition [i];
			energyContainers [i].transform.rotation = energyContainerInitialtransformRotation [i];
		}
	}

	public bool checkLevelWinConfirmation(){
		if(finalConfirmationLevelComplete){
			return true;
		} else {
			return false;
		}
	}

	public void pauseWindowManager(){
		if(pauseActivate){
			pauseWindow.SetActive (true);
			horizontalMovement = Input.GetAxis ("Horizontal");
			float enterAction = Input.GetAxis ("Submit");
			if(horizontalMovement == 0){ movingHorizontal = false; }
			if(enterAction == 0){ pressingEnter = false; }

			if(horizontalMovement < 0 && pauseAction == 1 && !movingHorizontal){
				pauseAction--;
				movingHorizontal = true;
				playArc.gameObject.SetActive (true);
				mainArc.gameObject.SetActive (false);
			} else if(horizontalMovement > 0 && pauseAction == 0 && !movingHorizontal){
				pauseAction++;
				movingHorizontal = true;
				playArc.gameObject.SetActive (false);
				mainArc.gameObject.SetActive (true);
			}

			if(enterAction > 0 && !pressingEnter){
				pressingEnter = true;
				if (pauseAction == 0) {
					pauseActivate = false;
					pauseWindow.SetActive (false);
				} else {
					exitActivate = true;
				}
			}
		}
	}
}
