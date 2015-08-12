using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkInstatiate : NetworkBehaviour {

	[SerializeField]Camera Cam;
	[SerializeField] Transform myTransform;

	// Use this for initialization
	public override void OnStartLocalPlayer () {
			GetComponent<CharacterController>().enabled = true;
			GetComponent<CharacterMovement>().enabled = true;
			GetComponent<Animator>().enabled = true;
			Cam.enabled = true;
			GetComponent<NetworkAnimator>().SetParameterAutoSend(0,true);
		//if(!isLocalPlayer){
			//myTransform.GetComponent<CharacterController>().enabled = true;
			//myTransform.GetComponent<CharacterMovement>().enabled = true;
			//myTransform.GetComponent<Animator>().enabled = true;
		//}
	}

	public override void PreStartClient(){
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0,true);
		GetComponent<Animator>().enabled = true;
	}

}
