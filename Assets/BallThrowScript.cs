using UnityEngine;
using System.Collections;

public class BallThrowScript : MonoBehaviour {

    public GameObject bowlingPinPrefab;
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

        if (thrown && controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Pin");
            foreach (GameObject obj in allObjects)
            {
                Destroy(obj);
            }

            GameObject objrect = Instantiate(bowlingPinPrefab);
            objrect.GetComponent<Transform>().position = new Vector3(-108.4538f, -486.8208f, -125.3749f);
            thrown = false;
        }
    }
}
