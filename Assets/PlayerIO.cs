using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerIO : NetworkBehaviour {

	Transform cameraTransform;
   // public Chunk chunk;
	int matid = 1;

    Vector3 oldPos;

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
        Vector3 trans = this.transform.position;
        Vector3 newpos = new Vector3(Mathf.FloorToInt(trans.x), Mathf.FloorToInt(trans.y), Mathf.FloorToInt(trans.z));
        if (oldPos != newpos)
        {
            Chunk chunk = Chunk.FindChunk(trans);
                StartCoroutine(chunk.CreateVisualMesh());
            Chunk surroundingChunkLeft = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(trans.x) - 6), Mathf.FloorToInt(trans.y), Mathf.FloorToInt(trans.z) - 6));
            Chunk surroundingChunkRight = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(trans.x) + 6), Mathf.FloorToInt(trans.y), Mathf.FloorToInt(trans.z) - 6));
            //Chunk surroundingChunkUp = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(trans.x)), (Mathf.FloorToInt(trans.y) + 1), Mathf.FloorToInt(trans.z)));
            //Chunk surroundingChunkDown = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(trans.x)), (Mathf.FloorToInt(trans.y) - 1), Mathf.FloorToInt(trans.z)));
            Chunk surroundingChunkForward = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(trans.x) - 6), Mathf.FloorToInt(trans.y), (Mathf.FloorToInt(trans.z) + 6)));
            Chunk surroundingChunkBack = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(trans.x) + 6), Mathf.FloorToInt(trans.y), (Mathf.FloorToInt(trans.z) + 6)));
            if (surroundingChunkLeft.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkLeft.CreateVisualMesh());
                StartCoroutine(surroundingChunkLeft.CreateWireframeMesh());
            }
            if (surroundingChunkRight.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkRight.CreateVisualMesh());
                StartCoroutine(surroundingChunkRight.CreateWireframeMesh());
            }
            /*
            if (surroundingChunkUp.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkUp.CreateVisualMesh());
                StartCoroutine(surroundingChunkUp.CreateWireframeMesh());
            }
            if (surroundingChunkDown.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkDown.CreateVisualMesh());
                StartCoroutine(surroundingChunkDown.CreateWireframeMesh());
            }*/
            if (surroundingChunkForward.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkForward.CreateVisualMesh());
                StartCoroutine(surroundingChunkForward.CreateWireframeMesh());
            }
            if (surroundingChunkBack.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkBack.CreateVisualMesh());
                StartCoroutine(surroundingChunkBack.CreateWireframeMesh());
            }

        }
        oldPos = newpos;
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
            Chunk surroundingChunkLeft = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(j.x) - 1), Mathf.FloorToInt(j.y), Mathf.FloorToInt(j.z)));
            Chunk surroundingChunkRight = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(j.x) + 1), Mathf.FloorToInt(j.y), Mathf.FloorToInt(j.z)));
            Chunk surroundingChunkUp = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(j.x)), Mathf.FloorToInt(j.y) + 1, Mathf.FloorToInt(j.z)));
            Chunk surroundingChunkDown = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(j.x)), Mathf.FloorToInt(j.y) - 1, Mathf.FloorToInt(j.z)));
            Chunk surroundingChunkForward = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(j.x)), Mathf.FloorToInt(j.y), Mathf.FloorToInt(j.z) + 1));
            Chunk surroundingChunkBack = Chunk.FindChunk(new Vector3((Mathf.FloorToInt(j.x)), Mathf.FloorToInt(j.y), Mathf.FloorToInt(j.z) - 1));
            if (surroundingChunkLeft.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkLeft.CreateVisualMesh());
                StartCoroutine(surroundingChunkLeft.CreateWireframeMesh());
            }
            if (surroundingChunkRight.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkRight.CreateVisualMesh());
                StartCoroutine(surroundingChunkRight.CreateWireframeMesh());
            }
            if (surroundingChunkUp.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkUp.CreateVisualMesh());
                StartCoroutine(surroundingChunkUp.CreateWireframeMesh());
            }
            if (surroundingChunkDown.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkDown.CreateVisualMesh());
                StartCoroutine(surroundingChunkDown.CreateWireframeMesh());
            }
            if (surroundingChunkForward.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkForward.CreateVisualMesh());
                StartCoroutine(surroundingChunkForward.CreateWireframeMesh());
            }
            if (surroundingChunkBack.GetInstanceID() != chunk.GetInstanceID())
            {
                StartCoroutine(surroundingChunkBack.CreateVisualMesh());
                StartCoroutine(surroundingChunkBack.CreateWireframeMesh());
            }
        }
	}
}
