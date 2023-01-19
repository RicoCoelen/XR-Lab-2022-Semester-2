using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelectScript : MonoBehaviour
{
    PaletteManagerScript p;
    MaterialPickerScript m;

    private void OnTriggerEnter(Collider other)
    {
        p.currentMaterial = m.materials[m.go.IndexOf(transform.gameObject)];
    }
}
