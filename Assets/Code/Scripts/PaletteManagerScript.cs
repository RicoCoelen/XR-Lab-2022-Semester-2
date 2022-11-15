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
    public SteamVR_Action_Boolean DrawingAction; // reference to actionfile 
    public Hand hand; // the reference to the VR hand
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
        hand = GameObject.Find("RightHand").GetComponent<Hand>();
    }

    #endregion

    #region EventListener

    /// <summary>
    /// enable event handler, also on hand
    /// </summary>
    private void OnEnable()
    {
        if (hand == null)
            Debug.Log("Hands not found");

        if (DrawingAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No drawing action assigned", this);
            return;
        }

        DrawingAction.AddOnChangeListener(OnDrawingActionChange, hand.handType);
    }

    /// <summary>
    /// disable event handler 
    /// </summary>
    private void OnDisable()
    {
        if (DrawingAction != null)
            DrawingAction.RemoveOnChangeListener(OnDrawingActionChange, hand.handType);
    }

    #endregion

    #region Methods

    /// <summary>
    /// change value if event listener changes
    /// </summary>
    /// <param name="actionIn"> </param>
    /// <param name="inputSource">  </param>
    /// <param name="newValue"> the new returned variable </param>
    private void OnDrawingActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            Debug.Log(newValue);
            StartCoroutine(Paint());
        }
    }

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
