using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject followObject;
    public GameObject chunk;
    Chunk chunkobj;
    Transform trans;

    int pastScroll;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        chunkobj = chunk.GetComponent<Chunk>();
	}
	
	// Update is called once per frame
	void Update () {
        trans.position = new Vector3(trans.position.x, followObject.transform.position.y+2, trans.position.z);
        if (pastScroll == Mathf.CeilToInt(followObject.transform.position.z + 1) && followObject.transform.position.y < 59)
        {
            chunkobj.scrollz = Mathf.CeilToInt(followObject.transform.position.z + 1);
            chunkobj.RegenerateView();
        } else if (chunkobj.scrollz < 20)
        {
            chunkobj.scrollz = 20;
            chunkobj.RegenerateView();
        }
        pastScroll = Mathf.CeilToInt(followObject.transform.position.z + 1);
    }
}
