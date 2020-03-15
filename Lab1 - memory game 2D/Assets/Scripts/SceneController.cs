using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] public static int gridRows = 2;
    [SerializeField] public static int gridCols = 4;
    [SerializeField] public static int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
    [SerializeField] public static float offsetX = 2f;
    [SerializeField] public static float offsetY = 2.5f;
    [SerializeField] public static float offsetStartX = 0;
    [SerializeField] public static float offsetStartY = 0;
    [SerializeField] public static Vector3 scaleFactor = new Vector3(1,1,1);

    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMeshPro scoreField;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TextMeshPro result;
    private List<MemoryCard> cards = new List<MemoryCard>();
    //private static Vector3 basicPos = new Vector3(-3,1,0);
    private MemoryCard firstRev;
    private MemoryCard secRev;
    private int score = 0;

    public bool canRev()
    {
        return secRev == null;
    }

    public void CardRev(MemoryCard card)
    {
        if(firstRev == null)
        {
            firstRev = card;
        } else
        {
            secRev = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1.0f);
        if (firstRev.getCardId() == secRev.getCardId())
        {
            score += 10;
            Debug.Log(firstRev.getCardId() + " = " + secRev.getCardId());
            scoreField.text = "Score: " + score;
            bool isWin = true;
            firstRev.removeCard();
            secRev.removeCard();
            foreach (var card in cards)
            {
                if (card.getCradBackIsActive())
                {
                    isWin = false;
                }
            }
            if (isWin)
            {
                Vector3 camPos = mainCamera.transform.position;
                mainCamera.transform.position = new Vector3(camPos.x, camPos.y, 0);
                result.text = "YOU WIN!";
                result.enabled = true;
            }
        }
        else
        {
            score -= 2;
            scoreField.text = "Score: " + score;
            if (score <= -cards.Count*2)
            {
                Vector3 camPos = mainCamera.transform.position;
                mainCamera.transform.position = new Vector3(camPos.x, camPos.y, 0);
                result.text = "YOU LOSE!";
                result.enabled = true;
            }
            else
            {
                firstRev.unRev();
                secRev.unRev();
            }
        }
        firstRev = null;
        secRev = null;
       
    }

    public void Reset()
    {
        cards = new List<MemoryCard>();
        SceneManager.LoadScene("SampleScene");
    }

    public void mode2x2()
    {
        gridRows = 2;
        gridCols = 2;
        offsetX = 2f;
        offsetY = 2.5f;
        offsetStartX = 2f;
        offsetStartY = 0;
        scaleFactor = new Vector3(1, 1, 1);
        //originalCard.transform.position = new Vector3(basicPos.x, -offsetY + basicPos.y, basicPos.z);
        int[] tmp = { 0, 0, 1, 1 };
        numbers = tmp;
        SceneManager.LoadScene("SampleScene");
    }
    public void mode2x4()
    {
        gridRows = 2;
        gridCols = 4;
        int[] tmp = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = tmp;
        offsetX = 2f;
        offsetY = 2.5f;
        offsetStartX = 0;
        offsetStartY = 0;
        scaleFactor = new Vector3(1, 1, 1);
        //originalCard.transform.position = basicPos;
        SceneManager.LoadScene("SampleScene");
    }
    public void mode4x4()
    {
        gridRows = 4;
        gridCols = 4;
        int[] tmp = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        numbers = tmp;
        scaleFactor = new Vector3(.6f, .6f, 1);
        offsetX = 2f*.6f;
        offsetY = 2.5f*.6f;
        offsetStartX = 1f;
        offsetStartY = 0.5f;
        SceneManager.LoadScene("SampleScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        //int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);
        originalCard.transform.position = new Vector3(-3+ offsetStartX, offsetStartY+1,0);
        originalCard.transform.localScale = scaleFactor;
        Vector3 startPos = originalCard.transform.position;
        cards = new List<MemoryCard>();
        Vector3 camPos = mainCamera.transform.position;
        mainCamera.transform.position = new Vector3(camPos.x, camPos.y, -10);
        result.enabled = false;
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if( i == 0 && j == 0)
                {
                    card = originalCard;
                } else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.setCard(id, images[id]);
                
                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
                cards.Add(card);
            }

        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newarray = numbers.Clone() as int[];
        for (int i = 0; i < newarray.Length; i++)
        {
            int tmp = newarray[i];
            int r = Random.Range(i, newarray.Length);
            newarray[i] = newarray[r];
            newarray[r] = tmp;
        }
        return newarray;
    }

  
}
