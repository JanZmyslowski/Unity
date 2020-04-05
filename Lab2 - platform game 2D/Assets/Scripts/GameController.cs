using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Player player;
    [SerializeField]
    private TextMeshPro points;
    [SerializeField]
    private TextMeshPro health;
    private int currPoints = 0;
    private int currHealth = 3;
    [SerializeField]
    private int point_per_lvl = 4;
    [SerializeField]
    private string currLvl = "Level1";
    [SerializeField]
    private string nextLvl = "Level2";
    [SerializeField]
    private TextMeshPro info;
    [SerializeField]
    private GameObject restartButtin;
   
    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");

    }
    void Start()
    {
        info.enabled = false;
        restartButtin.SetActive(false);
        player.MakeLife();
        health.text = currHealth + "/" + 3;
        points.text = currPoints + "/" + point_per_lvl;

    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameObject.transform.position.y < -15)
        {
            SceneManager.LoadScene(currLvl);
        }
        if (currPoints == point_per_lvl)
        {
            if (nextLvl != "End")
            {
                StartCoroutine(NextLvl());
            }
            else
            {
                StartCoroutine(EndGame());
            }
        }
    }
    IEnumerator EndGame()
    {
        player.DisableMoveing();
        info.text = "You win!";
        info.enabled = true;
        yield return new WaitForSeconds(1.5f);
        restartButtin.SetActive(true);

    }
    IEnumerator NextLvl()
    {
        player.DisableMoveing();       
        info.text = "Next level loading...";
        info.enabled = true;
        yield return new WaitForSeconds(3.0f);
        info.enabled = false;
        SceneManager.LoadScene(nextLvl);
    }

    IEnumerator RespawnPlayer()
    {
        //player.gameObject.transform.position = startPos;
        player.MakeDead();       
        info.text = "You died...";
        yield return new WaitForSeconds(1.5f);
        info.enabled = true;
        restartButtin.SetActive(true);

    }
    void CollectDiamond()
    {
        currPoints++;
        points.text = currPoints + "/" + point_per_lvl;
    }
    void Hurt()
    {
        if(currHealth>0)
        currHealth--;
        health.text = currHealth + "/" + 3;
        if (currHealth == 0)
        {
            player.DisableMoveing();
            StartCoroutine(RespawnPlayer());
        }

    }
}
