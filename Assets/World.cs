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
    public void generate(GameObject chunk)
    {
        Chunk chunkScript = chunk.GetComponent<Chunk>();
        for (int y = 0; y < chunkScript.groundheight; y++)
        {

            for (int x = 0; x < chunkScript.width; x++)
            {
                for (int z = 0; z < chunkScript.width; z++)
                {
                    if  (y < (chunkScript.groundheight - 2) || chunkScript.cords.y != 0)
                    {
                        //if (y == 0)
                        //{
                        //    //if bedrock
                        //    chunkScript.map[x, y, z] = new block(new Vector3(x, y, z), 9);
                        //}
                       // else
                       // {
                            //if emerald
                            if (Mathf.RoundToInt(Random.Range(0, 51)) == 25)
                            {
                                chunkScript.map[x, y, z] = new block(new Vector3(x, y, z), 5);
                            }
                            //if dirt in ground
                            else if (Mathf.RoundToInt(Random.Range(0, 2)) == 1)
                            {
                                chunkScript.map[x, y, z] = new block(new Vector3(x, y, z), 2);
                            }
                            //if stone in ground
                            else
                            {
                                chunkScript.map[x, y, z] = new block(new Vector3(x, y, z), 3);
                            }
                       // }
                    }
                    else if(chunkScript.cords.y == 0)
                    {
                        if (y < (chunkScript.groundheight - 1))
                        {
                            //solid grass layer
                            chunkScript.map[x, y, z] = new block(new Vector3(x, y, z), 1);
                        }
                        else
                        {
                            //random grass layer
                            if (Mathf.RoundToInt(Random.Range(0, 2)) == 1)
                            {
                                chunkScript.map[x, y, z] = new block(new Vector3(x, y, z), 1);
                            }
                            else
                            {
                                chunkScript.map[x, y, z] = null;
                            }
                        }
                    }
                }
            }
        }
        chunkScript.mesh = new Mesh();
        chunk.GetComponent<MeshFilter>().mesh = chunkScript.mesh;
        if (chunkScript.cords.y == 0)
        {
            int treeLoop = 0;
            while (treeLoop != 6)
            {
                Tree(chunkScript);
                treeLoop++;
            }
        }
    }

    // Generate random trees
    public void Tree(Chunk chunkscript)
    {
        int makeTree = Mathf.RoundToInt(Random.Range(0, 2));
        if (makeTree == 1)
        {
            int treeX = Mathf.RoundToInt(Random.Range(2, chunkscript.width - 2));
            int treeY = chunkscript.groundheight - 1;
            int treeZ = Mathf.RoundToInt(Random.Range(2, chunkscript.width - 2));

            chunkscript.map[treeX, treeY, treeZ] = new block(new Vector3(treeX, treeY, treeZ), 6);
            chunkscript.map[treeX, treeY + 1, treeZ] = new block(new Vector3(treeX, treeY + 1, treeZ), 6);
            chunkscript.map[treeX, treeY + 2, treeZ] = new block(new Vector3(treeX, treeY + 2, treeZ), 6);
            chunkscript.map[treeX, treeY + 3, treeZ] = new block(new Vector3(treeX, treeY + 3, treeZ), 6);
            chunkscript.map[treeX, treeY + 4, treeZ] = new block(new Vector3(treeX, treeY + 4, treeZ), 7);
            chunkscript.map[treeX, treeY + 3, treeZ - 1] = new block(new Vector3(treeX, treeY + 3, treeZ - 1), 7);
            chunkscript.map[treeX, treeY + 3, treeZ + 1] = new block(new Vector3(treeX, treeY + 3, treeZ + 1), 7);
            chunkscript.map[treeX - 1, treeY + 3, treeZ] = new block(new Vector3(treeX - 1, treeY + 3, treeZ), 7);
            chunkscript.map[treeX + 1, treeY + 3, treeZ] = new block(new Vector3(treeX + 1, treeY + 3, treeZ), 7);
            chunkscript.map[treeX - 1, treeY + 3, treeZ - 1] = new block(new Vector3(treeX - 1, treeY + 3, treeZ - 1), 7);
            chunkscript.map[treeX - 1, treeY + 3, treeZ + 1] = new block(new Vector3(treeX - 1, treeY + 3, treeZ + 1), 7);
            chunkscript.map[treeX + 1, treeY + 3, treeZ - 1] = new block(new Vector3(treeX + 1, treeY + 3, treeZ - 1), 7);
            chunkscript.map[treeX + 1, treeY + 3, treeZ + 1] = new block(new Vector3(treeX + 1, treeY + 3, treeZ + 1), 7);


        }
    }
}

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
                    Chunk chunkscript = g.GetComponent<Chunk>();
                    t.position = new Vector3(x * chunkscript.width, -y * chunkscript.groundheight, z * chunkscript.width);

                    chunkscript.cords = new Vector3(x * chunkscript.width, -y * chunkscript.groundheight, z * chunkscript.width);
                    chunkList.Add(g);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
