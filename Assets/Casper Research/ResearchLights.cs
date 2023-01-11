using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchLights : MonoBehaviour
{
    private bool Active1;
    private bool Active2;
    private bool Active3;

    [SerializeField] private GameObject light1, light2, light3;


    [SerializeField] private float currentTime = 0f;
    private void Start()
    {
        light2.SetActive(false);
        light3.SetActive(false);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Spot Light 1")
        {
            Active1 = true;
            light2.SetActive(true);
        }
        else if (other.name == "Spot Light 2")
        {
            Active2 = true;
            light2.SetActive(true);
        }
        else if (other.name == "Spot Light 3")
        {
            Active3 = true;
            Debug.Log(currentTime);
        }
    }

    private void Update()
    {
        currentTime += 1 * Time.deltaTime;
    }
}
