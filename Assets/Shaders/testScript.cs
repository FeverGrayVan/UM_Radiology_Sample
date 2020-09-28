using UnityEngine;
[ExecuteInEditMode]
public class testScript : MonoBehaviour
{
    public Transform culler;

    void Update()
    {
        if (!culler)
            return;

        var renderer = GetComponent<Renderer>();
        if (!renderer)
            return;

        var material = renderer.sharedMaterial;
        material.SetMatrix("_CullerSpace", culler.worldToLocalMatrix);
    }
}


///Summary
/// Original Volume Rendering Shader
///

//Shader "VolumeRendering/VolumeRendering"
//{
//	Properties
//	{
//		_Color("Color", Color) = (1, 1, 1, 1)
//		_Volume("Volume", 3D) = "" {}
//		_Intensity("Intensity", Range(1.0, 5.0)) = 1.2
//		_Threshold("Threshold", Range(0.0, 1.0)) = 0.95
//		_SliceMin("Slice min", Vector) = (0.0, 0.0, 0.0, -1.0)
//		_SliceMax("Slice max", Vector) = (1.0, 1.0, 1.0, -1.0)
//	}

//		CGINCLUDE

//			ENDCG

//			SubShader{
//				Cull Back
//				Blend SrcAlpha OneMinusSrcAlpha
//	// ZTest Always

//	Pass
//{
//	CGPROGRAM
//		float4 _Plane;
//#define ITERATIONS 100
//# include "./VolumeRendering.cginc"
//#pragma vertex vert
//#pragma fragment frag

//	ENDCG
//}
//}
//}