using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]

public class CharacterMovement : MonoBehaviour {
	public float speed = 0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float VHSpeedDiv = 2.0F;
	private Animator myAnim;
	private Vector3 moveDirection = Vector3.zero;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	public Transform Cam;
	
	//NetworkView networkView;
	
	
	
	float rotationY = 0F;
	//NetworkView networkView;

	void jump(){
		if (jumpSpeed < 6.0f){
			jumpSpeed = 6.0f;
			Invoke ("jump",0.1f);
		}else{
			jumpSpeed = 0.0f;
			//moveDirection.z = speed;
		}

	}

	void Update() {
		//if (networkView.isMine) {
			CharacterController controller = GetComponent<CharacterController> ();
			if (controller.isGrounded) {
				moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speed * Input.GetAxis ("Vertical");
				moveDirection.y = jumpSpeed;
				if (Input.GetButton ("Jump") & myAnim.GetBool ("IsJump") == false) {
					Invoke ("jump", 0f);
					myAnim.SetBool ("IsJump", true);
					Invoke ("stopjump", 0.1f);
					//controller.Move (moveDirection * Time.deltaTime);
				} 
			}
			
			speed = myAnim.GetFloat ("VSpeed") * 4 + speed / 2;
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move (moveDirection * Time.deltaTime);

			myAnim.SetFloat ("VSpeed", Input.GetAxis ("Vertical") / VHSpeedDiv);
			myAnim.SetFloat ("HSpeed", Input.GetAxis ("Horizontal") / VHSpeedDiv);

			if (myAnim.GetFloat ("HSpeed") > 0 || myAnim.GetFloat ("HSpeed") < 0) {
				if (myAnim.GetFloat ("VSpeed") > 0) {
					myAnim.SetBool ("CanStrafe", true);
					myAnim.SetBool ("CanStrafeBack", false);
				} else if (myAnim.GetFloat ("VSpeed") < 0) {
					myAnim.SetBool ("CanStrafe", false);
					myAnim.SetBool ("CanStrafeBack", true);
				}
			} else {
				myAnim.SetBool ("CanStrafe", false);
				myAnim.SetBool ("CanStrafeBack", false);
			}

		if (axes == RotationAxes.MouseXAndY) {
			float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3 (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
			Cam.localEulerAngles = new Vector3 (-rotationY, 0, 0);
		} else if (axes == RotationAxes.MouseX) {
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			//rotationY = Mathf.Clamp (-Input.GetAxis("Mouse Y")*(180/Mathf.PI), minimumY, maximumY);
			transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
			
			//Cam.Rotate (rotationY, 0, 0);
		} else {
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3 (-rotationY, transform.localEulerAngles.y, 0);
		}
		//}
	}
	
	void Start(){
		//networkView = GetComponent<NetworkView> ();
		myAnim = GetComponent<Animator> ();
	}

	void stopjump(){
		myAnim.SetBool ("IsJump", false);
	}


}

