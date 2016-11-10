using UnityEngine;
using System.Collections;

public class blockDelete : MonoBehaviour {
	public GameObject Block;
	public GameObject Player;
	float distance;

	// Use this for initialization 
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}


	void OnMouseDown(){
		
		if (Input.GetButtonDown ("Fire1")) {

			distance = Vector3.Distance (gameObject.transform.position, Player.transform.position);

			if( distance < 4.5 && Block.tag != "Bedrock"){
			//print (Input.mousePosition);
			Vector3 p = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			//print (p);
			//print (p.x);
			//print (p.y);
			Destroy (this.gameObject);
			}
		}

	}
		

	void OnCollisionEnter ( Collision col)
	{
		print ("IUGEFUIHWGUOIHWUOIGHOUGHOEUWGHOWEGHOEUGHOG");

		if (col.gameObject.tag == "air") {
			Destroy (this.gameObject);
			print ("SOMETHING DES");
		}
	}

}