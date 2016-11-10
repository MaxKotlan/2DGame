using UnityEngine;
using System.Collections;

public class treeTerrain : MonoBehaviour
{
	

    public GameObject Oak;
	public GameObject Leaf;
	public GameObject Grass;
	public GameObject Dirt;
	public GameObject Stone;
	public GameObject Air;

	float distanceMultiply = 5;
	//sqrt of blockSpawn blocksGened
	int totalBlocks = 300;
	int blocks = 0;
	double x = 0;
    double y = 0;
    double z = 0;
	double treeX;
	double treeY;
	double leafX;
	double leafY;
	double moundX;
	double moundY;
	double caveX;
	double caveY;
	int counter;


    // Use this for initialization
    void Start()
    {

		while (blocks * (distanceMultiply) <= totalBlocks * (distanceMultiply * 150))
        {
            blocks++;
            x+=2.6;

	//ALL TREE WORK
			//Random Number int
			int rndNum = Random.Range(0,101);

			print ("RND NUM " + rndNum);
	//Tree gen start


			//LARGE TREE
			if ( rndNum >= 11 && rndNum <= 18) {

				counter++;
				treeX = x;
				treeY = y;
				leafX = x;
				leafY = y;

				// Start Oak Gen


				//Row 1
				Instantiate (Dirt, new Vector3 ((float)treeX, (float)treeY, (float)z), Quaternion.Euler (0, 180, 0));
				treeY += 2.6;
				while (counter <= 5) {
					counter++;
						Instantiate (Oak, new Vector3 ((float)treeX, (float)treeY, (float)z), Quaternion.Euler (0, 180, 0));
					treeY += 2.6;

				}
				//End of row 1

				//Row 2
				treeY -= 2.6;
				treeX += 2.6;
				while (counter >= 6 && counter <= 10) {
					counter++;
						Instantiate (Oak, new Vector3 ((float)treeX, (float)treeY, (float)z), Quaternion.Euler (0, 180, 0));
						treeY -= 2.6; 
				}
				Instantiate (Dirt, new Vector3 ((float)treeX, (float)treeY, (float)z), Quaternion.Euler (0, 180, 0));

				//End of row 2
				//End Oak Gen

				//Leaf gen

				treeX = x;
				treeY = y;
				leafX = x;
				leafY = y;

				leafX -= 2.6;
				leafY += 10.4;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafX -= 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafY += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafX += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafY += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafX += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafY += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafX += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafY -= 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafX += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafY -= 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafX += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafY -= 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafX -= 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));

				//End Leaf gen

				x+=18.2;
				totalBlocks -= 7; 
				print ("Tree Created");

				//LARGE TREE END
			}



			//SMALL TREE
			if ( rndNum >= 0 && rndNum <= 10) {
					
				counter++;
				treeX = x;
				treeY = y;
				leafX = x;
				leafY = y;

				// Start Oak Gen
				while (counter <= 3) {
					counter++;
					treeY -= 2.6;
					Instantiate (Dirt, new Vector3 ((float)treeX, (float)treeY, (float)z), Quaternion.Euler (0, 180, 0));
					treeY += 5.2;
					Instantiate (Oak, new Vector3 ((float)treeX, (float)treeY, (float)z), Quaternion.Euler (0, 180, 0));

				}
				//End Oak Gen

				//Leaf gen
				leafY += 10.4;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafY -= 2.6;
				leafX += 2.6;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				leafX -= 5.2;
				Instantiate (Leaf, new Vector3 ((float)leafX, (float)leafY, (float)z), Quaternion.Euler (0, 180, 0));
				//End Leaf gen

				x+=10.4;
				totalBlocks -= 4; 
				print ("Tree Created");

	//SMALL TREE END
			}
				


	//ALL MOUND WORK
			//LARGE MOUND
			if (rndNum <= 100 && rndNum >= 97) {
				
				moundX = x;
				moundY = y;

				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundX += 2.6;
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY -= 2.6;
				moundX += 2.6;
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY -= 5.2;
				moundX += 2.6;
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY -= 5.2;
				moundX += 2.6;
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Stone, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY -= 5.2;
				moundX += 2.6;
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY -= 5.2;
				moundX += 2.6;
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));

				print ("Mound Created");
				x += 23.4;
				totalBlocks -= 9; 
				//LARGE MOUND END

			}



			//SMALL MOUND START
			if(rndNum <= 96 && rndNum >= 93){

				moundX = x;
				moundY = y;

				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundX += 2.6;
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundX += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY -= 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundX += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Dirt, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundY += 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));
				moundX += 2.6;
				moundY -= 2.6;
				Instantiate (Grass, new Vector3 ((float)moundX, (float)moundY, (float)z), Quaternion.Euler (0, 180, 0));

				print ("Small Mound Created");
				x+=23.4;
				totalBlocks -= 5; 

				//END SMALL MOUND
				//END ALL MOUND WORK
			}

			//CAVE GEN START

			if(rndNum >= 40 && rndNum <= 60){

				int rndMuliply = Random.Range(10,40); 
				caveX = x;
				caveY = y - ( rndMuliply * 2.6 );

				Instantiate (Air, new Vector3 ((float)caveX, (float)caveY, (float)z), Quaternion.Euler (0, 180, 0));
				caveX -= 2.6;
				Instantiate (Air, new Vector3 ((float)caveX, (float)caveY, (float)z), Quaternion.Euler (0, 180, 0));
				caveY -= 2.6;
				caveX -= 2.6;
				Instantiate (Air, new Vector3 ((float)caveX, (float)caveY, (float)z), Quaternion.Euler (0, 180, 0));
				caveX += 2.6;
				Instantiate (Air, new Vector3 ((float)caveX, (float)caveY, (float)z), Quaternion.Euler (0, 180, 0));
				caveX += 2.6;
				Instantiate (Air, new Vector3 ((float)caveX, (float)caveY, (float)z), Quaternion.Euler (0, 180, 0));
				caveX += 2.6;
				Instantiate (Air, new Vector3 ((float)caveX, (float)caveY, (float)z), Quaternion.Euler (0, 180, 0));
				caveX += 2.6;


				print ("Small Cave Created");

				//END SMALL MOUND
				//END ALL MOUND WORK
			}

			//CAVE GEN END

			counter = 0;

	//GEN END
			}

	//Start End
        }


	// Update is called once per frame
	void Update() {

	}
	//End of Update

}

