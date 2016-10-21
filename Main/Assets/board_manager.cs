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
	
	// Mouse positions  (-1 just a placeholder)
	private int selection_x = -1;
	private int selection_z = -1;
	
	
	private void Update() {
		
		update_selection();
		draw_chest_board();
		
		
	}
	
	// Function draws the chest board, can be edited using inspector:
	// Also draws cross over any tiles the mouse is over
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
		
		// Drawing Cross on corresponding tile mouse is hovered againts:
		if (selection_x >= 0 && selection_z >= 0) {
			Vector3 bottom_left_point = (height_unit_vector * selection_z) + (width_unit_vector * selection_x);
			Vector3 bottom_right_point = (height_unit_vector * selection_z) + (width_unit_vector * (selection_x + 1));
			Vector3 top_right_point = (height_unit_vector * (selection_z + 1)) + (width_unit_vector * (selection_x + 1));
			Vector3 top_left_point = height_unit_vector * (selection_z + 1) + (width_unit_vector * selection_x);
			Debug.DrawLine(bottom_left_point, top_right_point);
			Debug.DrawLine(bottom_right_point, top_left_point);
		}
		if (selection_x >= 0 && selection_z >= 0) {
			Vector3 bottom_left_point = (height_unit_vector * selection_z) + (width_unit_vector * selection_x);
			Vector3 right_right_point = (height_unit_vector * (selection_z + 1)) + (width_unit_vector * (selection_x + 1));
			Debug.DrawLine(bottom_left_point, right_right_point);
		}
	}
	
	//Function that updates position of mouse ONLY if its on the board
	// otherwise setting position x and z to -1.
	
	private void update_selection() {
		// Ray structure: At every frame we want a ray defined through the camera to the mouse position
		Ray camera_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Layer structure: represents layers we can define in the inspector 
		int board_layer = LayerMask.GetMask("board_plane");
		
		// Will act as a storage variable IF we hit anything..
		RaycastHit hit;
		// if there is no camera, 
		if (!Camera.main) return;
		// If we hit something from the camera to the board layer do (while storing in hit)	
		if (Physics.Raycast(camera_ray,out hit, 25.0f,board_layer) ) {
			selection_x = (int) hit.point.x;
			selection_z = (int) hit.point.z;
		}
		else {
			selection_x = -1;
			selection_z = -1;
		}
		
	}
	
	
}
