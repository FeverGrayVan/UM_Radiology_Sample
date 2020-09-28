//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class DrawLine : MonoBehaviour
//{
//    public Camera _camera;

//    private LineRenderer lineRenderer;
//    private float counter = 0f;
//    private float dist;

//    public Transform origin;
//    public Transform destination;

//    public float lineDrawSpeed = 6f;

//    float distanceBetweenTwoPointsInMM;

//    [SerializeField] public Transform distanceText;

//    // Start is called before the first frame update
//    void Start()
//    {
//        lineRenderer = GetComponent<LineRenderer>();
//        lineRenderer.SetPosition(0, origin.position);

//        dist = Vector3.Distance(origin.position, destination.position);
//        distanceBetweenTwoPointsInMM = dist * 1000;

//        Debug.Log("distance is :" + distanceBetweenTwoPointsInMM);

//        distanceText.position = (origin.position+ destination.position) / 2f;

//        distanceText.position = new Vector3(distanceText.position.x, distanceText.position.y + 0.1f, distanceText.position.z);

//        Quaternion distanceTextRotation = Quaternion.LookRotation(distanceText.transform.position - _camera.transform.position);
//        distanceText.transform.rotation = Quaternion.Slerp(distanceText.transform.rotation, distanceTextRotation, 1.5f);

//        distanceText.GetComponent<Text>().text = distanceBetweenTwoPointsInMM.ToString() + " mm";

//    }

//    // Update is called once per frame
//    void Update()
//    {
//            counter += 0.1f / lineDrawSpeed;
//            float x = Mathf.Lerp(0, dist, counter);

//            Vector3 pointA = origin.position;
//            Vector3 pointB = destination.position;

//            Vector3 pointALongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

//            lineRenderer.SetPosition(1, pointALongLine);

//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    public Camera _camera;

    private LineRenderer lineRenderer;
    private float counter = 0f;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed = 6f;

    float distanceBetweenTwoPointsInMM;

    [SerializeField] public Transform distanceText;

    // Start is called before the first frame update
    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.SetPosition(0, origin.position);

        dist = Vector3.Distance(origin.position, destination.position);
        distanceBetweenTwoPointsInMM = dist * 1000;

        Debug.Log("distance is :" + distanceBetweenTwoPointsInMM);

        distanceText.position = (origin.position + destination.position) / 2f;

        distanceText.position = new Vector3(distanceText.position.x, distanceText.position.y + 0.1f, distanceText.position.z + (-0.08f));

        Quaternion distanceTextRotation = Quaternion.LookRotation(distanceText.transform.position - _camera.transform.position);
        distanceText.transform.rotation = Quaternion.Slerp(distanceText.transform.rotation, distanceTextRotation, 0f);

        distanceText.GetComponent<Text>().text = distanceBetweenTwoPointsInMM.ToString() + " mm";

    }

    // Update is called once per frame
    void Update()
    {
        counter += 0.1f / lineDrawSpeed;
        float x = Mathf.Lerp(0, dist, counter);

        Vector3 pointA = origin.position;
        Vector3 pointB = destination.position;

        Vector3 pointALongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, pointALongLine);

    }
}

