using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private Transform raycastPosition;
    [SerializeField] private GameObject changeObject;
    private Color _stolenColor;
    private RaycastHit _hit;

    public Color stolenColor
    {
        get { return _stolenColor; }
        set { _stolenColor = value; }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPosition.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            try
            {
                if (hit.collider)
                {
                    Renderer renderer = hit.collider.GetComponent<Renderer>();
                    Texture2D tex = renderer.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    stolenColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                }
                changeColor(stolenColor);
            }
            catch (System.Exception e)
            {
                // Handle the error here
                Debug.LogError("Error Occured while trying to get color: " + e.Message);
            }
        }
    }

    public void changeColor(Color color)
    {
        changeObject.GetComponent<Renderer>().material.color = color;
    }
}