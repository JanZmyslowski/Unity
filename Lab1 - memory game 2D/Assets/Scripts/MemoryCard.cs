using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    //[SerializeField] private Sprite image;
    [SerializeField] private SceneController controller;

    private int CardId { get; set; }

    public bool getCradBackIsActive() => cardBack.active;

    public void setCard(int id, Sprite image)
    {
        CardId = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public int getCardId()
    {
        return CardId;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canRev())
        {
            cardBack.SetActive(false);
            controller.CardRev(this);
        }
    }

    public void unRev()
    {
        cardBack.SetActive(true);
    }

    public void removeCard()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
