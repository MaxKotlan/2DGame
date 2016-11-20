using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Tree
{
    Tree()
    {

    }
}

public class BlockTerrain
{

}

public class World : MonoBehaviour {

    public int seed = 0;
    public Vector3 chunksize = new Vector3(2, 1, 2);
    public GameObject chunkprefab;
    public GameObject[,,] chunkList;

	// Use this for initialization
	void Start () {
	    if (seed == 0)
        {
            seed = Mathf.FloorToInt(Random.value * int.MaxValue);
        }
        chunkList = new GameObject[Mathf.RoundToInt(chunksize.x), Mathf.RoundToInt(chunksize.y), Mathf.RoundToInt(chunksize.z)];
        for (int x = 0; x < chunksize.x; x++) {
            for (int y = 0; y < chunksize.y; y++)
            {
                for (int z = 0; z < chunksize.z; z++)
                {
                    GameObject g = Instantiate(chunkprefab);
                    Transform t = g.GetComponent<Transform>();
                    Chunk chunkscript = g.GetComponent<Chunk>();
                    t.position = new Vector3(x * chunkscript.width, -y * chunkscript.groundheight, z * chunkscript.width);

                    chunkscript.cords = new Vector3(x * chunkscript.width, -y * chunkscript.groundheight, z * chunkscript.width);
                    chunkList[x,y,z] = g;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

 //   Vector3 WorldCordinatesToChunk(int x, int y, int z)
 //   {
  //     Vector3 asd = Mathf.FloorToInt(chunkprefab.GetComponent<Chunk>().width);
 //   }
}
