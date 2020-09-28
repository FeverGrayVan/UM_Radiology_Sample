using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ManipulateObject : MonoBehaviour
{
    #region Instance Variables
    [SerializeField] public float rotationSpeed = 2f; // speed at which x and y rotation of object occur

    private static TrackController controllerTracking; // Object that tracks controller input
    private float x, y, distance; // Magic Leap Controller Touchpad values
    private float alpha; // angle formed on first press on Z Ring
    private bool firstZPress = true; // first press on the Z Ring
    float theta; // change in z angle
    float originalRotZ; // store z rotation of object between movements on Z Ring
    private float xRot = 0f, yRot = 0f, zRot = 0; // initial values of model
    private Quaternion newRotation; // target rotation for model to move to
    float sphereYRot; // stores current y rotation of sphere
    #endregion

    #region Unity Methods
    /* Loads on Unity Engine Start
     * 1 Start Magic Leap Input
     * 2 Set Controller Tracking Info
    */
    void Start()
    {
        MLInput.Start();
        controllerTracking = GetComponent<TrackController>();
    }

    /* Upon ending Unity App
     * 1 Stop Magic Leap Input
    */
    private void OnDestroy()
    {
        MLInput.Stop();
    }
    #endregion

    #region Rotating Object

    #region public methods
    /* Handles rotating of dicomModel provided
     * 1 Updates MLController Touchpad input
     * 2 Updates rotation of model given the input
    */
    public void RotateModel(GameObject _model)
    {
        UpdateTouchPadXYZ(_model);

        newRotation = Quaternion.Euler(xRot, yRot, zRot);
        _model.transform.rotation = Quaternion.Slerp(_model.transform.rotation, newRotation, rotationSpeed);
    }

    /* Handles rotating of axis ring visuals
     * 1 Updates y axis ring visual to follow sphere's y rotation
    */
    public void RotateAxis(GameObject _model)
    {
        sphereYRot = _model.transform.rotation.eulerAngles.y;
    }

    #endregion

    #region private methods
    /* Rotates selected game object via the touchpad
     * 1 Given the touchpad is active, then calculate values; if not, then reset firstZPress
     * 2 Obtain values for x and y from touchpad; distanceFromCenter through x and y
     * 3 Given user presses down on touchpad, then do the following
     *   > if distance <= 0.6,
     *          UpdateXY == change in x and y are directly calculated through x and y axis on the touchpad
     *          reset firstZPress
     *          set original zRot to current zRot
     *   > if distance > 0.6,
     *          activate Z axis ring visual
     *          given firstZPress is true, then set alpha and turn off firstZPress, set original zRot to current zRot
     *          UpdateZ == change in z through interpolation of the x and y between updates around the Z Ring (using angles formed)        
    */
    private void UpdateTouchPadXYZ(GameObject _model)
    {
        x = controllerTracking.touchpad.x;
        y = controllerTracking.touchpad.y;
        distance = Mathf.Sqrt(x * x + y * y);

        if (distance <= 0.6f)
        {
            UpdateTouchXandY();
            firstZPress = true;
            originalRotZ = zRot;
        }
        else
        {
           // zAxisRing.Play();

            if (firstZPress)
            {
                alpha = CurrentAngle();
                firstZPress = false;
                originalRotZ = zRot;
            }
            UpdateTouchZ();
        }
    }

    // Stop rotation of model
    public void stopRotation()
    {
        rotationSpeed = 0;
    }

    /* Update x and y rotaton for selected game object
     * 1 Check if x and y are in the range of inputs
     * 2 Update the rotation based on the rotationSpeed
     * 3 Activate axis rings appropriately
    */
    private void UpdateTouchXandY()
    {
        if (x >= 0.2)
        {
            yRot += rotationSpeed;
        }

        if (x <= -0.2)
        {
            yRot -= rotationSpeed;   
        }

        if (y >= 0.2)
        {
            xRot += rotationSpeed;
        }

        if (y <= -0.2)
        {
            xRot -= rotationSpeed;
        }
    }

    /* Update z rotation for selected game object
     * 1 Every first press on the Z ring will become the initial z rotation (alpha)
     * 2 As user drags their finger along this range (Z ring) store the current angle every iteration (beta)
     * 3 Update the rotation based on the change in the angle (theta)
    */
    private void UpdateTouchZ()
    {
        float beta = CurrentAngle();
        theta = beta - alpha;

        zRot = originalRotZ + theta;
    }

    /* Find the current angle accounting for the quadrant
     * 1 Find the vector going from the center of the touchpad towards the point (x,y)
     * 2 Calculate the directionAngle it creates
     * 3 Account for the quadrant (i.e. 85* in the 3rd Quadrant is actually 265*)
    */
    private float CurrentAngle()
    {
        float directionAngle = 0f;

        if (x != 0)
            directionAngle = Mathf.Atan(y / x) * Mathf.Rad2Deg;

        if (x > 0 && y > 0)
            return directionAngle;
        else if (x < 0 && y > 0)
            return directionAngle + 180;
        else if (x < 0 && y < 0)
            return directionAngle + 180;
        else if (x > 0 && y < 0)
            return directionAngle + 360;
        else if (x > 0 && y == 0)
            return 0;
        else if (x == 0 && y > 0)
            return 90;
        else if (x < 0 && y == 0)
            return 180;
        else if (x == 0 && y < 0)
            return 270;
        else
            return 0;
    }
    #endregion

    #endregion

}