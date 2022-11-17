using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalPoint : MonoBehaviour
{
    public Material _green;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Brush")
        {
            GetComponent<Renderer>().material = _green;
            GetComponent<Renderer>().tag = "Portal Point";
        }
    }
}
