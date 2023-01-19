using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialPickerScript : MonoBehaviour
{
    // picker variables
    public Texture2D[] thumbnails;
    public Material[] materials;
    public List<GameObject> go;
    private int currentMaterial = 0;
    public TextMeshProUGUI indexText;

    // custom materials
    [SerializeField]
    public Material[] customMaterials;

    // ui
    public GameObject viewport;
    public GameObject content;

    // manager
    PaletteManagerScript manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = transform.parent.parent.parent.GetComponent<PaletteManagerScript>();

        // get sub men hierarchy
        viewport = transform.GetChild(0).gameObject;
        content = viewport.transform.GetChild(0).gameObject;

        // initiate arrays
        materials = Resources.LoadAll<Material>("Materials");
        thumbnails = new Texture2D[materials.Count()];
        
        int index = 0;
        foreach (Material m in materials)
        {
            thumbnails[index] = RuntimePreviewGenerator.GenerateMaterialPreview(m, PrimitiveType.Sphere);

            GameObject imgObject = new GameObject("Thumbnail: " + m.name);
            imgObject.transform.parent = content.transform;

            Image img = imgObject.AddComponent<Image>();
            imgObject.transform.rotation = Quaternion.RotateTowards(imgObject.transform.rotation, viewport.transform.rotation, 90);
            imgObject.transform.localScale = new Vector3(1, 1, 1);

            img.sprite = Sprite.Create(thumbnails[index], new Rect(0,0, thumbnails[index].width, thumbnails[index].height), new Vector2(1f, 1f));
            
            Rigidbody rb = imgObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            BoxCollider col = imgObject.AddComponent<BoxCollider>();
            col.isTrigger = true;

            go.Add(imgObject);
            index++;
        }
    }

    void FixedUpdate()
    {
        if (manager.currentMaterial)
        {
            if (materials[currentMaterial].name != manager.currentMaterial.name)
            {
                manager.currentMaterial = new Material(materials[currentMaterial]);
                indexText.text = currentMaterial.ToString();
            }
        }
        else
        {
            manager.currentMaterial = new Material(materials[currentMaterial]);
            indexText.text = currentMaterial.ToString();
            Debug.Log(indexText);
            Debug.Log(currentMaterial.ToString());
            Debug.Log(indexText.text);

        }
    }

    public void SelectNext()
    {
        Debug.Log("Next");
        if (materials[currentMaterial] != null && currentMaterial + 1 < materials.Length)
        {
            currentMaterial++;
            manager.currentMaterial = new Material(materials[currentMaterial]);
           
        }
    }

    public void SelectPrevious()
    {
        if (materials[currentMaterial] != null && currentMaterial - 1 >= 0)
        {
            currentMaterial--;
            manager.currentMaterial = new Material(materials[currentMaterial]);
        }
    }
}
