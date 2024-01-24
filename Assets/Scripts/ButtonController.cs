using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            _spriteRenderer.sprite = pressedImage;
        }
        
        if (Input.GetKeyUp(keyToPress))
        { 
            _spriteRenderer.sprite = defaultImage;
        }
    }

}
