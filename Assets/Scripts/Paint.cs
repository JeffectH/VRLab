using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Paint : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeights;

    private RaycastHit _touchHit;
    private WhiteBoard _whiteaboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    private void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(Color.red, _penSize * _penSize).ToArray();
        _tipHeights = _tip.localScale.y;
    }


    private void Update()
    {
        Draw();  
    }


    private void Draw()
    {
        if (Physics.Raycast(_tip.position, transform.forward, out _touchHit, _tipHeights)) 
        {
            if (_touchHit.collider.GetComponent<WhiteBoard>()) 
            {
                Debug.Log("Доска");
                if (_whiteaboard == null) 
                {
                    _whiteaboard = _touchHit.transform.GetComponent<WhiteBoard>();
                }

                _touchPos = new Vector2(_touchHit.textureCoord.x, _touchHit.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteaboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteaboard.textureSize.y - (_penSize / 2));

                if (y < 0 || y > _whiteaboard.textureSize.y || x < 0 || x > _whiteaboard.textureSize.x) { return; }

                if (_touchedLastFrame) 
                {
                    _whiteaboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteaboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);

                    }

                    transform.rotation = _lastTouchRot;

                    _whiteaboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;

            }
        }

        _whiteaboard = null;
        _touchedLastFrame = false;

    }
}