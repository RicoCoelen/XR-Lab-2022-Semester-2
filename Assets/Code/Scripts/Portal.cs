using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Material _wallpaint1;
    public GameObject _portal;

    void Start()
    {
        if (_portal.activeSelf)
        {
            GetComponent<Renderer>().material = _wallpaint1;
        }
    }
}
