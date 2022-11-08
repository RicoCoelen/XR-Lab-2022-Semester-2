using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Material _portalpaint;


    private bool enter = false;
    void OnTriggerEnter(Collider collider)
    {
        if (enter == false) StartCoroutine(your_timer());
    }
    IEnumerator your_timer()
    {
        enter = true;
        if (GetComponent<Collider>().GetComponent<Renderer>().sharedMaterial.name == _portalpaint.name + " (Instance)")
        {
            GetComponent<Wall>().enabled = false;
            yield return new WaitForSeconds(0.01f);
        }
        enter = false;
    }
}
