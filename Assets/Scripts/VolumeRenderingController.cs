using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

namespace VolumeRendering
{

    public class VolumeRenderingController : MonoBehaviour {

        [SerializeField] protected VolumeRendering volume, volumeAxial, volumeSaggital, volumeCoronal;
       // [SerializeField] protected Slider sliderXMinMax, sliderYMinMax, sliderZMinMax; //sliderAxial, sliderSaggital, sliderCoronal; //sliderRotX, sliderRotY, sliderRotZ;
        [SerializeField] protected Transform axis;
        [SerializeField] protected GameObject sliceAxial, sliceSaggital, sliceCoronal;
        const float threshold = 0.065f;

        [SerializeField] protected Slider sliderThreshold, sliderIntensity;

        CustomObjectMovement objMove;

        public GameObject _slicePanel;
        public GameObject _slicingArrowsPanel;

        bool Axial =false;
        bool Saggital = false;
        bool Coronal = false;
 

        void Start ()
        {

            //sliderXMinMax.onValueChanged.AddListener((v) => {
            //    volume.sliceXMin = sliderXMinMax.value = Mathf.Min(v, volume.sliceXMax - threshold);
            //});
            ////sliderXMax.onValueChanged.AddListener((v) => {
            ////    volume.sliceXMax = sliderXMax.value = Mathf.Max(v, volume.sliceXMin + threshold);
            ////});

            //sliderYMinMax.onValueChanged.AddListener((v) => {
            //    volume.sliceYMin = sliderYMinMax.value = Mathf.Min(v, volume.sliceYMax - threshold);
            //});
            ////sliderYMax.onValueChanged.AddListener((v) => {
            ////    volume.sliceYMax = sliderYMax.value = Mathf.Max(v, volume.sliceYMin + threshold);
            ////});

            //sliderZMinMax.onValueChanged.AddListener((v) => {
            //    volume.sliceZMin = sliderZMinMax.value = Mathf.Min(v, volume.sliceZMax - threshold);
            //});
            ////sliderZMax.onValueChanged.AddListener((v) => {
            ////    volume.sliceZMax = sliderZMax.value = Mathf.Max(v, volume.sliceZMin + threshold);
            ////});

            ////sliderThreshold.onValueChanged.AddListener((t) => {
            ////   Debug.Log( t+ "shader name : "+volume.shader.name);
            ////});

            //ChangePreview();
        }

        void Update()
        {
            volume.axis = axis.rotation;

            CalculatePosition();

            if (_slicePanel.activeInHierarchy)
            {
                ChangePreview();
            }

            if (_slicingArrowsPanel.activeInHierarchy)
            {
                ModelSlicer();
            }

        }

        public void ModelSlicer()
        {
           // Debug.Log("Model Slicer ..........");
            if (Saggital)
            {
                Debug.Log("Model Slicer Coronal");
               // sliderXMinMax.onValueChanged.AddListener((v) => {
                    //volume.sliceXMin = sliderXMinMax.value = Mathf.Min(v, volume.sliceXMax - threshold);
                    volume.sliceXMin = CustomObjectMovement.moveValue = Mathf.Min(CustomObjectMovement.moveValue, volume.sliceXMax - threshold);
                //});
            }

            //sliderXMax.onValueChanged.AddListener((v) => {
            //    volume.sliceXMax = sliderXMax.value = Mathf.Max(v, volume.sliceXMin + threshold);
            //});
            if (Coronal)
            {
                Debug.Log("Model Slicer Saggital");
               // sliderYMinMax.onValueChanged.AddListener((v) => {
                    //volume.sliceYMin = sliderYMinMax.value = Mathf.Min(v, volume.sliceYMax - threshold);
                    volume.sliceYMin = CustomObjectMovement.moveValue = Mathf.Min(CustomObjectMovement.moveValue, volume.sliceYMax - threshold);
               // });
            }

            //sliderYMax.onValueChanged.AddListener((v) => {
            //    volume.sliceYMax = sliderYMax.value = Mathf.Max(v, volume.sliceYMin + threshold);
            //});
            if (Axial)
            {
                Debug.Log("Model Slicer Axial");
               // sliderZMinMax.onValueChanged.AddListener((v) => {
                    //volume.sliceZMin = sliderZMinMax.value = Mathf.Min(v, volume.sliceZMax - threshold);
                    volume.sliceZMin = CustomObjectMovement.moveValue = Mathf.Min(CustomObjectMovement.moveValue, volume.sliceZMax - threshold);
               // });
            }
           
            //sliderZMax.onValueChanged.AddListener((v) => {
            //    volume.sliceZMax = sliderZMax.value = Mathf.Max(v, volume.sliceZMin + threshold);
            //});

            //sliderThreshold.onValueChanged.AddListener((t) => {
            //   Debug.Log( t+ "shader name : "+volume.shader.name);
            //});
        }

