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

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void BrushFunctionality()
    {
        if (!base.runOnce)
        {
            
            var temp = Instantiate(linePrefab, TipPosition(gameObject, width), Quaternion.identity);
            temp.name = "Line: " + countLines;
            
            var temp2 = temp.GetComponent<Line>();
            temp2.order = countLines;
            temp2.objectRoot = TipPosition(transform.gameObject, width);

            parentScript.lines.Add(temp2);
            countLines++;

            base.runOnce = true;
        }
        else
        {
            parentScript.lines[parentScript.lines.Count - 1].AddPoint(base.TipPosition(transform.gameObject, width), width);
        }
    }
}
