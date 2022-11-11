using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public Material _portalpaint;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Renderer>().gameObject.tag == "Brush")
        {
            StartCoroutine(Timertje());
        }
    }
    IEnumerator Timertje()
    {
        Debug.Log("Begin");
        yield return new WaitForSeconds(5);
        Debug.Log("End");
    }
}
