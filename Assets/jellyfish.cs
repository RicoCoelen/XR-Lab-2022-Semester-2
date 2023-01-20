using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellyfish : MonoBehaviour
{
    private float bobbingRange;
    private Vector3 top, bottem;
    private bool reachtop, reachbottem;
    [SerializeField] private float min, max, speed;

    private void Start()
    {
        bobbingRange = Random.Range(min, max);
        top = new Vector3(transform.position.x, transform.position.y + bobbingRange, transform.position.z);
        bottem = new Vector3(transform.position.x, transform.position.y - bobbingRange, transform.position.z);
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(bottem, top, Mathf.PingPong(Time.time*speed, 1.0f));
    }
}
