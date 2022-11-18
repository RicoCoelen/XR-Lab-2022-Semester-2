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
    public SteamVR_Action_Boolean DrawTriggerAction; // reference to actionfile 
    public SteamVR_Input_Sources AllDevices = SteamVR_Input_Sources.Any;

    [Header("Painting Variables")]
    public List<Line> lines = new List<Line>();
    public bool grip = false;
    public bool trigger = false;
    public Hand LeftHand;
    public Hand RightHand;
    public Material currentMaterial;
    public bool runOnce = false;

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

        public Line(int order, Vector3 pos, Material mat)
        {
            gameObject = new GameObject();

            gameObject.transform.position = pos;
            points.Add(pos);

            lr = gameObject.AddComponent<LineRenderer>();
            lr.material = mat;
            lr.widthMultiplier = 0.1f;

            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            bc = gameObject.AddComponent<BoxCollider>();
            bc.isTrigger = true;
        }

        public void AddPoint(Vector3 pos)
        {
            points.Add(pos);
            lr.SetPositions(points.ToArray());
            Debug.Log(points.ToArray().ToString());
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

    /// <summary>
    /// enable input event handler on hand
    /// </summary>
    private void OnEnable()
    {
        // add event listeners
        GripAction.AddOnChangeListener(OnGripActionChange, AllDevices);
        DrawTriggerAction.AddOnChangeListener(OnDrawTriggerActionChange, AllDevices);
    }

    /// <summary>
    /// disable event handler 
    /// </summary>
    private void OnDisable()
    {
        if (GripAction != null)
        {
            GripAction.RemoveOnChangeListener(OnGripActionChange, AllDevices);
        }

        if (DrawTriggerAction != null)
        { 
            DrawTriggerAction.RemoveOnChangeListener(OnDrawTriggerActionChange, AllDevices);
        }

    }

    /// <summary>
    /// change value if Grip listener changes
    /// </summary>
    /// <param name="action"> </param>
    /// <param name="inputSource">  </param>
    /// <param name="newValue"> the new returned variable </param>
    private void OnGripActionChange(SteamVR_Action_Boolean action, SteamVR_Input_Sources inputSource, bool value)
    {
        this.grip = value;
    }

    /// <summary>
    /// change value if trigger listener changes
    /// </summary>
    /// <param name="action"> </param>
    /// <param name="inputSource">  </param>
    /// <param name="newValue"> the new returned variable </param>
    private void OnDrawTriggerActionChange(SteamVR_Action_Boolean action, SteamVR_Input_Sources inputSource, bool value)
    {
        this.trigger = value;
    }

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

    /// <summary>
    /// fixedupdate to update values at fixed rate
    /// </summary>
    public void FixedUpdate()
    {
        if (trigger)
        {
            if (LeftHand.currentAttachedObject == brushGO.gameObject)
            {
                if (!runOnce)
                {
                    lines.Add(new Line(lines.Count + 1, brushGO.transform.position, currentMaterial));
                    runOnce = true;
                }
                else
                {
                    lines[lines.Count - 1].AddPoint(brushGO.transform.position);
                }
            }

            if (RightHand.currentAttachedObject == brushGO.gameObject)
            {
                if (!runOnce)
                {
                    lines.Add(
                        new Line(
                            lines.Count + 1, brushGO.transform.position, currentMaterial
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

        if(grip)
        {
            if (LeftHand.currentAttachedObject == gameObject)
            {
                LeftHand.AttachObject(gameObject, GrabTypes.Grip);
            }

            if (RightHand.currentAttachedObject == gameObject)
            {
                RightHand.AttachObject(gameObject, GrabTypes.Grip);
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