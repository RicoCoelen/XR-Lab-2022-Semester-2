using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject paint;
    public Material _portalpaint;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.instance.material.name == "Portal paint")
        {
            Debug.Log("Yes portal");
        }
    }

}
