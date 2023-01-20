using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : BrushBaseScript, IBrush
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (GetComponent<Collider>().gameObject.tag == "Line" && trigger > 0)
    //    {
    //        if (drawnLines.Remove(GetComponent<Collider>().GetComponent<Line>()))
    //            Destroy(GetComponent<Collider>().gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Line" && trigger > 0)
        {
            if(!parentScript.selectorGO.GetComponent<SelectorScript>().selectedObjects.Contains(collider.gameObject))
            {
                if (drawnLines.Remove(collider.GetComponent<Line>()))
                    Destroy(collider.gameObject);
            }
        }
    }
}
