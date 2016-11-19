using UnityEngine;
using System.Collections;

public class CameraSelector : MonoBehaviour {
	public GameObject FP_view;
	public GameObject xSideView;
	public GameObject zSideView;
	public GameObject Topdown;
	public GameObject Player;
	int counter = 0;

	// Use this for initialization
	void Start () {
		FP_view.GetComponent<Camera>().enabled = true;
		xSideView.GetComponent<Camera>().enabled = false;
		zSideView.GetComponent<Camera>().enabled = false;
		Topdown.GetComponent<Camera>().enabled = false;
	}



	// Update is called once per frame
	void Update () {
	float y = Player.transform.position.y;




		//BUTTON CAMERA SWITCH -- TO BE REMOVED
		if(Input.GetKeyDown(KeyCode.F)){
			FP_view.GetComponent<Camera> ().enabled = true;
			xSideView.GetComponent<Camera> ().enabled = false;
			zSideView.GetComponent<Camera> ().enabled = false;
			Topdown.GetComponent<Camera> ().enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			xSideView.GetComponent<Camera>().enabled = true;
			FP_view.GetComponent<Camera>().enabled = false;
			zSideView.GetComponent<Camera>().enabled = false;
			Topdown.GetComponent<Camera>().enabled = false;
		}
		if (Input.GetKeyDown(KeyCode.H)){
			zSideView.GetComponent<Camera> ().enabled = true;
			FP_view.GetComponent<Camera> ().enabled = false;
			xSideView.GetComponent<Camera> ().enabled = false;
			Topdown.GetComponent<Camera> ().enabled = false;
			}
			
		//BUTTON CAMERA SWITCH




/*		if (y <= 20) {
			if (counter == 0) {
				FP_view.GetComponent<Camera> ().enabled = false;
				xSideView.GetComponent<Camera> ().enabled = true;
				zSideView.GetComponent<Camera> ().enabled = false;
				Topdown.GetComponent<Camera> ().enabled = false;
				counter++;
			}

		}
*/

	}
}
