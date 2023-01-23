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

        Quaternion temp = Quaternion.identity;
        temp.x = 90;
        temp.y = vrcamera.rotation.y;

        transform.rotation = temp;
    }
}
