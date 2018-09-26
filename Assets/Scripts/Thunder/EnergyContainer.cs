using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eType{
	movable,notMovable
}

public class EnergyContainer : MonoBehaviour {

	public float range;
	public eType type;
	public ParticleSystem ps;

	private SphereCollider sphereRange;

	[HideInInspector] public int containerPositionArray = 0;
	[HideInInspector] public bool hasEnergy;
	[HideInInspector] public LevelManager lm;

	// Use this for initialization
	void Start () {
		sphereRange = gameObject.GetComponent<SphereCollider> ();
		sphereRange.radius = range;
		if(type.ToString() == "notMovable"){
			gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		}
		lm = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		ps.gameObject.SetActive (false);
	}

	void Update(){
		if (containerPositionArray != 0) {
			if (lm.containerStepArray [containerPositionArray - 1] == "-") {
				hasEnergy = false;
				containerPositionArray = 0;
			}

			if (lm.checkLastOne (containerPositionArray)) {
				ps.gameObject.SetActive (false);
			}
		} else if(containerPositionArray == 0 && hasEnergy) {
			hasEnergy = false;
		}

		if (Input.GetKeyDown (KeyCode.U)) {
			Debug.Log (gameObject.name + " energia: " + hasEnergy.ToString() + ", posicion: " + containerPositionArray.ToString());

		}
	}

	private void OnTriggerEnter(Collider other){
		if (other.tag == "EnergyContainer") {
			if(hasEnergy){
				if(!lm.checkExistance(other.name)){
					if(!ps.gameObject.activeSelf){
						ps.gameObject.SetActive (true);
					}
					lm.addStepWithEnergy (other.name);
					other.GetComponent<EnergyContainer> ().setInitialEnergyData(lm.getContainerEnergyPosition (other.name),true);
				}
			}
		}
	}

	private void OnTriggerStay(Collider other){
		if(other.tag == "EnergyContainer"){
			if(!hasEnergy || !other.GetComponent<EnergyContainer>().hasEnergy){
				if (!hasEnergy && ps.gameObject.activeSelf) {
					ps.gameObject.SetActive (false);
				} else if(hasEnergy && !ps.gameObject.activeSelf && other.GetComponent<EnergyContainer>().containerPositionArray == 0) {
					ps.gameObject.SetActive (true);
					lm.addStepWithEnergy (other.name);
					other.GetComponent<EnergyContainer> ().setInitialEnergyData(lm.getContainerEnergyPosition (other.name),true);
				}
			}

			ps.transform.LookAt (other.transform);
			ps.trigger.SetCollider (0,other);
		}
	}

	private void OnTriggerExit(Collider other){
		if (other.tag == "EnergyContainer") {
			
			if (containerPositionArray < other.GetComponent<EnergyContainer> ().containerPositionArray || other.GetComponent<EnergyContainer> ().containerPositionArray == 0) {
				ps.gameObject.SetActive (false);
			} else {
				other.GetComponent<EnergyContainer> ().hasEnergy = false;
				lm.decreaseStepWithEnergy (containerPositionArray);
				containerPositionArray = 0;
			}
		}
	}

	public void setInitialEnergyData(int position, bool energy){
		containerPositionArray = position + 1;
		hasEnergy = energy;
	}
}
