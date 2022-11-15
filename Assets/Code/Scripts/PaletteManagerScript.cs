using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PaletteManagerScript : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject brushPrefab;

    [Header("Spots")]
    public Transform brushSpot;

    [Header("GameObjects")]
    public GameObject brushGO;

    [Header("Painting Variables")]
    public Line[] lines;

    public class Line
    {
        public int order;
        public Vector3[] position;
        public Color color;
    }

    // Start is called before the first frame update
    void Awake()
    {
        brushGO = Instantiate(brushPrefab, brushSpot.transform);
    }

    public void FixedUpdate()
    {
        //if (SteamVR_Input._default.)
        //{
        //    brushGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}
    }


    public void ReturnTools(GameObject objectTouch)
    {
        brushGO.transform.parent = transform;
        brushGO.transform.position = brushSpot.transform.position;
        brushGO.transform.rotation = brushSpot.transform.rotation;
    }
}
