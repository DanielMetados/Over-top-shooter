using UnityEngine;
using System.Collections;

public class CameraStablizerScript : MonoBehaviour {
	public Transform Char;
	public float X;
	public float Y;
	public float Z;
	public float wX;
	public float wY;
	public float wZ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		X = transform.position.x;
		Y = transform.position.y;
		Z = transform.position.z;

		wX = Char.position.x;
		wY = Char.position.y+9;
		wZ = Char.position.z-2;

		transform.position = new Vector3(wX,wY,wZ);
		transform.localEulerAngles = new Vector3 (70, 0, 0);
	}
}
