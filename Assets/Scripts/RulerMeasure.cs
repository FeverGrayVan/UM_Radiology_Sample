using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicLeapTools;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

public class RulerMeasure : MonoBehaviour
{
    //public Pointer pointer;
    [SerializeField] GameObject Marker;
    [SerializeField] GameObject Marker2;
    List<GameObject> Markers = new List<GameObject>();

    [SerializeField] PointerCursor cursor;

    //[SerializeField] ControlInput _controlInput;

    //[SerializeField] private StateHandler _stateHandler;

    //[SerializeField] private State _showMarkerState;
    //[SerializeField] private State _placeMarker1State;
    //[SerializeField] private State _placeMarker2State;

    //bool state1 = false;
    //bool state2 = false;

    //MLInput.Controller _controller;

    //MLInput.Controller.Button button;

    int counter = 0;

    [SerializeField] GameObject LineMeasure;


    [SerializeField] GameObject doneButton;

    //Init:
    private void Start()
    {
        MLInput.OnTriggerDown += HandleOnTriggerDown;
    }

    private void Update()
    {
        //ShowMarker();
       // PlaceMarker1();
       // PlaceMarker2();
    }

    public void ShowMarker()
    {
        //if (_stateHandler.currentState != _showMarkerState)
        //{
        //    return;
        //}

        // Debug.Log("State 1...........");

        Debug.Log("Show Marker .....");
        Marker.SetActive(true);

        //  _stateHandler.SwitchToNextState();
        counter = 1;

    }

    void HandleOnTriggerDown(byte controllerId, float pressure)
    {
        if (this.enabled)
        {
            if (counter == 1)
            {
                PlaceMarker1();
            }
            else if (counter == 2)
            {
                PlaceMarker2();


                // LineMeasure.GetComponent<DrawLine>().enabled = true;
            }

            counter += 1;
        }
       
    }

    void PlaceMarker1()
    {
        Debug.Log("Place Marker 1 .....");

        Vector3 _MarkerLocation = cursor.transform.position;
        GameObject _marker = Instantiate(Marker2, _MarkerLocation, Quaternion.identity);
        Markers.Add(_marker);

        LineMeasure.GetComponent<DrawLine>().origin = _marker.transform;
    }

    void PlaceMarker2()
    {
        Debug.Log("Place Marker 2 .....");

        Vector3 _MarkerLocation = cursor.transform.position;
        GameObject _marker = Instantiate(Marker2, _MarkerLocation, Quaternion.identity);
        Markers.Add(_marker);

        LineMeasure.GetComponent<DrawLine>().destination = _marker.transform;

        LineMeasure.SetActive(true);

       // clearMarkers.SetActive(true);

        doneButton.SetActive(true);
    }

    public void ClearMarkers()
    {
        if (Markers.Count > 0)
        {
            Debug.Log("marker count : " + Markers.Count);
            foreach (GameObject _marker in Markers)
            {
                Destroy(_marker);
            }
            Markers.Clear();

            LineMeasure.GetComponent<LineRenderer>().positionCount = 0;
            LineMeasure.GetComponent<DrawLine>().distanceText.GetComponent<Text>().text = "";
            LineMeasure.SetActive(false);

           

        }

        
        LineMeasure.GetComponent<LineRenderer>().positionCount = 2;

        LineMeasure.GetComponent<DrawLine>().origin = null;
        LineMeasure.GetComponent<DrawLine>().destination = null;

        ShowMarker();

    }

    public void Done()
    {
        if (Markers.Count > 0)
        {
            Debug.Log("marker count : " + Markers.Count);
            foreach (GameObject _marker in Markers)
            {
                Destroy(_marker);
            }
            Markers.Clear();

            LineMeasure.GetComponent<LineRenderer>().positionCount = 0;
            LineMeasure.GetComponent<DrawLine>().distanceText.GetComponent<Text>().text = "";
            LineMeasure.SetActive(false);

        }

        counter = 0;
        LineMeasure.GetComponent<LineRenderer>().positionCount = 2;
    }

    public void SetCounter()
    {
        counter = 0;
    }

    //void PlaceMarker1()
    //{
    //    if (_stateHandler.currentState != _placeMarker1State)
    //    {
    //        return;
    //    }

    //    if (_controlInput.Trigger && _controlInput.TriggerValue <= 0.1)
    //    {
    //        Debug.Log("State 2...........");
    //        if (Markers.Count < 1)
    //        {
    //            Vector3 _MarkerLocation = cursor.transform.position;
    //            GameObject _marker = Instantiate(Marker2, _MarkerLocation, Quaternion.identity);
    //            Markers.Add(_marker);

    //            Debug.Log("marker count 1 : " + Markers.Count);

    //        }

    //        _stateHandler.SwitchToNextState();

    //    }

    //    // controlInput.GetComponent<ControlHaptics>().touchDown.intensity = MLInput.Controller.FeedbackIntensity.Low;
    //}

    //void PlaceMarker2()
    //{
    //    if (_stateHandler.currentState != _placeMarker2State)
    //    {
    //        return;
    //    }

    //    if (_controlInput.Trigger && _controlInput.TriggerValue <= 0.1)
    //    {
    //        Debug.Log("State 3...........");
    //        if (Markers.Count == 1)
    //        {
    //            Vector3 _MarkerLocation = cursor.transform.position;
    //            GameObject _marker = Instantiate(Marker2, _MarkerLocation, Quaternion.identity);
    //            Markers.Add(_marker);

    //            Debug.Log("marker count 2 : " + Markers.Count);

    //        }


    //    }


    //}


}
