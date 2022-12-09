using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{
    List<GameObject> selectedObjects;
    List<GameObject> currentObjects;

    public void SelectObjects(float trigger, bool runonce)
    {
        if(trigger > 0)
        {
            if (!runonce)
            {
                // clear selection
                selectedObjects.Clear();
                runonce = true;
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
            runonce = false;
        }
    }

    private void LateUpdate()
    {
        currentObjects.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!currentObjects.Contains(collision.gameObject))
        {
            currentObjects.Add(collision.gameObject);
        }
    }
}
