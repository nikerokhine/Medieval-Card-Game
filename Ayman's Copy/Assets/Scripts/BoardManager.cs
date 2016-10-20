using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public ObjectMan[,] object_mans{ set; get; }
	private ObjectMan selected_objectman;

	public int width;
	public int height;

	private const float tile_size = 1.0f;
	private const float tile_offset = 0.5f;

	private int selection_x = -1;
	private int selection_z = -1;

	public List<GameObject> object_prefab;
	private List<GameObject> active_object;

	private Quaternion orientation = Quaternion.Euler(0,0,0);
	private void Start()
	{
		Spawn_All_Objects ();
	}

	private void Update()
	{
		Update_Selection ();
		Draw_Board ();
	}

	private void Update_Selection()
	{
		if (!Camera.main)
			return;
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("BoardPlane"))) {
			Debug.Log ((int)hit.point.x);
			Debug.Log ((int)hit.point.z);
			selection_x = (int)hit.point.x;
			selection_z = (int)hit.point.z;
		} else {
			selection_x = -1;
			selection_z = -1;
		}
	}

	private void Spawn_Object(int index, int x, int y) {
		GameObject go = Instantiate (object_prefab [index], Get_Tile_Center(x, y), orientation) as GameObject;
		go.transform.SetParent (transform);
		object_mans [x, y] = go.GetComponent<ObjectMan>();
		active_object.Add (go);
	}

	private void Spawn_All_Objects() {
		active_object =  new List<GameObject>();
		object_mans = new ObjectMan[8, 8];
		// Spawn the Human team
		Spawn_Object (0, 6,0);
		Spawn_Object (0, 7,0);
		Spawn_Object (0, 8,0);
		Spawn_Object (0, 9,0);

		//Spawn skeleton Team

		orientation = Quaternion.Euler(0,180,0);
		Spawn_Object (5, 6,14);
		Spawn_Object (5, 7,14);
		Spawn_Object (5, 8,14);
		Spawn_Object (5, 9,14);
	}

		

	private Vector3 Get_Tile_Center(int x, int y) {
		Vector3 origin = Vector3.zero;
		origin.x += (tile_size * x) + tile_offset;
		origin.z += (tile_size * y) + tile_offset;
		return origin;
	}

	private void Draw_Board()
	{
		Vector3 width_line = Vector3.right * width;
		Vector3 height_line = Vector3.forward * height;

		for(int i = 0; i <= width; ++i) {
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + width_line);
		}

		for (int j = 0; j <= height; ++j) {
			Vector3 start = Vector3.right * j;
			Debug.DrawLine (start, start + height_line);
		}

	// Draw the selection
	if (selection_x >= 0 && selection_z >= 0) {
		Debug.DrawLine(
				Vector3.forward * selection_z + Vector3.right * selection_x,
				Vector3.forward * (selection_z + 1) + Vector3.right * (selection_x + 1));
		Debug.DrawLine(
				Vector3.forward * (selection_z + 1) + Vector3.right * selection_x,
				Vector3.forward * selection_z + Vector3.right * (selection_x + 1));
		
		}
	}
}
	

			

				


