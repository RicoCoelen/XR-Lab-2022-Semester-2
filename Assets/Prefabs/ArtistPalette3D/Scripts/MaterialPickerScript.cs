using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MaterialPickerScript : MonoBehaviour
{
    // picker variables
    public Texture2D[] thumbnails;
    public Material[] materials;
    private int currentMaterial = 0;

    // ui
    public GameObject viewport;
    public GameObject content;

    // manager
    PaletteManagerScript manager;

    // Start is called before the first frame update
    void Start()
    {
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
            img.sprite = Sprite.Create(thumbnails[index], new Rect(0,0, thumbnails[index].width, thumbnails[index].height), new Vector2(1f, 1f));

            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // add highlight to selected image
    }

    void CheckIfNull()
    {
        //if (UnityEditor.AssetPreview.IsLoadingAssetPreview(instance))
        //{

        //    Debug.Log("still fetching");
        //}
        //else
        //{
        //    var getAssetPreview = UnityEditor.AssetPreview.GetAssetPreview(texture);
        //    gameObjTex = getAssetPreview;
        //}

    }

    public void SelectNext()
    {
        manager.currentMaterial = materials[currentMaterial =+ 1];

    }

    public void SelectPrevious()
    {
        manager.currentMaterial = materials[currentMaterial =- 1];
    }
}
