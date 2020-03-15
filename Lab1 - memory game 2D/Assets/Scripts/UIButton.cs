using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;
    [SerializeField] private string targetMsg;
    public Color highlightColor = Color.cyan;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void OnMouseOver()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor;
        }
    }

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }
    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (targetObj != null)
        {
            targetObj.SendMessage(targetMsg);
        }
    }
}
