using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolumeRendering
{

    public class Axis : MonoBehaviour {

        [SerializeField] protected Transform root;
        [SerializeField] protected float length = 5f;
        public GameObject Plane;

        protected void OnDrawGizmos()
        {
            Gizmos.matrix = Matrix4x4.Rotate(root.rotation) * Matrix4x4.Rotate(transform.rotation).inverse;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.zero, Vector3.right * length);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector3.zero, Vector3.up * length);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Vector3.zero, Vector3.forward * length);
        }

        public void Update()
        {
            //float AxisX = Plane.transform.localRotation.eulerAngles.x - 180;
            //float AxisY = 0;
            //float AxisZ = Plane.transform.localRotation.eulerAngles.z - 180;
            //transform.rotation = Quaternion.Euler(AxisX, AxisY, AxisZ);
        }

    }

}


