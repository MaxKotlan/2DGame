using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
public class SetupLocalPlayer : NetworkBehaviour {

	public static List<GameObject> players = new List<GameObject>();
	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
			//GetComponent<CharacterController> ().enabled = true;
			transform.FindChild ("FirstPersonCharacter").GetComponent<Camera> ().enabled = true;
			transform.FindChild ("FirstPersonCharacter").GetComponent<PlayerIO> ().enabled = true;
			GetComponentInChildren<World> ().enabled = true;
			players.Add (this.gameObject);
		} else {
			transform.FindChild ("FirstPersonCharacter").GetComponent<Camera> ().enabled = false;
		}
		if (isServer) {
			
		}
	}
	void Update(){
		
	}
}
