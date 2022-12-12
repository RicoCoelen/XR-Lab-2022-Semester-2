using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : BrushBaseScript, IBrush
{
    public GameObject linePrefab;
    public int countLines = 1;
    public PaletteManagerScript parents;

    public override void Awake()
    {
        base.Awake();
    }

    public override void BrushFunctionality()
    {
        if (!base.runOnce)
        {
            
            var temp = Instantiate(linePrefab, base.TipPosition(transform.gameObject, width), Quaternion.identity);
            temp.name = "Line: " + countLines;
            var temp2 = temp.GetComponent<Line>();
            temp2.order = countLines;
            drawnLines.Add(temp2);
            countLines++;

            base.runOnce = true;
        }
        else
        {
            drawnLines[drawnLines.Count - 1].AddPoint(base.TipPosition(transform.gameObject, width), width);
        }
    }
}
