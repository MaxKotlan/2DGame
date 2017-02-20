using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshExplode : MonoBehaviour {
    public int matid;
	// Use this for initialization

	void Start () {
   //     setUV();
        explode();
    }
	
	// Update is called once per frame
	void Update () {
        explode();
    }

    public void explode()
    {
        BroadcastMessage("Explode");
        GameObject.Destroy(gameObject);
    }
}
