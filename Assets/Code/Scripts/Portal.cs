using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Material _wallpaint1;
    public Renderer _wallie;

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
        yield return new WaitForSeconds(10);
        _wallie.material = _wallpaint1;
    }
}
