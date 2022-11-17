using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestPortalPoint : MonoBehaviour
{
    public Material _green;
    public Material _portal;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Brush")
        {
            GetComponent<Renderer>().material = _green;
        }
    }
}
