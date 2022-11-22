using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class ColorPicker : MonoBehaviour
{
    public Transform raycastPosition;
    public GameObject changeObject;
    public Color stolenColor;
    public GameObject clampObject;
    public bool clamp = false;

    // Update is called once per frame
    private void FixedUpdate()
    {
        // make clamped new position
        var newPos = new Vector3(
            Mathf.Clamp(transform.position.x, clampObject.transform.position.x, clampObject.transform.localScale.x), 
            transform.position.y + 0.1f,
            Mathf.Clamp(transform.position.z, clampObject.transform.position.z, clampObject.transform.localScale.z)
            );

        // assign new positon
        transform.position = newPos;

        RaycastHit hit;
        
        if (Physics.Raycast(raycastPosition.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.collider && hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
            {
                stolenColor = renderer.material.color;
                changeObject.GetComponent<Renderer>().material.color = stolenColor;
            }

            Vector2 hitPos;
            hitPos = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

            Wall wall;
            if (wall = hit.transform.GetComponent<Wall>())
            {
                var x = (int)(hitPos.x * wall.textureSize.x);
                var y = (int)(hitPos.y * wall.textureSize.y);

                if (y < 0 || y > wall.textureSize.y || x < 0 || x > wall.textureSize.x) return;

                stolenColor = wall.texture.GetPixel(x, y);

                changeObject.GetComponent<Renderer>().material.color = stolenColor;
            }
        }
    }
}
