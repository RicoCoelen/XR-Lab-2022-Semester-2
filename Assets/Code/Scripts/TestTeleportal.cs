using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTeleportal : MonoBehaviour
{
    public GameObject _player;
    public GameObject _teleportview;

    //if the player collides with the portal, the player is teleported to a certain location in the scene. 
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _teleportview.SetActive(true);
            _player.transform.position = new Vector3(30, 15, 0);
            Debug.Log("hi");
        }
    }
}
