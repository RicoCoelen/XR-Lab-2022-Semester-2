using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : BrushBaseScript, IBrush
{
    List<GameObject> selectedObjects = new List<GameObject>();
    List<GameObject> currentObjects = new List<GameObject>(); 

    public override void BrushFunctionality()
    {
        if(trigger > 0)
        {
            if (!base.runOnce)
            {
                // clear selection
                selectedObjects.Clear();
                base.runOnce = true;
            }
            
            // add every object touched 
            foreach (GameObject go in currentObjects)
            {
                if (!selectedObjects.Contains(go))
                {
                    selectedObjects.Add(go);
                }
            }
        }
        else
        {
            base.runOnce = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!currentObjects.Contains(collision.gameObject))
        {
            currentObjects.Add(collision.gameObject);
        }
    }
}
