using UnityEngine;
using System.Collections;

public class map_generator : MonoBehaviour {

	public GameObject basic_cube;
	public GameObject basic_floor;
	public GameObject basic_checkpoint;

	public GameObject chunk_parent;

	public GameObject godwall;


	GameObject chunk1;
	GameObject chunk2;

	public GameObject player;

	int height = 25;
	int width = 10;

	int current_decalage = 0;
	int current_entrance_x = 2;

	// Use this for initialization
	void Start () {
		current_entrance_x = spawn_chunk (current_decalage, current_entrance_x);
	}
	
	// Update is called once per frame
	void Update () {
	}


	public int spawn_chunk(int decalage, int start_pos)
	{
		/*
		 * 0 : vide
		 * 1 : mur
		 * 2 : couloir
		 * 3 : checkpoint
		 * */
		int[,] map = new int[width,height];

		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++) {
				map [x, y] = 1;
			}

		GameObject current_parent = (GameObject)Instantiate (chunk_parent);
		chunk1 = current_parent;

		int current_pos_y = 0;
		int current_pos_x = start_pos;

		while (current_pos_y < height-1) {
			map [current_pos_x, current_pos_y] = 2;

			if (Random.value > 0.6f) {
				if (Random.value > 0.5f)
					current_pos_x += 1;
				else
					current_pos_x -= 1;
			} else {
				current_pos_y += 1;
			}

			if (current_pos_x > width - 1)
				current_pos_x = width - 1;
			if (current_pos_x < 0)
				current_pos_x = 0;


			if (Random.value < 0.025f) {
				for (int xx = current_pos_x - Random.Range (0,current_pos_x); xx < Random.Range (current_pos_x, width); xx++) {
					for (int yy = current_pos_y - Random.Range (0,current_pos_y); yy < Random.Range (current_pos_y, height); yy++) {
						map [xx, yy] = 2;
					}
				}
			}

			map [current_pos_x, current_pos_y] = 2;
		}



		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++) {
				if(x == 0 || map[x-1,y] == 0 || map[x-1,y] == 1)
				if(x == width-1 || map[x+1,y] == 0 || map[x+1,y] == 1)
				if(y == 0 || map[x,y-1] == 0 || map[x,y-1] == 1)
				if(y == height-1 || map[x,y+1] == 0 || map[x,y+1] == 1)
					map [x, y] = 0;
			}


		GameObject objj = (GameObject)Instantiate (basic_checkpoint, new Vector3 (start_pos, 0, decalage), Quaternion.Euler (-90, 0, 0));
		objj.GetComponent<checkpoint_behave> ().map = this;
		objj.transform.SetParent (current_parent.transform);


		for (int x = 0; x < width ; x++)
			for (int y = 0; y < height ; y++) {
				if (map [x, y] == 1) {
					GameObject obj = (GameObject)Instantiate (basic_cube, new Vector3 (x, 0, y + decalage), Quaternion.Euler (-90, 0, 0));
					obj.transform.SetParent (current_parent.transform);
				}
				if (map [x, y] == 2) {
					GameObject obj = (GameObject)Instantiate (basic_floor, new Vector3 (x, 0, y + decalage), Quaternion.Euler (-90, 0, 0));
					obj.transform.SetParent (current_parent.transform);
				}
				if (x == 0) {
					GameObject obj = (GameObject)Instantiate (basic_cube, new Vector3 (x - 1, 0, y + decalage), Quaternion.Euler (-90, 0, 0));
					obj.transform.SetParent (current_parent.transform);
				}
				if (x == width-1) {
					GameObject obj = (GameObject)Instantiate (basic_cube, new Vector3 (x + 1, 0, y + decalage), Quaternion.Euler (-90, 0, 0));
					obj.transform.SetParent (current_parent.transform);
				}
			}
		return current_pos_x;
	}




	public void checkpoint_reached()
	{
		godwall.transform.position = new Vector3 (0, 0, current_decalage - 1);
		current_decalage += height;

		Destroy (chunk2);
		chunk2 = chunk1;
		current_entrance_x = spawn_chunk (current_decalage, current_entrance_x);
	}
}
