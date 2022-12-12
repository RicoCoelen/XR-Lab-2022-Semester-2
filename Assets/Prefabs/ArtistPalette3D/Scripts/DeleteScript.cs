using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : BrushBaseScript, IBrush
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Line")
        {
            if (drawnLines.Remove(gameObject.GetComponent<Line>()))
                Destroy(collider.gameObject);
        }
    }
}
