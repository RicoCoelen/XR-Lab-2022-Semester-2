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
        
        /*if (transform.position.y <= top.y && reachtop == false)
        {
            for (int i = 0; i <= bobbingRange; i++)
            {
                transform.position += new Vector3(0, speed, 0) * Time.deltaTime;

            }
            reachtop = true;
            reachbottem = false;
        }
        else if(transform.position.y >= bottem.y && reachbottem == false){
            for (int i = 0; i <= bobbingRange; i++)
            {
                transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;

            }
            reachtop = false;
            reachtop = true;
        }*/
        

        
    }
}
