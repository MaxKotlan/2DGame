using UnityEngine;
using System.Collections;

public class PlayerIO : MonoBehaviour {

    public Chunk chunk;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position + transform.forward / 2, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            Vector3 P = hit.point - hit.normal / 2;
            Vector3 blockpos = new Vector3(Mathf.Floor(P.x), Mathf.Floor(P.y), Mathf.Ceil(P.z));
            if (Input.GetMouseButtonDown(0))
            {
                int x = Mathf.RoundToInt(blockpos.x);
                int y = Mathf.RoundToInt(blockpos.y);
                int z = Mathf.RoundToInt(blockpos.z);
                print("Thge position is" + x + "," + y + "," + z);
                chunk.SetBrink(x, y, z);
            }
        }
    }
}
