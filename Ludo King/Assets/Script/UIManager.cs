using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject GamePanel;


    public void Game1()
    {
        GameManager.gm.totalPlayerCanPlay = 2;
        MainPanel.SetActive(false);
        GamePanel.SetActive(true);
        GameManager.gm.playerHome[1].SetActive(false);
        GameManager.gm.playerHome[3].SetActive(false);
    }
    public void Game2()
    {
        GameManager.gm.totalPlayerCanPlay = 3;
        MainPanel.SetActive(false);
        GamePanel.SetActive(true);
        GameManager.gm.playerHome[3].SetActive(false);
    }
    public void Game3()
    {
        GameManager.gm.totalPlayerCanPlay = 4;
        MainPanel.SetActive(false);
        GamePanel.SetActive(true);
    }
    public void Game4()
    {
        GameManager.gm.totalPlayerCanPlay = 1;
        MainPanel.SetActive(false);
        GamePanel.SetActive(true);
        GameManager.gm.playerHome[1].SetActive(false);
        GameManager.gm.playerHome[3].SetActive(false);
    }

}
