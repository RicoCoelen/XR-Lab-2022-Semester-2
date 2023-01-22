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
    public MeshFilter clampMesh;
    Bounds bounds;

    public bool clamp = false;

    public void Awake()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPosition.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.collider && hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
            {
                try
                {
                    stolenColor = renderer.material.color;
                }
                catch
                {

                }
            }

            Texture2D tex = hit.collider.GetComponent<Renderer>().material.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;
            stolenColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
        }
        changeColor(stolenColor);
    }

    public void changeColor(Color color)
    {
        changeObject.GetComponent<Renderer>().material.color = color;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastPosition.position, transform.TransformDirection(Vector3.down) * 1000);
    }
}
