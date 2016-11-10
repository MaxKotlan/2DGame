using UnityEngine;
using System.Collections;

public class blockPlace : MonoBehaviour {
	public GameObject Player;
	public GameObject Dirt;
	float distancePlace;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	

		if (Input.GetMouseButtonDown (1)) {

			distancePlace = Vector3.Distance (gameObject.transform.position, Player.transform.position);
			print (distancePlace);
			if(distancePlace <= 4.5){

				Vector3 p = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f));
				double locX = p.x / 2.6;
				locX = Mathf.RoundToInt ((float)locX);
				locX = locX * 2.6;
				double locY = p.y / 2.6;
				locY = Mathf.RoundToInt ((float)locY);
				locY = locY * 2.6;
				locY += 2.6;

				Instantiate (Dirt, new Vector3 ((float)locX, (float)locY, 0.0f), Quaternion.identity);


		}

	}
	}

	void Awake () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}


	void OnMouseDown(){



	}

}
