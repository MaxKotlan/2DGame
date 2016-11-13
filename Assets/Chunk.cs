using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour {
    public int width = 20;
    public byte[,,] map;
    // Use this for initialization
    protected Mesh mesh;

    protected List<Vector3> verts = new List<Vector3>();
    protected List<int> tris = new List<int>();
    protected List<Vector2> uv = new List<Vector2>();

    protected MeshCollider meshCollider;

	void Start () {
        meshCollider = GetComponent<MeshCollider>();
        map = new byte[width, width, width];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < width; z++)
            {
                map[x, 0, z] = 1;
                map[x, 1, z] = (byte)Mathf.RoundToInt(Random.value);
            }
        }

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Regenerate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DrawBrick(int x, int y, int z, byte block)
    {
        Vector3 start = new Vector3(x, y, z);
        Vector3 offset1, offset2;

        if (isTransparent(x, y-1, z))
        {
            offset1 = Vector3.left;
            offset2 = Vector3.back;
            DrawFace(start + Vector3.right, offset1, offset2, block);
        }
        if (isTransparent(x, y + 1, z))
        {
            offset1 = Vector3.right;
            offset2 = Vector3.back;
            DrawFace(start + Vector3.up, offset1, offset2, block);
        }

        if (isTransparent(x -1, y, z))
        {
            offset1 = Vector3.up;
            offset2 = Vector3.back;
            DrawFace(start, offset1, offset2, block);
        }
        if (isTransparent(x + 1, y, z))
        {
            offset1 = Vector3.down;
            offset2 = Vector3.back;
            DrawFace(start + Vector3.right + Vector3.up, offset1, offset2, block);
        }
   
        if (isTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left;
            offset2 = Vector3.up;
            DrawFace(start + Vector3.right + Vector3.back, offset1, offset2, block);
        }
        if (isTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right;
            offset2 = Vector3.up;
            DrawFace(start, offset1, offset2, block);
        }
    }

    public void DrawFace(Vector3 start, Vector3 offset1, Vector3 offset2, byte block)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        int textureid = Mathf.RoundToInt(Random.Range(1, 7));
        Vector2 uvBase = new Vector2(0.0f, .75f);
        
        switch(textureid)
        {
            case 1:
                uvBase = new Vector2(0.0f, .75f);
                break;
            case 2:
                uvBase = new Vector2(.25f, .75f);
                break;
            case 3:
                uvBase = new Vector2(.5f, .75f);
                break;
            case 4:
                uvBase = new Vector2(.75f, .75f);
                break;
            case 5:
                uvBase = new Vector2(0.0f, .5f);
                break;
            case 6:
                uvBase = new Vector2(.25f, .5f);
                break;
            case 7:
                uvBase = new Vector2(.5f, .5f);
                break;
        }

        uv.Add(uvBase);
        uv.Add(uvBase + new Vector2(.25f, 0));
        uv.Add(uvBase + new Vector2(0, .25f));
        uv.Add(uvBase + new Vector2(.25f, .25f));


        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);

    }

    public bool isTransparent(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= width) || (z >= width))
        {
            return true;
        }
        return map[x, y, z] == 0;
    }

    public void Regenerate()
    {
        verts.Clear();
        tris.Clear();
        uv.Clear();
        mesh.triangles = tris.ToArray();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    byte block = map[x, y, z];
                    if (block == 0)
                    {
                        continue;
                    }

                    DrawBrick(x, y, z, block);
                }
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }
}
