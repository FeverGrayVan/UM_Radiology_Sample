using UnityEngine;
[ExecuteInEditMode]
public class testScriptIppo : MonoBehaviour
{
    public Transform culler;
    public Vector4 section;

    void Update()
    {
        if (!culler)
            return;

        var renderer = GetComponent<Renderer>();
        if (!renderer)
            return;

        var material = renderer.sharedMaterial;

        float RotX = culler.localRotation.eulerAngles.x;

        float distance = Vector3.Distance(transform.position, culler.position);
        Debug.Log("Distance = " + distance);
        section = new Vector4(distance, 0, 0, 10);

        material.SetVector("_section", section);

        //material.SetMatrix("_CullerSpace", culler.worldToLocalMatrix);
    }
}