using UnityEngine;
using System.Collections;

public class CameraScriptz : MonoBehaviour {

    public GameObject FirstPersonCharacter;
	public GameObject FPSController;
	public GameObject Camera;
    public GameObject chunk;
	int counter = 0;
	float x;
    Chunk chunkobj;
    Transform trans;

    int pastScroll;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        chunkobj = chunk.GetComponent<Chunk>();
	}
	
	// Update is called once per frame
	void Update () {
		
	//locks players x movement
		if (Camera.GetComponent<Camera> ().enabled == true) {
			if (counter == 0) {
				x = (Mathf.Round(FPSController.GetComponent<Transform> ().position.x)) + (float).5;
			} else {
				counter = 1;
				FPSController.GetComponent<Transform> ().position = new Vector3 (x, FPSController.transform.position.y, FPSController.transform.position.z);
			}
			counter++;

		} else {
			counter = 0;
			x = 0;
		}



	//Locks Camera Movement & Follows player on y + z axis
		Camera.GetComponent<Transform> ().eulerAngles = new Vector3(0,90,0);
		Camera.GetComponent<Transform> ().position = new Vector3(-10, FPSController.transform.position.y, FPSController.transform.position.z); 


    }
		


}
