using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    public Material lmat;

	public SteamVR_TrackedObject trackedObj;
    private MeshLineRenderer currLine;
    private int numClicks = 0;

    private float width = .1f;

    //private GameObject line;

    // Update is called once per frame
    void Update () {

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            device.TriggerHapticPulse(500);

            GameObject go = new GameObject("line");
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            currLine = go.AddComponent<MeshLineRenderer>();

            currLine.lmat = new Material(lmat);
            currLine.setWidth(width);

            //currLine.startWidth = .1f;
            //currLine.endWidth = .1f;

        } else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.AddPoint(trackedObj.transform.position);
            //currLine.positionCount = numClicks + 1;
            //currLine.SetPosition(numClicks, trackedObj.transform.position);
            numClicks++;
        } else if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
        {
            numClicks = 0;
            currLine = null;
            device.TriggerHapticPulse(500);
        }

        if (currLine != null)
        {
            currLine.lmat.color = ColorManager.Instance.GetCurrentColor();
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Grip) && GameObject.Find("line"))
        {
            GameObject line = (GameObject)GameObject.Find("line");
            GameObject.Destroy(line);
            device.TriggerHapticPulse(1000);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad;

            touchpad = device.GetAxis();

            if (touchpad.y >= 0f)
            {
                width += .01f;
                if (width > 2f)
                {
                    width = 2f;
                }
            }

            if (touchpad.y < 0f)
            {
                width -= .01f;
                if (width >= .01f)
                {
                    width = .01f;
                }
            }

            device.TriggerHapticPulse(500);

        }

    }
}
