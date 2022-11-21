using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class PaletteManagerScript : MonoBehaviour
{
    #region Properties

    [Header("Prefabs")]
    public GameObject brushPrefab;

    [Header("Spots")]
    public Transform brushSpot;

    [Header("GameObjects")]
    public GameObject brushGO;

    [Header("Input Action Variables")]
    // public SteamVR_Action_Single TriggerAction; // reference to actionfile 
    public SteamVR_Action_Boolean GripAction; // reference to actionfile 
    public SteamVR_Action_Single DrawTriggerAction; // reference to actionfile 
    public SteamVR_Input_Sources AllDevices = SteamVR_Input_Sources.Any;

    [Header("Painting Variables")]
    public List<Line> lines = new List<Line>();
    public bool grip = false;
    public float trigger = 0f;
    public Hand LeftHand;
    public Hand RightHand;
    public Material currentMaterial;
    public bool runOnce = false;
    public float widthMultiplier = 0.05f;

    #endregion

    #region Classes

    /// <summary>
    ///   Class to hold the line variables
    /// </summary>
    public class Line
    {
        public int order;
        public List<Vector3> points = new List<Vector3>();
        public GameObject gameObject;

        LineRenderer lr;
        Rigidbody rb;
        BoxCollider bc;
        Throwable t;

        public Line(int order, Vector3 pos, Material mat, float width)
        {

            Debug.Log(pos);
            gameObject = new GameObject();
            gameObject.transform.position = Vector3.zero;

            lr = gameObject.AddComponent<LineRenderer>();
            lr.material = mat;
            lr.widthMultiplier = width;
            lr.generateLightingData = true;
            lr.useWorldSpace = false;

            points.Add(pos);
            lr.SetPosition(0, pos);

            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            bc = gameObject.AddComponent<BoxCollider>();
            t = gameObject.AddComponent<Throwable>();
        }

        public void AddPoint(Vector3 pos)
        {
            points.Add(pos);
            lr.positionCount = points.Count;
            lr.SetPositions(points.ToArray());
        }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// awake function to ready up for game start
    /// </summary>
    void Awake()
    {
        // spawn brush on palette
        brushGO = Instantiate(brushPrefab, brushSpot.transform);

        // get right component hand at start
        LeftHand = GameObject.Find("LeftHand").GetComponent<Hand>();
        RightHand = GameObject.Find("RightHand").GetComponent<Hand>();
    }

    #endregion

    #region EventListener

    // TODO:

    #endregion

    #region Methods

    /// <summary>
    /// change value if event listener changes
    /// </summary>
    IEnumerator Paint()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);


        yield return new WaitForSeconds(1f);

        ////After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void Update()
    {
        trigger = DrawTriggerAction.GetAxis(AllDevices);
        grip = GripAction.GetState(AllDevices);
    }

    /// <summary>
    /// fixedupdate to update values at fixed rate
    /// </summary>
    public void FixedUpdate()
    {
        // check left hand
        if (trigger > 0)
        {
            if (LeftHand.currentAttachedObject == brushGO.gameObject)
            {
                if (!runOnce)
                {
                    lines.Add(
                        new Line(
                            lines.Count + 1, brushGO.transform.position, currentMaterial, widthMultiplier
                            )
                        );
                    runOnce = true;
                }
                else
                {
                    lines[lines.Count - 1].AddPoint(brushGO.transform.position);
                }
            }

            // check right hand
            if (RightHand.currentAttachedObject == brushGO.gameObject && trigger > 0)
            {
                if (!runOnce)
                {
                    lines.Add(
                        new Line(
                            lines.Count + 1, brushGO.transform.position, currentMaterial, widthMultiplier
                            )
                        );
                    runOnce = true;
                }
                else
                {
                    lines[lines.Count - 1].AddPoint(brushGO.transform.position);
                }
            }
        }
        else
        {
            runOnce = false;
        }

        // check if grip is pressed 
        if (RightHand.currentAttachedObject == gameObject && grip)
        {
            if (LeftHand.currentAttachedObject == gameObject)
            {
                LeftHand.AttachObject(gameObject, GrabTypes.Grip);
            }

            if (RightHand.currentAttachedObject == gameObject)
            {
                RightHand.AttachObject(gameObject, GrabTypes.Grip);
                Debug.Log("connected");
            }
        }
    }

    /// <summary>
    /// return or rest tools to their original location
    /// </summary>
    /// <param name="objectTouch"> gives the reference to the game object that is touched </param>
    public void ReturnTools(GameObject objectTouch)
    {
        brushGO.transform.parent = transform;
        brushGO.transform.position = brushSpot.transform.position;
        brushGO.transform.rotation = brushSpot.transform.rotation;
    }

    #endregion
}