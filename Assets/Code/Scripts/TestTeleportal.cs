using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTeleportal : MonoBehaviour
{
    public GameObject _player;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Portal")
        {
            //_player.transform.position = new Vector3(0, 20, 0);
            Debug.Log("hi");
        }
    }
}
