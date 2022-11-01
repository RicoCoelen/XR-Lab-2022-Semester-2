using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintbrush : MonoBehaviour
{
    public Material paintred;
    public Material paintgreen;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Paintbucket red")
        {
            transform.GetComponent<Renderer>().material = paintred;
        }

        if (collider.gameObject.name == "Paintbucket green")
        {
            transform.GetComponent<Renderer>().material = paintgreen;
        }
    }
}
