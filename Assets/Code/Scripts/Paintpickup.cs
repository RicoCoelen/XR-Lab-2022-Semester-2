using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintpickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            gameObject.tag = "Pickedup";
        }
    }
}
