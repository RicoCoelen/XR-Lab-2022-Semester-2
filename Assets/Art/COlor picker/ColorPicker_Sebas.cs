using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker_Sebas : MonoBehaviour
{
    [SerializeField]
    RectTransform _texture;
    [SerializeField]
    GameObject _sphereTest;
    [SerializeField]
    Texture2D _refSprite;

    public void OnClickPickerColor()
    {
        SetColor();
    }
    private void SetColor()
    {
        Vector3 imagepos = _texture.position;
        float globalPosX = Input.mousePosition.x - imagepos.x;
        float globalPosY = Input.mousePosition.y - imagepos.y;

        int localPosX = (int)(globalPosX * (_refSprite.width / _texture.rect.width));
        int localPosY = (int)(globalPosY * (_refSprite.height / _texture.rect.height));

        Color c = _refSprite.GetPixel(localPosX, localPosY);
        SetActualColor(c);

    }

    void SetActualColor(Color c)
    {
        _sphereTest.GetComponent<MeshRenderer>().material.color = c;
    }
}
