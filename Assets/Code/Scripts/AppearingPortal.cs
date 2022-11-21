using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AppearingPortal : MonoBehaviour
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
        //the "goal" number at the beginning is 0. The script runs if the gameObject "portal" is disabled. 
        int goal = 0;
        if (!_portal.activeSelf)
        {
            for (int i = 0; i < points.Length; i++)
            {
                //the script runs if the "points" receive the tag when touched by the brush.  
                if (points[i].gameObject.tag == "Portal Point")
                {
                    //the timer starts if the goal of touching all 8 points has been reached. 
                    goal++;
                    if (goal == 8)
                    {
                        StartCoroutine(Timertje());
                    }
                }
            }
        }
    }
    IEnumerator Timertje()
    {
        //after 2 seconds the portal will be enabled. 
        yield return new WaitForSeconds(2);
        _portal.SetActive(true);

    }
}
