using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Paint : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _brushSize = 8;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Wall _Wall;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;


    void Start()

    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, _brushSize * _brushSize).ToArray();
        _tipHeight = _tip.localScale.y;

    }

    void Update()
    {
        Draw();

    }
    private void Draw()
    {
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Wall"))
            {
                if (_Wall == null)
                {
                    _Wall = _touch.transform.GetComponent<Wall>();

                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _Wall.textureSize.x - (_brushSize / 2));
                var y = (int)(_touchPos.y * _Wall.textureSize.y - (_brushSize / 2));

                if (y < 0 || y > _Wall.textureSize.y || x < 0 || x > _Wall.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    _Wall.texture.SetPixels(x, y, _brushSize, _brushSize, _colors);

                    for (float F = 0.01f; F < 1.00f; F += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, F);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, F);
                        _Wall.texture.SetPixels(lerpX, lerpY, _brushSize, _brushSize, _colors);
                    }

                    transform.rotation = _lastTouchRot;

                    _Wall.texture.Apply();

                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _Wall = null;
        _touchedLastFrame = false;
    }
}
