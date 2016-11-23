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

        if (Physics.Raycast(ray, out hit, 120f))
        {
            Vector3 P = hit.point - hit.normal / 2;
            Vector3 blockpos = new Vector3(Mathf.Floor(P.x), Mathf.Floor(P.y), Mathf.Ceil(P.z - 1));
            if (Input.GetMouseButtonDown(0))
            {
				blockpos += hit.normal;
					Chunk chunk = Chunk.FindChunk (blockpos);
					print ("Chunk Cords" + chunk.transform.position);
					Vector3 j = chunk.worldCordtoChunkCord (blockpos);
					Vector3 k = chunk.worldCordtoChunkCord (transform.position);
					Vector3 l = k;
					l.y = l.y -1;
					if (j != k && j != l) {
						print ("blockpos" + blockpos + "Vector to change " + j);
						chunk.setBlock (Mathf.FloorToInt (j.x), Mathf.FloorToInt (j.y), Mathf.FloorToInt (j.z), (byte)4);
					}
                //chunk.SetBrink(x, y, z);
            }
			if (Input.GetMouseButtonDown (1)) {
				Chunk chunk = Chunk.FindChunk(blockpos);
				print ("Chunk Cords" + chunk.transform.position);
				Vector3 j = chunk.worldCordtoChunkCord (blockpos);
				print ("blockpos" + blockpos + "Vector to change " + j);
				chunk.setBlock (Mathf.FloorToInt(j.x), Mathf.FloorToInt(j.y), Mathf.FloorToInt(j.z), (byte)0);
			}
        }
    }
}
