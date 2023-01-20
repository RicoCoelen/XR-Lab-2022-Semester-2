using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Paintbrush : MonoBehaviour
{
    public GameObject paint;
    public Material paintred;
    public Material paintgreen;
    public Material paintblue;

    void OnTriggerEnter(Collider collider)
    {
        var p = paint.GetComponent<Paint>();
        if (collider.gameObject.name == "Paintbucket red")
        {
            transform.GetComponent<Renderer>().material = paintred;
            p.GetComponent<Paint>()._colors = Enumerable.Repeat(paintred.color, p._brushSize * p._brushSize).ToArray();
        }

        if (collider.gameObject.name == "Paintbucket green")
        {
            transform.GetComponent<Renderer>().material = paintgreen;
            p.GetComponent<Paint>()._colors = Enumerable.Repeat(paintgreen.color, p._brushSize * p._brushSize).ToArray();
        }

        if (collider.gameObject.name == "Paintbucket blue")
        {
            transform.GetComponent<Renderer>().material = paintblue;
            p.GetComponent<Paint>()._colors = Enumerable.Repeat(paintblue.color, p._brushSize * p._brushSize).ToArray();
        }
    }
}
