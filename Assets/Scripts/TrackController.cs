using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class TrackController : MonoBehaviour
{
    public bool bumperPress = false;
    public bool homeTap = false;
    public bool trigger = false;
    public Vector3 touchpad = Vector3.zero;
    private MLInput.Controller controller;

    #region Unity Methods
    void Start()
    {
        MLInput.Start();
        controller = MLInput.GetController(MLInput.Hand.Left);
    }
    void Update()
    {
        TouchpadInput();
    }

    void OnDestroy()
    {
        MLInput.Stop();
    }
    #endregion

    public void BumperPressed()
    {
        Debug.Log("pressed bumper");
        if (!bumperPress)
            bumperPress = true;
    }

    public void HomeTapped()
    {
        Debug.Log("tapped home");
        if (!homeTap)
            homeTap = true;
    }

    public void TouchpadInput()
    {
        touchpad.x = controller.Touch1PosAndForce.x;
        touchpad.y = controller.Touch1PosAndForce.y;
        touchpad.z = controller.Touch1PosAndForce.z;
    }

    public void ResetControllerInput()
    {
        bumperPress = false;
        homeTap = false;
        trigger = false;
        touchpad = Vector3.zero;
    }


}