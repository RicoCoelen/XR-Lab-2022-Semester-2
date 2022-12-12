using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : BrushBaseScript, IBrush
{
    [SerializeField] private Material selectHighlight;

    public List<GameObject> selectedObjects = new List<GameObject>();
    public List<GameObject> currentObjects = new List<GameObject>(); 

    public override void BrushFunctionality()
    {
        if(trigger > 0)
        {
            if (!runOnce)
            {
                for(int i=0;i<currentObjects.Count;i++)
                {
                    var temp = 0;
                    var index = 0;

                    foreach (Material mat in currentObjects[i].GetComponent<LineRenderer>().sharedMaterials)
                    {
                        if (mat == selectHighlight)
                        {
                            index = temp;

                            Debug.Log(index);
                        }
                        else
                        {
                            temp++;
                        }
                    }

                    if (index > 0)
                    {
                        currentObjects[i].GetComponent<LineRenderer>().sharedMaterials = new Material[]
                        {
                            currentObjects[i].GetComponent<LineRenderer>().sharedMaterials[0],
                        };
                    }

                    // clear selection
                    currentObjects.RemoveAt(i);
                }
                runOnce = true;
            }
            
            // add every object touched and trigger held
            foreach (GameObject go in currentObjects)
            {
                if (!selectedObjects.Contains(go))
                {
                    LineRenderer temp = go.GetComponent<LineRenderer>();
                    temp.sharedMaterials = new Material[] 
                    {
                        temp.sharedMaterial,
                        new Material(selectHighlight)
                    };
                    selectedObjects.Add(go);
                }
            }
        }
        else
        {
            runOnce = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Line" && trigger > 0)
        {
            if (!currentObjects.Contains(collider.gameObject))
            {
                currentObjects.Add(collider.gameObject);
            }
        }
    }
}
