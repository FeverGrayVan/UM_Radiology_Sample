
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using VolumeRendering.Utils;

namespace VolumeRendering
{

    [RequireComponent (typeof(MeshRenderer), typeof(MeshFilter))]
    public class VolumeRendering : MonoBehaviour {

        public enum perspectiveView
        {
            Axial,
            Coronal,
            Saggital
        }

        [SerializeField] public Shader shader;
        protected Material material;
        public Material duplicateMaterial;

        public bool isPlaneModeOn;

        public Transform culler;

        public Transform Axis;

        public Vector3 cullerPos;

        public VolumeRendering duplicateVolumeRendering;
        public perspectiveView currentPerspective;
        public bool isDuplicate;

        [SerializeField] Color color = Color.white;
        [Range(0f, 1f)] public static float threshold = 0.5f;
        [Range(0.5f, 5f)] public static float intensity = 1.5f;
        [Range(0f, 1f)] public float sliceXMin = 0.0f, sliceXMax = 1.0f;
        [Range(0f, 1f)] public float sliceYMin = 0.0f, sliceYMax = 1.0f;
        [Range(0f, 1f)] public float sliceZMin = 0.0f, sliceZMax = 1.0f;
        public Quaternion axis = Quaternion.identity;

        public bool perspectiveIs3D;
        public GameObject duplicate;

        public Texture volume;

        CustomSlider _customSlider;

        protected virtual void Start () 
        {
            if(!isDuplicate)
            {
                CreateDuplicate();
            }
            material = new Material(shader);
            GetComponent<MeshFilter>().sharedMesh = Build();
            GetComponent<MeshRenderer>().sharedMaterial = material;
        }
        
        protected void Update () 
        {
            material.SetTexture("_Volume", volume);
            material.SetColor("_Color", color);
            material.SetFloat("_Threshold", threshold);
            material.SetFloat("_Intensity", intensity);
            material.SetMatrix("_AxisRotationMatrix", Matrix4x4.Rotate(axis));

            if (!isDuplicate)
            {
                UpdateDuplicate();
                if (isPlaneModeOn) 
                {
                    ClippingPlaneCalculation();
                }
                else 
                {
                    material.SetVector("_SliceMin", new Vector3(sliceXMin, sliceYMin, sliceZMin));
                    material.SetVector("_SliceMax", new Vector3(sliceXMax, sliceYMax, sliceZMax));
                }
            } 
            else
            {
                material.SetVector("_SliceMin", new Vector3(sliceXMin, sliceYMin, sliceZMin));
                material.SetVector("_SliceMax", new Vector3(sliceXMax, sliceYMax, sliceZMax));
            }

        }

        public void SetClippingMode(bool mode) 
        {
            isPlaneModeOn = mode;
        }

        public void ClippingPlaneCalculation()
        {
            Vector4 clip;
            Vector4 clipAdd;

            clip.x = ((0.4f * culler.localPosition.x) + 0.2f) / 0.4f;

            clip.y = -1;
            clip.z = -1;
            clip.w = -1;

            clipAdd.x = clip.x + 10;
            clipAdd.y = clip.y + 10;
            clipAdd.z = clip.z + 10;
            clipAdd.w = -1;

            material.SetVector("_SliceMin", clip);
            material.SetVector("_SliceMax", clipAdd);

            Axis.rotation = Quaternion.Inverse(culler.localRotation);
        }


        public void UpdateDuplicate()
        {
            if(currentPerspective == perspectiveView.Axial)
            {
                duplicateVolumeRendering.sliceXMin = 0;
                duplicateVolumeRendering.sliceXMax = 1;

                duplicateVolumeRendering.sliceYMin = 0;
                duplicateVolumeRendering.sliceYMax = 1;

                duplicateVolumeRendering.sliceZMin = 0;
                duplicateVolumeRendering.sliceZMax = sliceZMin;
            } 
            else if(currentPerspective == perspectiveView.Saggital)
            {
                duplicateVolumeRendering.sliceXMin = 0;
                duplicateVolumeRendering.sliceXMax = sliceXMin;

                duplicateVolumeRendering.sliceYMin = 0;
                duplicateVolumeRendering.sliceYMax = 1;

                duplicateVolumeRendering.sliceZMin = 0;
                duplicateVolumeRendering.sliceZMax = 1;
            }
            else if(currentPerspective == perspectiveView.Coronal)
            {
                duplicateVolumeRendering.sliceXMin = 0;
                duplicateVolumeRendering.sliceXMax = 1;

                duplicateVolumeRendering.sliceYMin = 0;
                duplicateVolumeRendering.sliceYMax = sliceYMin;

                duplicateVolumeRendering.sliceZMin = 0;
                duplicateVolumeRendering.sliceZMax = 1;
            }
        }

