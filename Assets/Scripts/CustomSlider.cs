using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;
using MagicLeap;
using System;
/// <summary>
/// This class was created because the UI default slider does not work with Magic Leap Input Module. 
/// </summary>
[RequireComponent(typeof(Slider))]
public class CustomSlider : MonoBehaviour
{

    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Transform _endPoint;
    private Slider _slider;
    private Vector3 _projectedPositionOnLine;
    private float _totalLineLength;
    private float _projectedPositionOnLineDistanceFromEnd;
    private bool _movingSlider;
    private MLInput.Controller _controller;

    //private GameObject _cursor;

    private GameObject _controlPointer;

    public GameObject thresholdPanel;

    //VolumeRendering.VolumeRendering _volumeRendering;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        //_controller = MLInput.GetController(MLInput.Hand.Left);
    }
    private void OnEnable()
    {
       
    }
    private void Update()
    {
        if (_movingSlider)
        {
            MoveSlider();
        }
    }
    public void FindCursor()
    {
        // _cursor = GameObject.Find("Cursor");
        _controlPointer = GameObject.Find("PointerCursor");

    }
    private void MoveSlider()
    {
       // _projectedPositionOnLine = NearestPointOnFiniteLine(_startPoint.position, _endPoint.position, _cursor.transform.position);

        _projectedPositionOnLine = NearestPointOnFiniteLine(_startPoint.position, _endPoint.position, _controlPointer.transform.position);

        _totalLineLength = Vector3.Distance(_startPoint.position, _endPoint.position);
        _projectedPositionOnLineDistanceFromEnd = Vector3.Distance(_projectedPositionOnLine, _endPoint.position);
        _slider.value = _slider.maxValue - (_projectedPositionOnLineDistanceFromEnd / _totalLineLength) * _slider.maxValue;
        //  _slider.value = (float)Math.Round(_slider.value, 0);

        Debug.Log("slider value-------" + _slider.value);

        if (thresholdPanel.activeInHierarchy)
        {
            VolumeRendering.VolumeRendering.threshold = _slider.value;
            VolumeRendering.VolumeRendering.intensity = _slider.value;
        }
       
    }
    public void SetSliderMoving(bool b)
    {
        if (b)
        {
            FindCursor();
        }
        _movingSlider = b;
    }
    public static Vector3 NearestPointOnFiniteLine(Vector3 start, Vector3 end, Vector3 pnt)
    {
        var line = (end - start);
        var len = line.magnitude;
        line.Normalize();

        var v = pnt - start;
        var d = Vector3.Dot(v, line);
        d = Mathf.Clamp(d, 0f, len);
        return start + line * d;
    }

}