using UnityEngine;
using System.Collections;

public class BallThrowScript : MonoBehaviour {

    public GameObject bowlingPinPrefab;
    public int inputNumber;

    private bool thrown = false;

    private Vector3 velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var controller = SteamVR_Controller.Input(inputNumber);

        if(controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            thrown = true;
            gameObject.GetComponent<SteamVR_TrackedObject>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            gameObject.GetComponent<Rigidbody>().AddRelativeForce((gameObject.GetComponent<Transform>().localPosition - velocity) * 10, ForceMode.VelocityChange);
        }

        if(!thrown)
        {
            if(!gameObject.GetComponent<SteamVR_TrackedObject>().enabled)
            {
                gameObject.GetComponent<SteamVR_TrackedObject>().enabled = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            }

            velocity = gameObject.GetComponent<Transform>().localPosition;
        }

        if (thrown && controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Pin");
            foreach (GameObject obj in allObjects)
            {
                Destroy(obj);
            }

            GameObject objrect = Instantiate(bowlingPinPrefab);
            objrect.GetComponent<Transform>().position = new Vector3(0.57f, -486.97f, 27.08f);
            thrown = false;

            gameObject.GetComponent<SteamVR_TrackedObject>().enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        }
    }
}
