using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eType{
	movable,notMovable
}

public class EnergyContainer : MonoBehaviour {

	public float range;
	public eType type;
	private SphereCollider sphereRange;

	// Use this for initialization
	void Start () {
		sphereRange = gameObject.GetComponent<SphereCollider> ();
		sphereRange.radius = range;
		if(type.ToString() == "notMovable"){
			gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		}
	}
}
