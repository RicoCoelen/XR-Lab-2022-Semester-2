using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportal : MonoBehaviour
{
    public GameObject _player;
    public GameObject _teleportview1;
    public GameObject _teleportview2;

    //if the player collides with the portal, the player is teleported to a certain location in the scene. 
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(Timertje());
        }
    }
    IEnumerator Timertje()
    {
        _teleportview1.SetActive(true);
        yield return new WaitForSeconds(2);
        _player.transform.position = new Vector3(30, 1, 0);
        yield return new WaitForSeconds(2);
        _teleportview2.SetActive(true);
        _teleportview1.SetActive(false);
        yield return new WaitForSeconds(4);
        _teleportview2.SetActive(false);
    }
}
