using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;

[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(MeshCollider))]
[RequireComponent (typeof(MeshFilter))]
public class Chunk : MonoBehaviour {
	
	public static List<Chunk> chunks = new List<Chunk>();
	public static int width {
		get { return World.currentWorld.chunkWidth; }
	}
	public static int height {
		get { return World.currentWorld.chunkHeight; }
	}
	public byte[,,] map;
	public Mesh visualMesh;
	protected MeshRenderer meshRenderer;
	protected MeshCollider meshCollider;
	protected MeshFilter meshFilter;

	// Use this for initialization
	void Start () {
		
		chunks.Add(this);
		
		meshRenderer = GetComponent<MeshRenderer>();
		meshCollider = GetComponent<MeshCollider>();
		meshFilter = GetComponent<MeshFilter>();
		
		
	
		CalculateMapFromScratch();
		StartCoroutine(CreateVisualMesh());
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static byte GetTheoreticalByte(Vector3 pos) {
		Random.seed = World.currentWorld.seed;
		
		Vector3 grain0Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);
		Vector3 grain1Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);
		Vector3 grain2Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);
		
		return GetTheoreticalByte(pos, grain0Offset, grain1Offset, grain2Offset);
		
	}
	
	public static byte GetTheoreticalByte(Vector3 pos, Vector3 offset0, Vector3 offset1, Vector3 offset2)
	{
		

		byte brick = 3;

		float meme = CalculateNoiseValue(pos, offset1,  0.09f);
		float clusterValue = CalculateNoiseValue(pos, offset1,  0.02f);

		clusterValue *= meme / 2 + clusterValue;

		//clusterValue += (10f - meme);

		if (pos.y < 40) {

			if (.3 >= clusterValue){
				if (.5 >= meme) {
					return 3;
				} else {
					return 2;
				}
			}
		} else {
			if (pos.y > 42) {
				if (meme/3 > .2) {
					return 0;
				}
				return 0;
			} else {
				return 2;
			}
		}
		return 0;
	}
	
	public virtual void CalculateMapFromScratch() {
		map = new byte[width, height, width];
		
		Random.seed = World.currentWorld.seed;
		Vector3 grain0Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);
		Vector3 grain1Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);
		Vector3 grain2Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);
		
		
		
		for (int x = 0; x < World.currentWorld.chunkWidth; x++)
		{
			for (int y = 0; y < height; y++)
			{
				for (int z = 0; z < width; z++)
				{
					map[x, y, z] = GetTheoreticalByte(new Vector3(x, y, z) + transform.position);
				
				}
			}
		}
		
	}
	
	public static float CalculateNoiseValue(Vector3 pos, Vector3 offset, float scale)
	{
		
		float noiseX = Mathf.Abs((pos.x + offset.x) * scale);
		float noiseY = Mathf.Abs((pos.y + offset.y) * scale);
		float noiseZ = Mathf.Abs((pos.z + offset.z) * scale);
		
		return Mathf.Max(0, Noise.Generate(noiseX, noiseY, noiseZ));
		
	}
	
	
	public virtual IEnumerator CreateVisualMesh() {
		visualMesh = new Mesh();
		
		List<Vector3> verts = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();
		
		
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				for (int z = 0; z < width; z++)
				{
					if (map[x,y,z] == 0) continue;
					
					byte brick = map[x,y,z];
					// Left wall
					if (IsTransparent(x - 1, y, z))
						BuildFace (brick, new Vector3(x, y, z), Vector3.up, Vector3.forward, false, verts, uvs, tris);
					// Right wall
					if (IsTransparent(x + 1, y , z))
						BuildFace (brick, new Vector3(x + 1, y, z), Vector3.up, Vector3.forward, true, verts, uvs, tris);
					
					// Bottom wall
					if (IsTransparent(x, y - 1 , z))
						BuildFace (brick, new Vector3(x, y, z), Vector3.forward, Vector3.right, false, verts, uvs, tris);
					// Top wall
					if (IsTransparent(x, y + 1, z))
						BuildFace (brick, new Vector3(x, y + 1, z), Vector3.forward, Vector3.right, true, verts, uvs, tris);
					
					// Back
					if (IsTransparent(x, y, z - 1))
						BuildFace (brick, new Vector3(x, y, z), Vector3.up, Vector3.right, true, verts, uvs, tris);
					// Front
					if (IsTransparent(x, y, z + 1))
						BuildFace (brick, new Vector3(x, y, z + 1), Vector3.up, Vector3.right, false, verts, uvs, tris);
					
					
				}
			}
		}
					
		visualMesh.vertices = verts.ToArray();
		visualMesh.uv = uvs.ToArray();
		visualMesh.triangles = tris.ToArray();
		visualMesh.RecalculateBounds();
		visualMesh.RecalculateNormals();
		
		meshFilter.mesh = visualMesh;
		
		meshCollider.sharedMesh = null;
		meshCollider.sharedMesh = visualMesh;
		
		yield return 0;
		
	}
	public virtual void BuildFace(byte brick, Vector3 corner, Vector3 up, Vector3 right, bool reversed, List<Vector3> verts, List<Vector2> uvs, List<int> tris)
	{
		int index = verts.Count;
		
		verts.Add (corner);
		verts.Add (corner + up);
		verts.Add (corner + up + right);
		verts.Add (corner + right);
		
		Vector2 uvWidth = new Vector2(0.25f, 0.25f);
		Vector2 uvCorner = new Vector2(0.00f, 0.75f);


		uvCorner.x += (float)(brick - 1) / 4;
		
		uvs.Add(uvCorner);
		uvs.Add(new Vector2(uvCorner.x, uvCorner.y + uvWidth.y));
		uvs.Add(new Vector2(uvCorner.x + uvWidth.x, uvCorner.y + uvWidth.y));
		uvs.Add(new Vector2(uvCorner.x + uvWidth.x, uvCorner.y));
		
		if (reversed)
		{
			tris.Add(index + 0);
			tris.Add(index + 1);
			tris.Add(index + 2);
			tris.Add(index + 2);
			tris.Add(index + 3);
			tris.Add(index + 0);
		}
		else
		{
			tris.Add(index + 1);
			tris.Add(index + 0);
			tris.Add(index + 2);
			tris.Add(index + 3);
			tris.Add(index + 2);
			tris.Add(index + 0);
		}
		
	}
	public virtual bool IsTransparent (int x, int y, int z)
	{
		byte brick = GetByte(x,y,z);
		switch (brick)
		{
		case 0: 
			return true;
		default:
			return false;
		}
	}

	public void setBlock(int x, int y, int z, byte block){
		map [x, y, z] = block;
		CreateVisualMesh ();
	}

	public virtual byte GetByte (int x, int y , int z)
	{
		
		if ( (x < 0) || (z < 0) || (y < 0) || (y >= height)  || (x >= width) || (z >= width))
		{

			Vector3 worldPos = new Vector3(x, y, z) + transform.position;
			Chunk chunk = Chunk.FindChunk(worldPos);
			if (z >= width && worldPos.z == 60) {
				return 0;
			}
			if (chunk == this)
				return GetTheoreticalByte (worldPos);
			if (chunk == null) 
			{
				return GetTheoreticalByte(worldPos);
			}
			return chunk.GetByte (worldPos);
			
		}
		return map[x,y,z];
	}
	public virtual byte GetByte(Vector3 worldPos) {
		worldPos -= transform.position;
		int x = Mathf.FloorToInt(worldPos.x);
		int y = Mathf.FloorToInt(worldPos.y);
		int z = Mathf.FloorToInt(worldPos.z);
		return GetByte (x, y, z);
	}
	
	public static Chunk FindChunk(Vector3 pos) {
		
		for (int a = 0; a < chunks.Count; a++)
		{
			Vector3 cpos = chunks[a].transform.position;
			
			if ( ( pos.x < cpos.x) || (pos.z < cpos.z) || (pos.x >= cpos.x + width) || (pos.z >= cpos.z + width) ) continue;
			return chunks[a];
			
		}
		return null;
		
	}

	public Vector3 worldCordtoChunkCord(Vector3 worldcord){
		Vector3 chunkCords = worldcord - this.gameObject.transform.position;
		chunkCords.y = 3;
		return chunkCords;
	}
}


