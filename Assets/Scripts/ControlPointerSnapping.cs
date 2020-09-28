using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicLeapTools
{
    public class ControlPointerSnapping : MonoBehaviour
    {
        [SerializeField]
        ControlInput controlInput;

        [SerializeField] Transform snapPoint;

        public Pointer pointer;

        private void Reset()
        {
            //refs:
            pointer = controlInput.transform.parent.GetComponent<Pointer>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("inside collision enter");
            if(collision.gameObject.tag== "Pointer")
            {
                //controlInput.GetComponent<Pointer>().Origin.Set(snapPoint.position.x, snapPoint.position.y, snapPoint.position.z);
                controlInput.GetComponent<Pointer>().Tip.Set(snapPoint.position.x, snapPoint.position.y, snapPoint.position.z);

                Debug.Log("snapped to the snap point");
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Debug.Log("inside collision exit");
            controlInput.transform.position = pointer.Tip;
            controlInput.transform.rotation = Quaternion.LookRotation(pointer.Normal);

            Debug.Log("removed snap point ");

        }
    }

}
