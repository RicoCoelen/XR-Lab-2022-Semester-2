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

    [Header("Prefabs")]
    public GameObject deletePrefab;
    public GameObject selectorPrefab;
    public GameObject brushPrefab;
    public GameObject sprayPrefab;

    [Header("Spots")]
    public Transform deleteSpot;
    public Transform selectorSpot;
    public Transform brushSpot;
    public Transform spraySpot;

    [Header("GameObjects")]
    public GameObject deleteGO;
    public GameObject selectorGO;
    public GameObject brushGO;
    public GameObject sprayGO;
    public GameObject colorGO;
    public GameObject materialGO;

    [Header("UI")]
    public TMP_Text lineSize;

    #endregion
   
    #region Constructor

    /// <summary>
    /// awake function to ready up for game start
    /// </summary>
    void Awake()
    {
        // spawn brush on palette
        deleteGO = Instantiate(deletePrefab, deleteSpot.transform); 
        selectorGO = Instantiate(selectorPrefab, selectorSpot.transform); 
        brushGO = Instantiate(brushPrefab, brushSpot.transform); 
        //sprayGO = Instantiate(sprayPrefab, spraySpot.transform);

        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadMaterials()
    {
        //DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Vehicles");
        //FileInfo[] info = dir.GetFiles("*.prefab");
        //foreach (FileInfo f in info)
        //{
        //    GUI.Label(new Rect(300, 500, 50, 20), f.FullName);
        //}
    }

    #endregion

    #region Methods


    /// <summary>
    /// fixedupdate to update values at fixed rate
    /// </summary>
    public void FixedUpdate()
    {
        currentColor = colorGO.GetComponent<ColorPicker>().stolenColor;
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

        selectorGO.transform.parent = transform;
        selectorGO.transform.position = selectorSpot.transform.position;
        selectorGO.transform.rotation = selectorSpot.transform.rotation;

        deleteGO.transform.parent = transform;
        deleteGO.transform.position = deleteSpot.transform.position;
        deleteGO.transform.rotation = deleteSpot.transform.rotation;

        //sprayGO.transform.parent = transform;
        //sprayGO.transform.position = spraySpot.transform.position;
        //sprayGO.transform.rotation = spraySpot.transform.rotation;
    }

    /// <summary>
    /// function for sizeup brushsize button to increase width multiplier
    /// </summary>
    public void SizeUp()
    {
        widthMultiplier++;
        lineSize.text = widthMultiplier.ToString();
    }

    /// <summary>
    /// function for sizedown brushsize button to decrease width multiplier
    /// </summary>
    public void SizeDown()
    {
        if(widthMultiplier > 1)
        {
            widthMultiplier--;
        }
        lineSize.text = widthMultiplier.ToString();
    }

    public void MergePaint()
    {
        selectorGO.GetComponent<SelectorScript>().MergePaint();
    }

    public void ApplyPaint()
    {
        currentColor = colorGO.GetComponent<ColorPicker>().stolenColor;
        currentMaterial.SetColor("color", currentColor);
        selectorGO.GetComponent<SelectorScript>().ApplyMaterials(currentMaterial, currentColor);
    }

    public void SaveSelectedLines(string json, GameObject go, string folder)
    {
        var tempJson = JsonUtility.ToJson(go);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + folder + "/LineData.json", tempJson);
    }

    public void SaveAllLines(string json, GameObject go, string folder)
    {
        var tempJson = JsonUtility.ToJson(go);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + folder + "/LineData.json", tempJson);
    }

    public void LoadLine(string folder, int id, string name)
    {
        Debug.Log(System.IO.File.Exists(folder));
    }

    public void LoadAllLines(string folder, int id, string name)
    {
        Debug.Log(System.IO.File.Exists(folder));
    }

    #endregion
}