using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum hType{
	Origen,Destino
}

public class Hammer : MonoBehaviour {

	public ParticleSystem thunderBolt;
	public ParticleSystem thunderBoltContainer;
	public GameObject light;
	public hType type;
	private LevelManager lm;

	// Use this for initialization
	void Start () {
		light.SetActive (false);
		if (type.ToString () == "Destino") {
			thunderBolt.gameObject.SetActive (false);
		} else {
			thunderBolt.gameObject.SetActive (true);
		}
		thunderBoltContainer.gameObject.SetActive (false);
		lm = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
	}

	void Update(){
		if(type.ToString() == "Destino"){
			if(!lm.checkLevelWin()){
				thunderBolt.gameObject.SetActive (false);
				thunderBoltContainer.gameObject.SetActive (false);
				light.SetActive (false);
			}
		}
	}

	private void OnTriggerEnter(Collider obj){
		if(obj.tag == "EnergyContainer"){
			if (type.ToString () == "Origen") {
				
				if (!thunderBoltContainer.gameObject.activeSelf) {
					light.SetActive (true);
					thunderBoltContainer.gameObject.SetActive (true);
					lm.addStepWithEnergy (obj.gameObject.name);
					obj.GetComponent<EnergyContainer> ().setInitialEnergyData (lm.getContainerEnergyPosition (obj.gameObject.name), true);
				}

			} else if(type.ToString () == "Destino") {
				
				if(lm.checkLevelWin()){
					if (!thunderBoltContainer.gameObject.activeSelf) {
						light.SetActive (true);
						thunderBoltContainer.gameObject.SetActive (true);
						thunderBolt.gameObject.SetActive (true);
						lm.openWinWindow ();
					}
				}

			}
		}
	}

	private void OnTriggerStay(Collider obj){
		if(obj.tag == "EnergyContainer"){
			if (type.ToString () == "Origen") {
				if (thunderBoltContainer.gameObject.activeSelf && obj.name == lm.getFirstNameInArray ()) {
					thunderBoltContainer.transform.LookAt (obj.transform);
					thunderBoltContainer.trigger.SetCollider (0, obj);
				} else if (!thunderBoltContainer.gameObject.activeSelf) {
					light.SetActive (true);
					thunderBoltContainer.gameObject.SetActive (true);
					lm.addStepWithEnergy (obj.gameObject.name);
					obj.GetComponent<EnergyContainer> ().setInitialEnergyData (lm.getContainerEnergyPosition (obj.gameObject.name), true);
				}
			} else {
				thunderBoltContainer.transform.LookAt (obj.transform);
				thunderBoltContainer.trigger.SetCollider (0, obj);
				if(lm.checkLevelWin()){
					if (!thunderBoltContainer.gameObject.activeSelf) {
						light.SetActive (true);
						thunderBoltContainer.gameObject.SetActive (true);
						thunderBolt.gameObject.SetActive (true);
						lm.openWinWindow ();
					}
				}

			}
		}
	}

	private void OnTriggerExit(Collider obj){
		if(obj.tag == "EnergyContainer"){
			if(type.ToString () == "Origen"){
				lm.decreaseStepWithEnergy (obj.GetComponent<EnergyContainer>().containerPositionArray);
				thunderBoltContainer.gameObject.SetActive (false);
				light.SetActive (false);
			}
		}
	}
}
