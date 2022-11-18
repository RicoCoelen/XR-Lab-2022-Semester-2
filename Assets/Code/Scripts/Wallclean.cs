using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallclean : MonoBehaviour
{
    public Material _wallpaint1;
    public GameObject _portal;

    void Update()
    {
        if (_portal.activeSelf)
        {
            GetComponent<Renderer>().material = _wallpaint1;
        }
    }
}
