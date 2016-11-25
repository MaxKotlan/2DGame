using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerIO : NetworkBehaviour {

	Transform cameraTransform;
   // public Chunk chunk;
	int matid = 1;
	// Use this for initialization
	void Start () {
		cameraTransform = transform.FindChild ("FirstPersonCharacter").GetComponent<Transform> ();
	}

    // Update is called once per frame
    void Update()
    {
		Ray ray = new Ray(cameraTransform.position + cameraTransform.forward / 2, cameraTransform.forward);
        RaycastHit hit;
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && matid < 9) {
			matid++;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && matid > 0) {
			matid--;
		}
        if (Physics.Raycast(ray, out hit, 8f))
        {
            Vector3 P = hit.point - hit.normal / 2;
            Vector3 blockpos = new Vector3(Mathf.Floor(P.x), Mathf.Floor(P.y), Mathf.Ceil(P.z - 1));
            if (Input.GetMouseButtonDown(0))
            {
				blockpos += hit.normal;
				if (isLocalPlayer) {
					CmdSetBlock ((int)blockpos.x, (int)blockpos.y, (int)blockpos.z, (byte)matid);
				}
            }
			if (Input.GetMouseButtonDown (1)) {
				CmdSetBlock ((int)blockpos.x, (int)blockpos.y, (int)blockpos.z, (byte)0);
			}
        }
    }

	[Command]
	void CmdSetBlock(int x, int y, int z, byte block)
	{

		Vector3 blockpos = new Vector3 (x, y, z);
		Chunk chunk = Chunk.FindChunk (blockpos);
		print ("Chunk Cords" + chunk.transform.position);
		Vector3 j = chunk.worldCordtoChunkCord (blockpos);
		Vector3 k = chunk.worldCordtoChunkCord (transform.position + new Vector3(0f,.8f,0f));
		Vector3 l = k;
		l.y = l.y - 1;
		if (j != k && j != l) {
			print ("blockpos" + blockpos + "Vector to change " + j);
			chunk.setBlock (Mathf.FloorToInt (j.x), Mathf.FloorToInt (j.y), Mathf.FloorToInt (j.z), block);
		}
	}
}
