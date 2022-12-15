using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintpickup : MonoBehaviour
{
    //when either spraypaint or paintbrush has been picked up it gets the tag indicating it has been picked up
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            gameObject.tag = "Pickedup";
        }
    }
}
