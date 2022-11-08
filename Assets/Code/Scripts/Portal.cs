using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Material _portalpaint;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Renderer>().sharedMaterial.name == _portalpaint.name + " (Instance)")
        {
            Debug.Log("Yes portal");
        }
    }

}
