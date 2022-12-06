using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using TMPro;
using UnityEngine.UI;

public class PaletteManagerScript : MonoBehaviour
{
    #region Properties

    [Header("Painting Variables")]
    public Color currentColor;
    public Texture2D currentTexture;
    public Material currentMaterial;
    public int widthMultiplier = 1; // average line width setting
    
    [Header("Technical Variables")]
    public List<Line> lines = new List<Line>();
    public int interval = 5;
    public Transform lineHolder;

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
    public GameObject deleteIndicator;

    public GameObject editGO;
    public GameObject editIndicator;

    public GameObject brushGO;
    public GameObject brushIndicator;

    public GameObject sprayGO;
    public GameObject sprayIndicator;

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

        MeshRenderer mr;
        MeshFilter mf;
        LineRenderer lr;
        Rigidbody rb;
        MeshCollider mc;
        LineRendererSmoother lrs;
        Throwable t;
        Interactable i;

        public Line(int order, Vector3 pos, Material mat, float width, Transform parent)
        {
            // make the game object
            gameObject = new GameObject("Line: " + order);
            //gameObject.transform.parent = parent;
            gameObject.transform.position = pos;

            // add mesh renderer and filter for highlighting
            mr = gameObject.AddComponent<MeshRenderer>();
            mf = gameObject.AddComponent<MeshFilter>();

            // add linerenderer and vars
            lr = gameObject.AddComponent<LineRenderer>();
            lr.material = mat;
            lr.widthMultiplier = 0.05f * width;
            lr.generateLightingData = true;
            lr.useWorldSpace = false;

            // add first position to linerenderer
            points.Add(gameObject.transform.position - pos);
            lr.positionCount = points.Count;
            lr.SetPositions(points.ToArray());

            // create rigidbody for collisions
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            // mesh collider for grabbing
            mc = gameObject.AddComponent<MeshCollider>();

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

        brushIndicator = Instantiate(brushIndicator, spraySpot.transform);
        editIndicator = Instantiate(editIndicator, editSpot.transform);
        deleteIndicator = Instantiate(deleteIndicator, deleteSpot.transform);
        sprayIndicator = Instantiate(sprayIndicator, spraySpot.transform);


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
        // calculate brush paint area
        var brush = brushGO.transform.position;
        brush += -brushGO.transform.up * (widthMultiplier * 0.05f);

        // check if brush is held in lefthand
        if (LeftHand.currentAttachedObject == brushGO.gameObject)
        {
            if (!runOnce)
            {
                lines.Add(
                    new Line(
                        lines.Count + 1, brush, currentMaterial, widthMultiplier, lineHolder
                        )
                    );
                runOnce = true;
            }
            else
            {
                lines[lines.Count - 1].AddPoint(brush, widthMultiplier);
            }
        }

        // check if brush is held in lefthand
        if (RightHand.currentAttachedObject == brushGO.gameObject && trigger > 0)
        {
            if (!runOnce)
            {
                lines.Add(
                    new Line(
                        lines.Count + 1, brush, currentMaterial, widthMultiplier, lineHolder
                        )
                    );
                runOnce = true;
            }
            else
            {
                lines[lines.Count - 1].AddPoint(brush, widthMultiplier);
            }
        }
    }

    private void Update()
    {
        trigger = DrawTriggerAction.GetAxis(AllDevices);
        grip = GripAction.GetState(AllDevices);
    }

    public Vector3 TipPosition(GameObject brush, int multiplier)
    {
        // calculate brush paint area
        var pos = brush.transform.position;
        pos += -brush.transform.up * (multiplier * 0.05f);
        return pos;
    }

    public void ChangeIndicator(GameObject indicator, GameObject tipObject, int width, Color color)
    {
        indicator.transform.localScale = indicator.transform.localScale * width;
        indicator.transform.position = TipPosition(tipObject, width);
        var id = indicator.GetComponent<Renderer>().material;
        id.SetColor("_WireColor", color);
        id.SetColor("_BaseColor", color);
        indicator.GetComponent<Renderer>().material = id;
    }

    /// <summary>
    /// fixedupdate to update values at fixed rate
    /// </summary>
    public void FixedUpdate()
    {
        // indicator code
        ChangeIndicator(brushIndicator, brushGO, widthMultiplier, currentColor);
        ChangeIndicator(editIndicator, editGO, widthMultiplier, currentColor);
        ChangeIndicator(deleteIndicator, deleteGO, widthMultiplier, currentColor);
        ChangeIndicator(sprayIndicator, sprayGO, widthMultiplier, currentColor);

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
        if(widthMultiplier > 1)
        {
            widthMultiplier--;
        }
        lineSize.text = widthMultiplier.ToString();
    }

    /// <summary>
    /// return or rest tools to their original location
    /// </summary>
    /// <param name="objectTouch"> gives the reference to the game object that is touched </param>
    public Material ChangePaint(Material mat, Texture2D tex, Color col)
    {
        currentMaterial = mat;
        currentMaterial.SetTexture("_MainTex", tex);
        currentMaterial.SetColor("_Color", col);
        return currentMaterial;
    }

    /// <summary>
    /// return or rest tools to their original location
    /// </summary>
    /// <param name="objectTouch"> gives the reference to the game object that is touched </param>
    private void OnDrawGizmos()
    {
        var delete = deleteGO.transform.position;
        delete += -deleteGO.transform.up * (widthMultiplier * 0.05f);
        Gizmos.DrawWireSphere(delete, widthMultiplier * 0.05f);

        var edit = editGO.transform.position;
        edit += -editGO.transform.up * (widthMultiplier * 0.05f);
        Gizmos.DrawWireSphere(edit, widthMultiplier * 0.05f);

        var brush = brushGO.transform.position;
        brush += -brushGO.transform.up * (widthMultiplier * 0.05f);
        Gizmos.DrawWireSphere(brush, widthMultiplier * 0.05f);

        var spray = brushGO.transform.position;
        spray += -brushGO.transform.up * (widthMultiplier * 0.05f);
        Gizmos.DrawWireSphere(spray, widthMultiplier * 0.05f);
    }
    
    #endregion
}