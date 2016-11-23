using UnityEngine;
using System.Collections;

public class PlayerIO : MonoBehaviour {

   // public Chunk chunk;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position + transform.forward / 2, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 8f))
        {
            Vector3 P = hit.point - hit.normal / 2;
            Vector3 blockpos = new Vector3(Mathf.Floor(P.x), Mathf.Floor(P.y), Mathf.Ceil(P.z));
			Chunk chunk = Chunk.FindChunk(blockpos);
			print ("Chunk Cords" + chunk.transform.position);
            if (Input.GetMouseButtonDown(0))
            {
				blockpos += hit.normal;
                int x = Mathf.FloorToInt(blockpos.x);
				int y = Mathf.FloorToInt(blockpos.y);
                int z = Mathf.CeilToInt(blockpos.z);
				z--;
				Vector3 j = chunk.worldCordtoChunkCord (new Vector3 (x, y, z));
				print ("blockpos" + blockpos + "Vector to change " + j);
				chunk.setBlock (Mathf.FloorToInt(j.x), Mathf.FloorToInt(j.y), Mathf.FloorToInt(j.z), (byte)4);
                //chunk.SetBrink(x, y, z);
            }
			if (Input.GetMouseButtonDown (1)) {
				int x = Mathf.RoundToInt(blockpos.x);
				int y = Mathf.RoundToInt(blockpos.y);
				int z = Mathf.RoundToInt(blockpos.z);
				z--;
				//Chunk chunk = Chunk.FindChunk(blockpos);
				Vector3 j = chunk.worldCordtoChunkCord (new Vector3 (x, y, z));
				print ("blockpos" + blockpos + "Vector to change " + j);
				chunk.setBlock (Mathf.FloorToInt(j.x), Mathf.FloorToInt(j.y), Mathf.FloorToInt(j.z), (byte)0);
			}
        }
    }
}
