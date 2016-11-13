using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]

public class block
{
    public Vector3 cords;
    public int materialID;

    public block(Vector3 cord, int matID)
    {
        materialID = matID;
    }
}

public class Chunk : MonoBehaviour {
    public int width = 20;
    public int height = 100;
    public int groundheight = 60;

    public block[,,] map;
    // Use this for initialization
    protected Mesh mesh;

    protected List<Vector3> verts = new List<Vector3>();
    protected List<int> tris = new List<int>();
    protected List<Vector2> uv = new List<Vector2>();

    protected MeshCollider meshCollider;

    void Start() {
        meshCollider = GetComponent<MeshCollider>();
        map = new block[width, height, width];
        for (int y = 0; y < groundheight; y++)
        {

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < width; z++)
                {
                    if (y < (height - 1))
                    {
                        if (Mathf.RoundToInt(Random.Range(0,51)) == 25)
                        {
                            map[x, y, z] = new block(new Vector3(x, y, z), 5);
                        }
                        else if (Mathf.RoundToInt(Random.Range(0, 2)) == 1)
                        {
                            map[x, y, z] = new block(new Vector3(x, y, z), 2);
                        } else
                        {
                            map[x, y, z] = new block(new Vector3(x, y, z), 3);
                        }
                    } else
                    {
          
                      map[x, y, z] = new block(new Vector3(x, y, z), 1);
                 
                    }
                }
            }
        }

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Regenerate();
    }

    // Update is called once per frame
    void Update() {

    }

    public void DrawBrick(int x, int y, int z, block block)
    {
        Vector3 start = new Vector3(x, y, z);
        Vector3 offset1, offset2;

        if (isTransparent(x, y - 1, z))
        {
            offset1 = Vector3.left;
            offset2 = Vector3.back;
            DrawFace(start + Vector3.right, offset1, offset2, block, Vector3.down);
        }
        if (isTransparent(x, y + 1, z))
        {
            offset1 = Vector3.right;
            offset2 = Vector3.back;
            DrawFace(start + Vector3.up, offset1, offset2, block, Vector3.up);
        }

        if (isTransparent(x - 1, y, z))
        {
            offset1 = Vector3.up;
            offset2 = Vector3.back;
            DrawFace(start, offset1, offset2, block, Vector3.left);
        }
        if (isTransparent(x + 1, y, z))
        {
            offset1 = Vector3.down;
            offset2 = Vector3.back;
            DrawFace(start + Vector3.right + Vector3.up, offset1, offset2, block, Vector3.right);
        }

        if (isTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left;
            offset2 = Vector3.up;
            DrawFace(start + Vector3.right + Vector3.back, offset1, offset2, block, Vector3.back);
        }
        if (isTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right;
            offset2 = Vector3.up;
            DrawFace(start, offset1, offset2, block, Vector3.forward);
        }
    }

    public void DrawFace(Vector3 start, Vector3 offset1, Vector3 offset2, block block, Vector3 direction)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        int textureid = Mathf.RoundToInt(Random.Range(1, 7));
        Vector2 uvBase = new Vector2(0.0f, .75f);

        int matid = block.materialID;
        if (matid == 1 && direction == Vector3.up)
        {
            matid = 8;
        }

        switch (matid)
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
            case 8:
                uvBase = new Vector2(.75f, .5f);
                break;
        }

        if (direction == Vector3.right)
        {
            uv.Add(uvBase + new Vector2(.25f, .25f));
            uv.Add(uvBase + new Vector2(.25f, 0));
            uv.Add(uvBase + new Vector2(0, .25f));
            uv.Add(uvBase);
        } else if (direction == Vector3.left)
        {
            uv.Add(uvBase);
            uv.Add(uvBase + new Vector2(0, .25f));
            uv.Add(uvBase + new Vector2(.25f, 0));
            uv.Add(uvBase + new Vector2(.25f, .25f));
        } else 
        { 
            uv.Add(uvBase);
            uv.Add(uvBase + new Vector2(.25f, 0));
            uv.Add(uvBase + new Vector2(0, .25f));
            uv.Add(uvBase + new Vector2(.25f, .25f));
        }


        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);

    }

    public bool isTransparent(int x, int y, int z)
    {

        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= width) || (map[x, y, z] == null))
        {
            return true;
        }
        //if (map[x, y, z] == null)
        //{
        //    return true;
        //}
        return false;
    }

    public void Regenerate()
    {
        verts.Clear();
        tris.Clear();
        uv.Clear();
        mesh.triangles = tris.ToArray();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    block block = map[x, y, z];
                    if (block == null)
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
