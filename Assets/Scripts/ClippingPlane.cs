using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolumeRendering
{
    public class ClippingPlane : MonoBehaviour
    {

        [SerializeField] Transform clippingPlane;
        [SerializeField] Transform dicomModel;

        [SerializeField] Material nonTransMat;
        [SerializeField] Material transMat;

        [SerializeField] GameObject volumeRendering;
        bool clippingMode;

        Outline outline;

        // Start is called before the first frame update
        void Start()
        {
            volumeRendering = GameObject.Find("VolumeRendering");
            clippingMode = volumeRendering.GetComponent<VolumeRendering>().isPlaneModeOn;

            outline = clippingPlane.GetComponent<Outline>();
        }

        // Update is called once per frame
        void Update()
        {
            if (clippingMode)
            {
                FindObjectOfType<ManipulateObject>().enabled = true;
                FindObjectOfType<TrackController>().enabled = true;

                FindObjectOfType<ManipulateObject>().RotateModel(clippingPlane.gameObject);

                //Debug.Log("Inside update......");
                float distaceFromTheDicom = Vector3.Distance(clippingPlane.position, dicomModel.position);

                //Debug.Log("distance from the dicom : " + distaceFromTheDicom);

                if (distaceFromTheDicom < 1f)
                {
                    clippingPlane.GetComponent<MeshRenderer>().material = transMat;
                    outline.enabled = true;

                }
                else
                {
                    clippingPlane.GetComponent<MeshRenderer>().material = nonTransMat;
                    outline.enabled = false;
                }
            }
            else
            {
                FindObjectOfType<ManipulateObject>().enabled = false;
                FindObjectOfType<TrackController>().enabled = false;
            }

        }


    }
}
