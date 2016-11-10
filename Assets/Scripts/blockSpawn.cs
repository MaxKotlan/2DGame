﻿using UnityEngine;
using System.Collections;

public class blockSpawn : MonoBehaviour
{
	public GameObject Dirt;
	public GameObject Grass;
	public GameObject Stone;
	public GameObject Emerald;
	public GameObject Player;
	public GameObject Bedrock;
	float blockX = 50;
	float blockY = 1000;
	int blocks = 0;
	double x = 0;
	double y = 0;
	double z = 0;

	// Use this for initialization
	void Start()
	{

		// creates horozontal cubes
		while (blocks != (blockY * blockX))
		{
			blocks++;
			x+=2.6;
			//Random Number int
			int rndNum = Random.Range(0,2);
			int rndOreNum = Random.Range(0,51);


			//Determine Grass Layer
			if (blocks <= blockX) {
				Instantiate (Grass, new Vector3 ((float)x, (float)y, (float)z), Quaternion.Euler (0, 180, 0));
			} else {

				//Bedrock Layer & randomizer
				if (blocks >= (blockX * blockY) - blockX) {
					Instantiate (Bedrock, new Vector3 ((float)x, (float)y, (float)z), Quaternion.Euler (0, 180, 0));
				} else {

					//Emerald Spawner
					if (rndOreNum == 25) {
						Instantiate (Emerald, new Vector3 ((float)x, (float)y, (float)z), Quaternion.Euler (0, 180, 0));
					} else {

						if (rndNum == 0) {
							Instantiate (Dirt, new Vector3 ((float)x, (float)y, (float)z), Quaternion.Euler (0, 180, 0));
						} else {
							Instantiate (Stone, new Vector3 ((float)x, (float)y, (float)z), Quaternion.Euler (0, 180, 0));
						}
					}

					//End of Emerald/Dirt  Spawner
				}
				//End of Bedrock
			}

			if (x >= 2.6 * blockX)
			{
				x = 0;
				y -= 2.6;
			}

		}
	}


	// Update is called once per frame
	void Update() {

	}
	//End of Update



}