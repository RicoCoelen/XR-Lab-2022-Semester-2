using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayScript : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] public int _brushSize = 8;

    private Renderer _renderer;
    public Color _colors;
    public float _tipHeight;

    private RaycastHit _touch;
    private Wall _Wall;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;


    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = _renderer.material.color;
        _tipHeight = _tip.localScale.y;
    }

    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(_tip.position, -_tip.transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Wall"))
            {
                if (_Wall == null)
                {
                    _Wall = _touch.transform.GetComponent<Wall>();

                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _Wall.textureSize.x - (_brushSize));
                var y = (int)(_touchPos.y * _Wall.textureSize.y - (_brushSize / 2));

                if (y < 0 || y > _Wall.textureSize.y || x < 0 || x > _Wall.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    _Wall.texture.SetPixel(x, y, _colors);

                    for (float F = 0.01f; F < 1.00f; F += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, F);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, F);
                        _Wall.texture.SetPixel(lerpX, lerpY, _colors);
                    }

                    _Wall.texture.Apply();

                }

                _lastTouchPos = new Vector2(x, y);
                _touchedLastFrame = true;
                return;
            }
        }

        _Wall = null;
        _touchedLastFrame = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_tip.position, _touch.point);
    }
}