        public void ChangePreview()
        {
            if (Axial)
            {
                //volumeAxial.sliceZMin = sliderAxial.value;
                volumeAxial.sliceZMin = CustomObjectMovement.moveValue;
                volumeAxial.sliceZMax = volumeAxial.sliceZMin + threshold;
            }

            if (Saggital)
            {
                //volumeSaggital.sliceXMin = sliderSaggital.value;
                volumeSaggital.sliceXMin = CustomObjectMovement.moveValue;
                volumeSaggital.sliceXMax = volumeSaggital.sliceXMin + threshold;
            }

            if (Coronal)
            {
                //volumeCoronal.sliceYMin = sliderCoronal.value;
                Debug.Log("movie value in controller :" + CustomObjectMovement.moveValue);
                volumeCoronal.sliceYMin = CustomObjectMovement.moveValue;
                volumeCoronal.sliceYMax = volumeCoronal.sliceYMin + threshold;
            }
          
        }

        //public void ChangeRotation()
        //{
        //    axis.eulerAngles = new Vector3(sliderRotX.value, sliderRotY.value, sliderRotZ.value);
        //}

        public void CalculatePosition()
        {
            //float axialSlicePos = (0.5f * sliderAxial.value - 0.25f) / 0.5f;
            float axialSlicePos = (0.5f * CustomObjectMovement.moveValue - 0.25f) / 0.5f;
            sliceAxial.transform.localPosition = new Vector3(sliceAxial.transform.localPosition.x, sliceAxial.transform.localPosition.y, axialSlicePos);

            //float saggitalSlicePos = (0.5f * sliderSaggital.value - 0.25f) / 0.5f;
            float saggitalSlicePos = (0.5f * CustomObjectMovement.moveValue - 0.25f) / 0.5f;
            sliceSaggital.transform.localPosition = new Vector3(saggitalSlicePos, sliceSaggital.transform.localPosition.y, sliceSaggital.transform.localPosition.z);

            //float coronalSlicePos = (0.5f * sliderCoronal.value - 0.25f) / 0.5f;
            float coronalSlicePos = (0.5f * CustomObjectMovement.moveValue - 0.25f) / 0.5f;
            sliceCoronal.transform.localPosition = new Vector3(sliceCoronal.transform.localPosition.x, coronalSlicePos, sliceCoronal.transform.localPosition.z);
        }

        //public void OnIntensity(float v)
        //{
        //    volume.intensity = v;
        //}

        //public void OnThreshold(float v)
        //{
        //    volume.threshold = v;
        //}

        //public void TestBuild()
        //{
        //    string path = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
        //    int width = 512;
        //    int height = 512;
        //    int depth = 56;

        //    var max = width * height * depth;
        //    var tex  = new Texture3D(width, height, depth, TextureFormat.ARGB32, false);
        //    tex.wrapMode = TextureWrapMode.Clamp;
        //    tex.filterMode = FilterMode.Bilinear;
        //    tex.anisoLevel = 0;

        //    string[] filePaths = Directory.GetFiles(path);
        //    foreach(string item in filePaths)
        //    {
        //        Debug.Log("Item name =" + item);
        //    }



        //    AssetDatabase.CreateAsset(tex, "Assets/MRITEST.asset");
        //    AssetDatabase.SaveAssets();
        //    AssetDatabase.Refresh();
        //}

    //    void SetBool(string str)
    //    {
    //        char[] arr = str.ToCharArray();
    //        bool b = false;

    //        if(arr[1] == 't')
    //        {
    //            b = true;
    //        }
    //        else if (arr[1] == 'f')
    //        {
    //            b = false;
    //        }

    //        Debug.Log("inside set bool : bool value "+ b + "string is : "+str);

    //        if (arr[0] == 'a')
    //        {
    //            Axial = b;
    //            Saggital = false;
    //            Coronal = false;
    //        }else

    //        if(arr[0] == 's')
    //        {
    //            Saggital = b;
    //            Axial = false;
    //            Coronal = false;
    //        }else

    //        if(arr[0] == 'c')
    //        {
    //            Coronal = b;
    //            Axial = false;
    //            Saggital = false;
    //        }
           

    //}

        public void SetBoolAxialT()
        {
            Axial = true;
            Saggital = false;
            Coronal = false;
        }

        public void SetBoolAxialF()
        {
            Axial = false;
        }

        public void SetBoolSaggitalT()
        {
            Axial = false;
            Saggital = true;
            Coronal = false;
        }

        public void SetBoolSaggitalF()
        {
            Saggital = false;
        }

        public void SetBoolCoronalT()
        {
            Axial = false;
            Saggital = false;
            Coronal = true;
        }

        public void SetBoolCoronalF()
        {
            Coronal = false;
        }


    }

    
}


