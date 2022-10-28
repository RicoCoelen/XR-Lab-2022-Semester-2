using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintbucket : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Brush")
        {
            Debug.Log("paintbucket bitch");
        }
        
    }
    
}
