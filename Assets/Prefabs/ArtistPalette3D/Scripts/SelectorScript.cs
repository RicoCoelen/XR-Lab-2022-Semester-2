using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : BrushBaseScript, IBrush
{
    [SerializeField] private Material selectHighlight;
    [SerializeField] private List<GameObject> selectedObjects;
    [SerializeField] private int layers;

    /// <summary>
    /// Remove materials from selected objects
    /// </summary>
    public override void BrushFunctionality()
    {
        if(trigger > 0)
        {
            if (!runOnce)
            {
                RemoveMaterials();
                runOnce = true;
            }
        }
        else
        {
            runOnce = false;
        }
    }

    /// <summary>
    /// Remove materials from selected objects
    /// </summary>
    public void RemoveMaterials()
    {
        foreach (GameObject go in selectedObjects)
        {
            LineRenderer lr = go.GetComponent<LineRenderer>();
            Material[] mat = lr.sharedMaterials;

            for (int j = 0; j < mat.Length; j++)
            {
                if (mat[j].name == selectHighlight.name)
                {
                    lr.sharedMaterials = new Material[]
                    {
                        lr.sharedMaterials[0],
                    };
                }
            }
        }
        // clear selection
        selectedObjects.Clear();
    }

    /// <summary>
    /// add all the lines to a empty transform to merge them (maybe future linerenderer? or under 1 parent instead of layers)
    /// </summary>
    public void MergePaint()
    {   
        // bug delete already merged lines if not taken with selection
        if(selectedObjects.Count > 1)
        {
            // make new gameobject to put in empty parent transfrom
            GameObject layer = new GameObject();
            layer.name = "Merge Layer: " + layers;
            layer.tag = "Merge";

            bool r = false;
            // go through the hieracrchy of highlighted objects
            foreach (GameObject go in selectedObjects)
            {
                if (r!=true)
                {
                    layer.transform.position = go.transform.position;
                    r = true;
                }

                // assign merge parents
                go.GetComponent<Line>().mergeParent = layer.transform;

                // check if already has parents
                Transform temp = go.transform.parent;    
                if (temp != null)
                {
                    // if merge and empty delete
                    if (temp.tag.Equals("Merge") && temp.childCount < 1)
                        Destroy(temp.gameObject);
                }

                // assign the selected object parent to new merge parent
                go.transform.parent = layer.transform; 
            }

            RemoveMaterials();
            layers++;
        }
    }

    /// <summary>
    /// on trigger function to add lines (while trigger pull) and give them a new material
    /// </summary>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Line" && trigger > 0)
        {
            if (!selectedObjects.Contains(collider.gameObject))
            {
                LineRenderer temp = collider.GetComponent<LineRenderer>();

                temp.sharedMaterials = new Material[]
                {
                        temp.sharedMaterial,
                        new Material(selectHighlight)
                };

                selectedObjects.Add(collider.gameObject);
            }
        }
    }
}