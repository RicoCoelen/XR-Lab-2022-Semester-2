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
        if (clamp)
        {
            clampObject = GameObject.Find("ColorGradient");
            clampMesh = clampObject.GetComponent<MeshFilter>();
            bounds = clampMesh.mesh.bounds;

            transform.position = clampObject.transform.position;
        }  
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // assign new positon
        var objectSize = Vector3.Scale(clampObject.transform.localScale, bounds.size);
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, clampObject.transform.position.x, clampObject.transform.position.x + objectSize.x),
            clampObject.transform.position.y + 0.1f,
            Mathf.Clamp(transform.position.z, clampObject.transform.position.z, clampObject.transform.position.z + objectSize.z)
            );
        
        transform.rotation = clampObject.transform.rotation; 
        transform.parent = clampObject.transform; 

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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastPosition.position, transform.TransformDirection(Vector3.down) * 1000);
    }
}
