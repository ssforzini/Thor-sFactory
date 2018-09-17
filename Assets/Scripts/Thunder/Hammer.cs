using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

	private ParticleSystem thunderBolt;
	public GameObject light; 
	// Use this for initialization
	void Start () {
		thunderBolt = GetComponentInChildren<ParticleSystem> ();	
	}
	
	void OnTriggerEnter(Collider obj){
		
		if(obj.tag == "EnergyContainer"){
			if(!thunderBolt.isPlaying){
				light.SetActive (true);
				thunderBolt.Play ();
			}
		}
	}

	void OnTriggerExit(Collider obj){
		if(obj.tag == "EnergyContainer"){
			light.SetActive (false);
			thunderBolt.Stop ();
		}
	}
}
