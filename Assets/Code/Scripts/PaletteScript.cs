using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteScript : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject brushPrefab;
    public GameObject sprayPrefab;
    public GameObject eraserPrefab;

    [Header("Spots")]
    public Transform brushSpot;
    public Transform spraySpot;
    public Transform eraserSpot;

    [Header("GameObjects")]
    public GameObject brushGO;
    public GameObject sprayGo;
    public GameObject eraserGo;

    // Start is called before the first frame update
    void Start()
    {
        brushGO = Instantiate(brushPrefab, brushSpot.transform);
        sprayGo = Instantiate(sprayPrefab, spraySpot.transform);
        eraserGo = Instantiate(eraserPrefab, eraserSpot.transform);
    }
    
    public void ReturnTools(GameObject objectTouch)
    {
        brushGO.transform.parent = transform;
        brushGO.transform.position = brushSpot.transform.position;
        brushGO.transform.rotation = brushSpot.transform.rotation;

        sprayGo.transform.parent = transform;
        sprayGo.transform.position = spraySpot.transform.position;
        sprayGo.transform.rotation = spraySpot.transform.rotation;

        eraserGo.transform.parent = transform;
        eraserGo.transform.position = eraserSpot.transform.position;
        eraserGo.transform.rotation = eraserSpot.transform.rotation;
    }
}
