using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFallingFloor : MonoBehaviour {

	bool shaking, down, wait, up, set_position;
	public Rigidbody rb;
	int distance_to_drop = 8;
	float shaking_distance = 0.1f;
	float shaking_val = 30f;
	float shaking_time, down_time, wait_time, up_time, tmp_time;
	Vector3 initial_position, final_position, new_position;
	public Material verde, rojo;
	public MeshRenderer mr;

	void Start () {
		initial_position = transform.position;
		final_position = new Vector3 (initial_position.x, initial_position.y-distance_to_drop, initial_position.z);
		shaking = down = up = false;
	}

	public void drop(float time){
		mr.material = rojo;
		shaking_time = time *  40/ 100;
		down_time = time *  5/ 100;
		wait_time = time * 20/100;
		up_time = time *  35/ 100;
		shaking = true;
	}

	void Update(){
		if (shaking) {
			//new_position = new Vector3(initial_position.x, initial_position.y + (Mathf.Sin(Time.time * shaking_val)*shaking_distance/2), initial_position.z );
			shaking_time -= Time.deltaTime;
			if (shaking_time<=0){
				tmp_time = down_time;
				shaking = false;
				down = true;
			}
		}else if (down){
			down_time -= Time.deltaTime;
			new_position = Vector3.Lerp (initial_position, final_position, Mathf.InverseLerp (tmp_time, 0, down_time));
			if (down_time<=0) {
				down = false;
				wait = true;
				new_position = final_position;
			}
		}else if (wait){
			wait_time -= Time.deltaTime;
			if (wait_time<=0) {
				wait = false;
				up = true;
				tmp_time = up_time;
			}
		}else if (up){
			up_time -= Time.deltaTime;
			new_position = Vector3.Lerp (final_position, initial_position, Mathf.InverseLerp (tmp_time, 0, up_time));
			if (up_time<=0) {
				set_position = true;
				up = false;
				new_position = initial_position;
				mr.material = verde;
			}
		}
	}

	void FixedUpdate(){
		if (up || down || set_position) {
			rb.MovePosition (new_position);
			if (set_position)set_position = false;
		}
	}
}
