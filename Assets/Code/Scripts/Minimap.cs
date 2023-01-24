using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;


    public Transform vrcamera;

    //If the this object is enabled follow the players position and y rotation.
    public void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;


        Vector3 relativePos = vrcamera.position - transform.position;

        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //Quaternion temp = Quaternion.identity;
        
        
        /*temp.Set(vrcamera.rotation.x, Quaternion.identity.y, vrcamera.rotation.z, 1);

        transform.rotation = temp;*/
        //temp.x = 90;
        //float temp = vrcamera.rotation.z;
        //Quaternion playerRotation = vrcamera.rotation;
        //temp.Set(90, 0, playerRotation.z, 1);
        //temp = vrcamera.rotation.z;

        //transform.rotation = temp;
    }
}
