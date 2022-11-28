using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallclean : MonoBehaviour
{
    public Material _wallpaint1;
    public GameObject _portal;

    public GameObject _point1;
    public GameObject _point2;
    public GameObject _point3;
    public GameObject _point4;
    public GameObject _point5;
    public GameObject _point6;
    public GameObject _point7;
    public GameObject _point8;
    public GameObject[] points;

    private void Start()
    {
        //the constructor so that I can select all points as "points[i]".
        points = new GameObject[8];
        points[0] = _point1;
        points[1] = _point2;
        points[2] = _point3;
        points[3] = _point4;
        points[4] = _point5;
        points[5] = _point6;
        points[6] = _point7;
        points[7] = _point8;
    }
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            //if the portal is enabled, all points will be disabled and the wall will look clean. 
            if (_portal.activeSelf)
            {
                GetComponent<Renderer>().material = _wallpaint1;
                points[i].SetActive(false);
            }
        }
    }
}
