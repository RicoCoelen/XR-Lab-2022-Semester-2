using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TestPortal : MonoBehaviour
{
    public GameObject _point1;
    public GameObject _point2;
    public GameObject _point3;
    public GameObject _point4;
    public GameObject _point5;
    public GameObject _point6;
    public GameObject _point7;
    public GameObject _point8;
    public GameObject[] points;

    public GameObject _portal;

    class Point
    {
        public int order;
        public Vector2 position;
        public Material material;
    }
    private void Start()
    {
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
        int goal = 0;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].gameObject.tag == "Portal Point")
            {
                goal++;
                if (goal == 8)
                {
                    StartCoroutine(Timertje());
                }
            }
        }
    }
    IEnumerator Timertje()
    {
        yield return new WaitForSeconds(3);
        _portal.SetActive(true);
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (_portal.activeSelf)
            {
                points[i].SetActive(false);
            }
        }
    }
}
