using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject brush;
    public Material _portalpaint;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Renderer>().material == _portalpaint)
        {
            Debug.Log("Yes portal");
        }
    }

}
