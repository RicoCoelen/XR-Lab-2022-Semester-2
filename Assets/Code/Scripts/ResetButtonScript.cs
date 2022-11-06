using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        // Forward to the parent (or just deal with it here).
        // Let's say it has a script called "returntools" on it:
        PaletteScript parentScript = transform.parent.GetComponent<PaletteScript>();

        // Let it know a collision happened:
        parentScript.ReturnTools(col.gameObject);

    }
}
