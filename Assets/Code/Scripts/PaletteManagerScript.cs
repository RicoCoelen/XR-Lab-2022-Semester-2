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
        brushGO = Instantiate(brushPrefab, brushSpot.transform);
    }

    #endregion

    #region Methods

    /// <summary>
    /// enable event handler, also on hand
    /// </summary>
    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (DrawingAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
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
           // Plant();
        }
    }

    /// <summary>
    /// fixedupdate to update values at fixed rate
    /// </summary>
    public void FixedUpdate()
    {
        if (DrawingAction.active)
        {
            brushGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
