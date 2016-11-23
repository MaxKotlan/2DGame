﻿using UnityEngine;
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
		
		float heightBase = 42;
		float maxHeight = 62;
		float heightSwing = maxHeight - heightBase;

		byte brick = 3;

		float meme = CalculateNoiseValue(pos, offset1,  0.09f);
		float other = CalculateNoiseValue(pos, offset1,  0.03f);
		float clusterValue = CalculateNoiseValue(pos, offset1,  0.02f);
		float blobValue = CalculateNoiseValue(pos, offset1,  0.09f);
		float mountainValue = CalculateNoiseValue(pos, offset1,  0.009f);

		clusterValue *= meme / 2 + clusterValue;

		mountainValue *= mountainValue;

		mountainValue *= heightSwing;
		mountainValue += heightBase;

		mountainValue += (clusterValue * 10);
		//clusterValue += (10f - meme);

		/*if (pos.y < 40) {
			
			if (.3 >= clusterValue){
				if (.5 >= meme) {
					return 3;
				} else {
					return 2;
				}
			}
		} else {
			if (pos.y >= 40) {*/
				if ((mountainValue) > pos.y){
					if ((mountainValue - 3) > pos.y) {
						if (.3 >= clusterValue){
							if (.5 >= meme) {
								return 3;
							} else if (.7 > other) {
								return 2; 
							} else {
								return 5;
							}
						}
					} else {
						Vector3 abovebyte = new Vector3 (pos.x, pos.y + 1, pos.z);
						byte abovebytebyte = GetTheoreticalByte (abovebyte, offset0, offset1, offset2);
						if (abovebytebyte != 0) {
							return 2;
						} else {
							return 1;
						}
					}
				} else {
					return 0;
				}/*
			} else {
				return 2;
			}
		}*/
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
						BuildFace (brick, new Vector3(x, y, z), Vector3.up, Vector3.forward, false, verts, uvs, tris, Vector3.left);
					// Right wall
					if (IsTransparent(x + 1, y , z))
						BuildFace (brick, new Vector3(x + 1, y, z), Vector3.up, Vector3.forward, true, verts, uvs, tris, Vector3.right);
					
					// Bottom wall
					if (IsTransparent(x, y - 1 , z))
						BuildFace (brick, new Vector3(x, y, z), Vector3.forward, Vector3.right, false, verts, uvs, tris, Vector3.down);
					// Top wall
					if (IsTransparent(x, y + 1, z))
						BuildFace (brick, new Vector3(x, y + 1, z), Vector3.forward, Vector3.right, true, verts, uvs, tris, Vector3.up);
					
					// Back
					if (IsTransparent(x, y, z - 1))
						BuildFace (brick, new Vector3(x, y, z), Vector3.up, Vector3.right, true, verts, uvs, tris, Vector3.back);
					// Front
					if (IsTransparent(x, y, z + 1))
						BuildFace (brick, new Vector3(x, y, z + 1), Vector3.up, Vector3.right, false, verts, uvs, tris, Vector3.forward);
					
					
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
	public virtual void BuildFace(byte brick, Vector3 corner, Vector3 up, Vector3 right, bool reversed, List<Vector3> verts, List<Vector2> uvs, List<int> tris, Vector3 direction)
	{
		int index = verts.Count;
		
		verts.Add (corner);
		verts.Add (corner + up);
		verts.Add (corner + up + right);
		verts.Add (corner + right);
		
		Vector2 uvBase = new Vector2(0.0f, .75f);

		int matid = brick;
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
		case 9:
			uvBase = new Vector2(0.0f, .25f);
			break;
		}

		/*
		if (direction == Vector3.right)
		{
			uvs.Add(uvBase + new Vector2(.25f, .25f));
			uvs.Add(uvBase + new Vector2(.25f, 0));
			uvs.Add(uvBase + new Vector2(0, .25f));
			uvs.Add(uvBase);
		} else if (direction == Vector3.left)
		{*/
			uvs.Add(uvBase);
		uvs.Add(uvBase + new Vector2(0, .25f));
		uvs.Add(uvBase + new Vector2(.25f, .25f));
			uvs.Add(uvBase + new Vector2(.25f, 0));
		/*} else 
		{ 
			uvs.Add(uvBase);
			uvs.Add(uvBase + new Vector2(.25f, 0));
			uvs.Add(uvBase + new Vector2(0, .25f));
			uvs.Add(uvBase + new Vector2(.25f, .25f));
		}*/

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
		StartCoroutine(CreateVisualMesh ());
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
		Vector3 chunBase = this.gameObject.transform.position;
		chunBase.z++;
		Vector3 chunkCords = worldcord - (chunBase);
		if (chunkCords.z == -1) {
			chunkCords.z = width - 1;
		}
		return chunkCords;
	}
}


