using UnityEngine;
using System.Collections;

public class BallThrowScript : MonoBehaviour {

    public GameObject controllerObject;
    public int inputNumber;

    private bool thrown = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var controller = SteamVR_Controller.Input(inputNumber);

        if(controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            thrown = true;
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(controller.velocity);
        }

        if(!thrown)
        {
            gameObject.GetComponent<Transform>().position = controllerObject.GetComponent<Transform>().position;
        }
    }
}
