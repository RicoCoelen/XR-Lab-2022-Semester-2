using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
///   Class to hold the line variables
/// </summary>
/// 

[Serializable]
public class Line : MonoBehaviour
{
    public int order;
    public bool isSelected = false;
    public Vector3 objectRoot;
    public List<Vector3> points = new List<Vector3>();
    public PaletteManagerScript parentScript;
    public Transform mergeParent;
    public Vector3 mergeOffset;
    public Material mat;

    MeshRenderer mr;
    MeshFilter mf;
    LineRenderer lr;
    Rigidbody rb;
    MeshCollider mc;
    LineRendererSmoother lrs;
    Throwable t;
    Interactable i;
    
    public void Awake()
    {
        if (parentScript == null)
            parentScript = GameObject.Find("Artist Palette 3D").GetComponent<PaletteManagerScript>();

        // make the game object
        this.name = "Line: " + order;
        this.tag = "Line";

        mat = parentScript.currentMaterial;
        
        //gameObject.transform.parent = parent;
        //gameObject.transform.position ;

        // add mesh renderer and filter for highlighting
        // mr = gameObject.AddComponent<MeshRenderer>();
        mf = GetComponent<MeshFilter>();

        // add linerenderer and vars
        lr = GetComponent<LineRenderer>();
        //lr.widthMultiplier = 0.05f * parentScript.widthMultiplier; //// brooo
        lr.generateLightingData = true;
        lr.useWorldSpace = false;

        // add first position to linerenderer
        points.Add(objectRoot);
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());

        // create rigidbody for collisions
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // mesh collider for grabbing
        mc = gameObject.GetComponent<MeshCollider>();
        mc.convex = true;
        mc.isTrigger = true;

        // linerenderer smoother, for bezier curves and meshcollider generation
        lrs = GetComponent<LineRendererSmoother>();
        lrs.Line = lr;

        // give meshcollider mesh to meshfilter 
        mf.mesh = lrs.GetMesh(mc);

        // add steamVR physics
        t = gameObject.GetComponent<Throwable>();

        // make it interactable and glow on hover
        i = GetComponent<Interactable>();
        i.highlightOnHover = true;
    }

    public void OnGrab()
    {
        if (mergeParent)
        {
             mergeOffset = mergeParent.position - transform.position;
        }
    }

    public void OnHold()
    {
        if (mergeParent)
        { 
            mergeParent.position = transform.position + mergeOffset;
            mergeParent.transform.RotateAround(mergeParent.transform.position, transform.rotation.eulerAngles, Time.deltaTime);
        }
    }

    public void OnDetach()
    {

    }

    //public void UpdateLinePositions()
    //{
    //    if (mergeParent)
    //    {
    //        transform.parent.position = mergeParent.position - transform.position;
    //    }
    //}

    public void AddPoint(Vector3 pos, float width)
    {
        // add point to array
        points.Add(pos - transform.position);

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