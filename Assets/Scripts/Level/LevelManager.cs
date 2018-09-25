using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public int containers;
	[HideInInspector]
	public string[] containerStepArray;

	void Start(){
		containerStepArray = new string[containers];
		for(int i = 0; i < containerStepArray.Length; i++){
			containerStepArray [i] = "-";
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.K)) {
			for(int j = 0; j < containerStepArray.Length; j++){
				//if(containerStepArray[j] != "-"){
					Debug.Log (containerStepArray[j]);				
				//}
			}
		}
	}

	//AGREGA UN TRUE AL SIGUIENTE VALOR DEL ARRAY DE ENERGIA
	public void addStepWithEnergy(string obj){
		bool setted = false;
		for(int i = 0; i < containerStepArray.Length; i++){
			if(containerStepArray[i] == "-" && !setted){
				containerStepArray [i] = obj;
				setted = true;
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
		position = position - 1;
		for(int i = position; i < containerStepArray.Length; i++){
			containerStepArray [i] = "-";
		}
	}

	public bool checkLevelWin(){
		if (containers == containerStepArray.Length + 1) {
			return true;
		} else {
			return false;
		}
	}

	public void openWinWindow(){
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
}