        public void ResetSlices()
        {   
            sliceXMin = sliceYMin = sliceZMin = 0;
            sliceXMax = sliceYMax = sliceZMax = 1;
        }

        public void RotateAxial()
        {
            duplicate.transform.localPosition = new Vector3(0,0,-1);
            currentPerspective = perspectiveView.Axial;
            ResetSlices();
        }

        public void RotateSaggital()
        {
            duplicate.transform.localPosition = new Vector3(-1,0,0);
            currentPerspective = perspectiveView.Saggital;
            ResetSlices();
        }

        public void RotateCoronal()
        {
            duplicate.transform.localPosition = new Vector3(0,-1,0);
            currentPerspective = perspectiveView.Coronal;
            ResetSlices();
        }       

        public void SwitchPerspective3D(bool perspective)
        {
            perspectiveIs3D = perspective;
            if(perspectiveIs3D)
            {
                duplicate.SetActive(true);
            } 
            else 
            {
                duplicate.SetActive(false);
            }
        }

        public void CreateDuplicate()
        {
            duplicate = Instantiate(this.gameObject);
            duplicate.transform.SetParent(this.gameObject.transform);
            Destroy(duplicate.GetComponent<TransformController>());
            duplicateMaterial = duplicate.GetComponent<Renderer>().material;
            duplicateVolumeRendering = duplicate.GetComponent<VolumeRendering>();
            duplicateVolumeRendering.isDuplicate = true;
            duplicate.SetActive(false);
        }

        Mesh Build() {
            var vertices = new Vector3[] {
                new Vector3 (-0.5f, -0.5f, -0.5f),
                new Vector3 ( 0.5f, -0.5f, -0.5f),
                new Vector3 ( 0.5f,  0.5f, -0.5f),
                new Vector3 (-0.5f,  0.5f, -0.5f),
                new Vector3 (-0.5f,  0.5f,  0.5f),
                new Vector3 ( 0.5f,  0.5f,  0.5f),
                new Vector3 ( 0.5f, -0.5f,  0.5f),
                new Vector3 (-0.5f, -0.5f,  0.5f),
            };
            var triangles = new int[] {
                0, 2, 1,
                0, 3, 2,
                2, 3, 4,
                2, 4, 5,
                1, 2, 5,
                1, 5, 6,
                0, 7, 4,
                0, 4, 3,
                5, 4, 7,
                5, 7, 6,
                0, 6, 7,
                0, 1, 6
            };

            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            mesh.hideFlags = HideFlags.HideAndDontSave;
            return mesh;
        }

         public void ThresholdBrain()
        {
            threshold = 0.7f;
            intensity = 1.5f;
        }

        public void ThresholdBones()
        {
            threshold = 0.5f;
            intensity = 3;
        }

        public void ThresholdLungs()
        {
            threshold = 0.8f;
            intensity = 1.5f;
        }

        public void ThresholdSoftTissues()
        {
            threshold = 1;
            intensity = 1;
        }

        void OnValidate()
        {
            Constrain(ref sliceXMin, ref sliceXMax);
            Constrain(ref sliceYMin, ref sliceYMax);
            Constrain(ref sliceZMin, ref sliceZMax);
        }

        void Constrain (ref float min, ref float max)
        {
            const float threshold = 0.04f;
            //const float threshold = 0.005f;

            if(min > max - threshold)
            {
                min = max - threshold;
            } else if(max < min + threshold)
            {
                max = min + threshold;
            }
        }

        void OnDestroy()
        {
            Destroy(material);
        }

    }

}


