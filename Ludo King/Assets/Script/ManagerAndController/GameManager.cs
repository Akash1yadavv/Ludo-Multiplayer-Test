using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public int numberOfStepsToMove;

    public RolingDice rolingDice;

    public bool canPlayerMove = true;
    List<PathPoint> PlayersOnPathPointList = new List<PathPoint>();

    public bool canDiceRoll = true;

    public bool transferDice = false;

    public List<RolingDice> rolingDiceList;

    public int blueOutPlayer;
    public int redOutPlayer;
    public int greenOutPlayer;
    public int yellowOutPlayer;

    public int blueCompletePlayer;
    public int redCompletePlayer;
    public int greenCompletePlayer;
    public int yellowCompletePlayer;

    public int totalPlayerCanPlay;
    //it is use to provide player home
    public List<GameObject> playerHome;

    public int TotalSix;

    public List<PlayerPice> bluePlayerPices;
    public List<PlayerPice> redPlayerPices;
    public List<PlayerPice> greenPlayerPices;
    public List<PlayerPice> yellowPlayerPices;




    private void Awake()
    {
        gm = this;
    }

    //adding two or more then components on same index with some differences
    public void AddPathPoint(PathPoint pathPoint)
    {
        PlayersOnPathPointList.Add(pathPoint);
    }

    public void RemovePathPoint(PathPoint pathPoint)
    {
        if (PlayersOnPathPointList.Contains(pathPoint))
        {
            PlayersOnPathPointList.Remove(pathPoint);
        }
    }

    public void rolingDiceTransfer()
    {

        if (transferDice)
        {
            GameManager.gm.TotalSix = 0;
            transferRollingDice();
        }

        else
        {
            if (GameManager.gm.totalPlayerCanPlay == 1)
            {
                if (GameManager.gm.rolingDice == GameManager.gm.rolingDiceList[2])
                {
                    Invoke("role", 0.6f);
                }
            }
        }

        canDiceRoll = true;
        transferDice = false;
    }


    void role()
    {
        rolingDiceList[2].MouseRole();
    }


    void transferRollingDice()
    {
        if (GameManager.gm.totalPlayerCanPlay == 1)
        {
            if (rolingDice == rolingDiceList[0])
            {
                rolingDiceList[0].gameObject.SetActive(false);
                rolingDiceList[2].gameObject.SetActive(true);
                Invoke("role", 0.6f);
            }
            else
            {
                rolingDiceList[2].gameObject.SetActive(false);
                rolingDiceList[0].gameObject.SetActive(true);
            }
        }


        else if (GameManager.gm.totalPlayerCanPlay == 2)
        {
            if (rolingDice == rolingDiceList[0])
            {
                rolingDiceList[0].gameObject.SetActive(false);
                rolingDiceList[2].gameObject.SetActive(true);
            }
            else
            {
                rolingDiceList[2].gameObject.SetActive(false);
                rolingDiceList[0].gameObject.SetActive(true);
            }
        }

        else if (GameManager.gm.totalPlayerCanPlay == 3)
        {
            int nextDice;
            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    nextDice = 0;
                }
                else
                {
                    nextDice = i + 1;
                }
                i=passout(i);
                if (rolingDice == rolingDiceList[i])
                {
                    rolingDiceList[i].gameObject.SetActive(false);
                    rolingDiceList[nextDice].gameObject.SetActive(true);
                }
            }
        }

        else if (GameManager.gm.totalPlayerCanPlay == 4)
        {
            int nextDice;
            for (int i = 0; i < rolingDiceList.Count; i++)
            {
                if (i == (rolingDiceList.Count - 1))
                {
                    nextDice = 0;
                }
                else
                {
                    nextDice = i + 1;
                }
                i=passout(i);
                if (rolingDice == rolingDiceList[i])
                {
                    rolingDiceList[i].gameObject.SetActive(false);
                    rolingDiceList[nextDice].gameObject.SetActive(true);
                }
            }
        }
    }

    int passout(int i)
    {
        if (i == 0)
        {
            if (blueOutPlayer == 4)
            {
               return (i+1);
            }
        }
        else if (i == 1)
        {
            if (redOutPlayer == 4)
            {
               return (i+1);
            }
        }
        else if (i == 2)
        {
            if (greenOutPlayer == 4)
            {
               return (i+1);
            }
        }
        else if (i == 3)
        {
            if (yellowOutPlayer == 4)
            {
               return (i+1);
            }
        }
        return i;
    }
}
