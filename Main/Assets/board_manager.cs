using UnityEngine;
using System.Collections;

public class board_manager : MonoBehaviour {

	// Unit vectors
	private  Vector3 width_unit_vector = Vector3.right;
	private  Vector3 height_unit_vector = Vector3.forward;
	
	// Tile Dimensions
	public int width;
	public int height;
	
	// Lengths of each tile, and tile center:
	private const float tile_length = 1.0f;
	private const float tile_center = 0.5f;
	
	// Mouse positions 
	private int selection_x;
	private int selection_z;
	
	private void Update() {
		draw_chest_board();
		
	}
	
	// Function draws the chest board, can be edited using inspector:
	private void draw_chest_board() {
		// right is (x,y,z) = (1,0,0)
		Vector3 width_line = width_unit_vector * width;
		// forward is (x,y,z) = (0,0,1)
		Vector3 height_line = height_unit_vector * height;
		
		// Drawing the Board in the x (width) direction using for loops:
		for (int i = 0; i <= width; ++i) {
			Vector3 growing_height_unit_vector = height_unit_vector * i;
			Debug.DrawLine(growing_height_unit_vector, width_line + growing_height_unit_vector);
		}
		// Drawing the Board in the z (height) direction using for loops:
		for (int i = 0; i <= height; ++i) {
			Vector3 growing_width_unit_vector = width_unit_vector * i;
			Debug.DrawLine(growing_width_unit_vector, height_line + growing_width_unit_vector);
		}
	}
}
