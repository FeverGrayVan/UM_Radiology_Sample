using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColliders : MonoBehaviour
{
    public GameObject _ObjectsWithCollider;
    // Start is called before the first frame update
   

   public void TurnOffColliders()
    {

        _ObjectsWithCollider.GetComponent<BoxCollider>().enabled = false;
        
    }

    public void TurnOnColliders()
    {

        _ObjectsWithCollider.GetComponent<BoxCollider>().enabled = true;
        
    }
}
