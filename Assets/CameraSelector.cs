using UnityEngine;
using System.Collections;

public class CameraSelector : MonoBehaviour {
	public GameObject FP_view;
	public GameObject AngledSideView;
	public GameObject SideCam;
	public GameObject Topdown;
	// Use this for initialization
	void Start () {
		FP_view.GetComponent<Camera>().enabled = true;
		AngledSideView.GetComponent<Camera>().enabled = false;
		SideCam.GetComponent<Camera>().enabled = false;
		Topdown.GetComponent<Camera>().enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			FP_view.GetComponent<Camera>().enabled = false;
			SideCam.GetComponent<Camera>().enabled = true;
		}
	}
}
