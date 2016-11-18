using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public int seed = 0;
    public Vector3 chunksize = new Vector3(2, 1, 2);
    public GameObject chunkprefab;
    public List<GameObject> chunkList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	    if (seed == 0)
        {
            seed = Mathf.FloorToInt(Random.value * int.MaxValue);
        }
        for (int x = 0; x < chunksize.x; x++) {
            for (int y = 0; y < chunksize.y; y++)
            {
                for (int z = 0; z < chunksize.z; z++)
                {
                    GameObject g = Instantiate(chunkprefab);
                    Transform t = g.GetComponent<Transform>();
                    t.position = new Vector3(x * 20, y, z * 20);
                    chunkList.Add(g);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
