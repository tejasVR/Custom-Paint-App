using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLine : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    private GameObject line;

    // Use this for initialization
    void Start () {

        line = GameObject.Find("New Game Object");
		
	}
	
	// Update is called once per frame
	void Update () {

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPress(SteamVR_Controller.ButtonMask.Grip) && GameObject.Find("New Game Object"))
        {
            Destroy(line);
            device.TriggerHapticPulse(500);
        }


    }
}
