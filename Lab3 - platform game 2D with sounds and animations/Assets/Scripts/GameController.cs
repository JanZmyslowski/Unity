using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
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
    [RangeAttribute(1, 10)]
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
    [SerializeField]
    private Enemy[] enemies;

    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");

    }
    void Start()
    {
        info.enabled = false;
        restartButtin.SetActive(false);
        health.text = currHealth + "/" + 3;
        points.text = currPoints + "/" + point_per_lvl;
        Assert.IsNotNull(player, "Player is null");
        Assert.IsNotNull(points, "Point presenter is null");
        Assert.IsNotNull(health, "Health presenter is null");
        Assert.IsNotNull(info, "Info presenter is null");
        Assert.IsNotNull(restartButtin, "Restart button is null");

    }

    // Update is called once per frame
    void Update()
    {
        Assert.AreNotEqual(-1, currPoints);
        if (info.enabled == false)
        {
            Assert.IsTrue(currHealth >= 0);
        }
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
    private void stopEnemies()
    {
        foreach (var enemy in enemies)
        {
            if(enemy !=null )
            enemy.makeDisable();
        }
    }
    IEnumerator EndGame()
    {
        player.DisableMoveing();
        stopEnemies();
        info.text = "You win!";
        info.enabled = true;
        yield return new WaitForSeconds(1.5f);
        restartButtin.SetActive(true);

    }
    IEnumerator NextLvl()
    {
        player.DisableMoveing();
        stopEnemies();
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
        stopEnemies();
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
