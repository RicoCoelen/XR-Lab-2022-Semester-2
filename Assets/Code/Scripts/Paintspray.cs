using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Paintspray : MonoBehaviour
{
    public GameObject paint;
    public Material paintred;
    public Material paintgreen;
    public Material paintblue;

    void OnTriggerEnter(Collider collider)
    {
        var p = paint.transform.parent.GetComponent<SprayScript>();
        if (collider.gameObject.name == "Paintbucket red")
        {

            transform.GetComponent<Renderer>().material = paintred;
            p._colors = paintred.color;
        }

        if (collider.gameObject.name == "Paintbucket green")
        {
            transform.GetComponent<Renderer>().material = paintgreen;
            p._colors = paintgreen.color;
        }

        if (collider.gameObject.name == "Paintbucket blue")
        {
            transform.GetComponent<Renderer>().material = paintblue;
            p._colors = paintblue.color;
        }
    }
}
