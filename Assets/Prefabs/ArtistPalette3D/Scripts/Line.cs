using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
///   Class to hold the line variables
/// </summary>
public class Line : MonoBehaviour
{
    public int order;
    public List<Vector3> points = new List<Vector3>();

    MeshRenderer mr;
    MeshFilter mf;
    LineRenderer lr;
    Rigidbody rb;
    MeshCollider mc;
    LineRendererSmoother lrs;
    Throwable t;
    Interactable i;

    PaletteManagerScript parentScript;

    public void Awake()
    {
        if (parentScript == null)
            parentScript = GameObject.Find("Artist Palette 3D").GetComponent<PaletteManagerScript>();

        // make the game object
        this.name = "Line: " + order;
        //gameObject.transform.parent = parent;
       // gameObject.transform.position ;
        this.tag = "Line";
        transform.parent = GameObject.Find("Lines").transform;

        // add mesh renderer and filter for highlighting
        // mr = gameObject.AddComponent<MeshRenderer>();
        mf = gameObject.AddComponent<MeshFilter>();

        // add linerenderer and vars
        lr = gameObject.AddComponent<LineRenderer>();
        //lr.widthMultiplier = 0.05f * parentScript.widthMultiplier; //// brooo
        lr.generateLightingData = true;
        lr.useWorldSpace = false;

        // add first position to linerenderer
        points.Add(transform.position);
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());

        // create rigidbody for collisions
        rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        // mesh collider for grabbing
        mc = gameObject.AddComponent<MeshCollider>();
        mc.convex = true;
        mc.isTrigger = true;

        // linerenderer smoother, for bezier curves and meshcollider generation
        lrs = gameObject.AddComponent<LineRendererSmoother>();
        lrs.Line = lr;

        // give meshcollider mesh to meshfilter 
        mf.mesh = lrs.GetMesh(mc);

        // add steamVR physics
        t = gameObject.AddComponent<Throwable>();

        // make it interactable and glow on hover
        i = gameObject.GetComponent<Interactable>();
        i.highlightOnHover = true;
    }


    public void AddPoint(Vector3 pos, float width)
    {
        // add point to array
        points.Add(pos - gameObject.transform.position);

        // set width
        lr.startWidth = 0.05f * width;
        lr.endWidth = 0.05f * width;

        // define size and give array to line renderer
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());

        // generate the collider using bezier curves
        lrs.GenerateMeshCollider();
    }
}
