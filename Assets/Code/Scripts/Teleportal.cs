using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportal : MonoBehaviour
{
    public GameObject _portal;
    public GameObject _wall;
    public GameObject _teleportview1;
    public GameObject _teleportview2;
    [SerializeField] private string newScene;

    //if the player collides with the portal, the player is teleported to a certain location in the scene. 
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("hi");
            StartCoroutine(Timertje());
        }
    }
    IEnumerator Timertje()
    {
            _teleportview1.SetActive(true);
            _wall.SetActive(false);
            yield return new WaitForSeconds(4);
            _teleportview2.SetActive(true);
            _teleportview1.SetActive(false);
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene("DreamWorld");
    }
}
