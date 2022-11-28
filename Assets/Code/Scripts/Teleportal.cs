using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class Teleportal : MonoBehaviour
{
    public GameObject _player;
    public GameObject _teleportview1;
    public GameObject _teleportview2;
    public string _dreamscene;

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
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_dreamscene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(_player, SceneManager.GetSceneByName(_dreamscene));
        SceneManager.UnloadSceneAsync(currentScene);
        yield return new WaitForSeconds(2);
        _teleportview2.SetActive(true);
        _teleportview1.SetActive(false);
        yield return new WaitForSeconds(4);
        _teleportview2.SetActive(false);
    }
}
