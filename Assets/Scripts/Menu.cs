using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using MagicLeapTools;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public ControlInput controlInput;
    public GameObject menuWindow;

    private MLInput.Controller _controller = null;

    // Start is called before the first frame update
    void Start()
    {
        if (!MLInput.IsStarted)
            MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonUp += OnButtonUp;
    }

    void OnButtonUp(byte controllerId, MLInput.Controller.Button button)
    {
        if (controllerId == _controller.Id && button == MLInput.Controller.Button.HomeTap)
        {
            menuWindow.SetActive(!menuWindow.activeInHierarchy);
        }
    }
}
