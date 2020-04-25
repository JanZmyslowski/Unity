using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Diamond : MonoBehaviour
{
    [SerializeField]
    private GameController controller;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(controller, "Controller is null");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        controller.SendMessage("CollectDiamond");
    }
}
