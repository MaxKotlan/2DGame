using UnityEngine;
using System.Collections;

public class CameraScriptx : MonoBehaviour {

	public GameObject FirstPersonCharacter;
	public GameObject FPSController;
	public GameObject Camera;
	public GameObject chunk;
	int counter = 0;
	float z;
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

		//locks players z movement
		if (Camera.GetComponent<Camera> ().enabled == true) {
			if (counter == 0) {
				z = (Mathf.Round(FPSController.GetComponent<Transform> ().position.z)) + (float).5;
				print (z);
			} else {
				counter = 1;
				FPSController.GetComponent<Transform> ().position = new Vector3 (FPSController.transform.position.x, FPSController.transform.position.y, z);
			}
			counter++;
			//Locks camera look around on the z
			FirstPersonCharacter.GetComponent<Transform>().eulerAngles = new Vector3(0,0,0);

		} else {
			counter = 0;
			z = 0;

		}
			

		//Locks Camera Movement & Follows player on x + y axis
		Camera.GetComponent<Transform> ().rotation = Quaternion.identity;
		Camera.GetComponent<Transform> ().position = new Vector3(FPSController.transform.position.x, FPSController.transform.position.y, -10); 

    }

}
