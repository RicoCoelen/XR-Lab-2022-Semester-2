using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using TMPro;

public class PaletteManagerScript : MonoBehaviour
{
    #region Properties

    [Header("Painting Variables")]
    public List<Line> lines = new List<Line>();
    public Material currentMaterial;
    public float widthMultiplier = 0.05f; // average line width setting
    public int interval = 5;

    [Header("Prefabs")]
    public GameObject deletePrefab;
    public GameObject editPrefab;
    public GameObject brushPrefab;
    public GameObject colorPrefab;
    public GameObject sprayPrefab;

    [Header("Spots")]
    public Transform deleteSpot;
    public Transform editSpot;
    public Transform brushSpot;
    public Transform spraySpot;

    [Header("GameObjects")]
    public GameObject deleteGO;
    public GameObject editGO;
    public GameObject brushGO;
    public GameObject sprayGO;
    public GameObject colorGO;
    public GameObject colorGradient;
    public TMP_Text lineSize;

    [Header("Buttons")]
    public GameObject brushUpButton;
    public GameObject brushDownButton;
    public GameObject resetToolsButton;

    [Header("Input Variables")]
    // public SteamVR_Action_Single TriggerAction; // reference to actionfile 
    public SteamVR_Action_Boolean GripAction; // reference to actionfile 
    public SteamVR_Action_Single DrawTriggerAction; // reference to actionfile 
    public SteamVR_Input_Sources AllDevices = SteamVR_Input_Sources.Any; 
    public Hand LeftHand;
    public Hand RightHand;
    public bool grip = false;
    public float trigger = 0f;
    public bool runOnce = false; // use runonce to check if trigger is held down

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

        public void AddPoint(Vector3 pos, float trigger)
        {
            points.Add(pos);
            lr.positionCount = points.Count;
            lr.SetPositions(points.ToArray());
            // lr.startWidth = trigger;
            // lr.endWidth = trigger;
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
        deleteGO = Instantiate(deletePrefab, deleteSpot.transform); 
        editGO = Instantiate(editPrefab, editSpot.transform); 
        brushGO = Instantiate(brushPrefab, brushSpot.transform); 
        sprayGO = Instantiate(sprayPrefab, spraySpot.transform); 
        
        colorGO = Instantiate(colorPrefab, spraySpot.transform);
        colorGO.GetComponent<ColorPicker>().clampObject = colorGradient;

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
    private void Paint(float trigger)
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
                lines[lines.Count - 1].AddPoint(brushGO.transform.position, trigger);
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
                lines[lines.Count - 1].AddPoint(brushGO.transform.position, trigger);
            }
        }
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
            // reduce point count and update by delay
            if (Time.frameCount % interval == 0)
            {
                Paint(trigger);
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

    /// <summary>
    /// return or rest tools to their original location
    /// </summary>
    /// <param name="objectTouch"> gives the reference to the game object that is touched </param>
    public void SizeUp()
    {
        widthMultiplier++;
        lineSize.text = widthMultiplier.ToString();
    }

    /// <summary>
    /// return or rest tools to their original location
    /// </summary>
    /// <param name="objectTouch"> gives the reference to the game object that is touched </param>
    public void SizeDown()
    {
        widthMultiplier--;
        lineSize.text = widthMultiplier.ToString();
    }

    public void projectDrawArea()
    {
        
    }

    #endregion
}