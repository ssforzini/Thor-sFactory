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
			//ps.gameObject.SetActive (false);
		}
	}

	public void setInitialEnergyData(int position, bool energy){
		containerPositionArray = position + 1;
		hasEnergy = energy;
	}
}
