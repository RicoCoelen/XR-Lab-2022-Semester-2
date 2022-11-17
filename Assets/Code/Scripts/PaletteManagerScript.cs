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

    public Hand RightHand; // the reference to the right VR hand
    public Hand LeftHand; // the reference to the left VR hand

    public GameObject prefabToPlant; // plant to spawn

    [Header("Painting Variables")]
    public Line[] lines;

    #endregion

    #region Classes

    /// <summary>
    ///   Class to hold the line variables
    /// </summary>
    public class Line
    {
        public int order;
        public Vector3[] position;
        public Color color;
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
    /// enable event handler, also on hand
    /// </summary>
    private void OnEnable()
    {
        if (LeftHand == null || RightHand == null) // check if hands exists
        {
            Debug.Log("One or more Hands not found!");
            return;
        }

        if (GripAction == null || DrawTriggerAction == null) // check if action map exists
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No action assigned", this); 
            return;
        }
        
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
        if (value)
        {
            Debug.Log(value);
        }
    }

    /// <summary>
    /// change value if trigger listener changes
    /// </summary>
    /// <param name="action"> </param>
    /// <param name="inputSource">  </param>
    /// <param name="newValue"> the new returned variable </param>
    private void OnDrawTriggerActionChange(SteamVR_Action_Boolean action, SteamVR_Input_Sources inputSource, bool value)
    {
        if (value)
        {
            Debug.Log(value);
            StartCoroutine(Paint());
        }
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

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    /// <summary>
    /// fixedupdate to update values at fixed rate
    /// </summary>
    public void FixedUpdate()
    {
        if (true)
        {
            //StartCoroutine(Paint());
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
