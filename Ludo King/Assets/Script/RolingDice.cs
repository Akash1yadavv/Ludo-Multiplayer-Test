using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolingDice : MonoBehaviour
{

    [SerializeField] Sprite[] numberSprites;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rolingDiceAnimation;
    [SerializeField] int numberGot;

    Coroutine generateRandomNumberDice;

    int otPlayers;

    List<PlayerPice> playerPices;

    PathPoint[] currentPathPoints;

    public PlayerPice currentPlayerPice;



    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnMouseDown()
    {
        generateRandomNumberDice = StartCoroutine(rollDice());
    }
    public void MouseRole()
    {
        generateRandomNumberDice = StartCoroutine(rollDice());
    }

    IEnumerator rollDice()
    {
        yield return new WaitForEndOfFrame();
        if (GameManager.gm.canDiceRoll)
        {
            GameManager.gm.canDiceRoll = false;
            numberSpriteHolder.gameObject.SetActive(false);
            rolingDiceAnimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);

            //avoid 32 times six and about six
            int maxnum = 6;
            if (GameManager.gm.TotalSix == 2)
            {
                maxnum = 5;
                GameManager.gm.TotalSix = 0;
            }
            numberGot = Random.Range(0, maxnum);
            if (numberGot == 5)
            {
                GameManager.gm.TotalSix += 1;
            }

            numberSpriteHolder.sprite = numberSprites[numberGot];
            numberGot++;
            //for moving the player
            GameManager.gm.numberOfStepsToMove = numberGot;
            //for identify the home with dice
            GameManager.gm.rolingDice = this;

            numberSpriteHolder.gameObject.SetActive(true);
            rolingDiceAnimation.gameObject.SetActive(false);
            outPlayers();

            if (playerCanMove())
            {
                if (otPlayers == 0)
                {
                    ReadyToMove();

                }
                else
                {
                    currentPlayerPice.MovePlayer(currentPathPoints);
                }
            }
            else
            {
                //whenever dice roll it transfer the dice to opponent
                if (GameManager.gm.numberOfStepsToMove != 6 && otPlayers == 0)
                {
                    GameManager.gm.transferDice = true;
                    yield return new WaitForSeconds(0.5f);
                    GameManager.gm.rolingDiceTransfer();
                }
            }




            if (generateRandomNumberDice != null)
            {
                StopCoroutine(rollDice());
            }
        }
    }
    public void outPlayers()
    {
        if (GameManager.gm.rolingDice == GameManager.gm.rolingDiceList[0])
        {
            playerPices = GameManager.gm.bluePlayerPices;
            currentPathPoints = playerPices[0].pathParent.BluePathPoint;
            otPlayers = GameManager.gm.blueOutPlayer;
        }
        else if (GameManager.gm.rolingDice == GameManager.gm.rolingDiceList[1])
        {
            playerPices = GameManager.gm.redPlayerPices;
            currentPathPoints = playerPices[0].pathParent.RedPathPoint;
            otPlayers = GameManager.gm.redOutPlayer;
        }
        else if (GameManager.gm.rolingDice == GameManager.gm.rolingDiceList[2])
        {
            playerPices = GameManager.gm.greenPlayerPices;
            currentPathPoints = playerPices[0].pathParent.GreenPathPoint;
            otPlayers = GameManager.gm.greenOutPlayer;
        }
        else
        {
            playerPices = GameManager.gm.yellowPlayerPices;
            currentPathPoints = playerPices[0].pathParent.YellowPathPoint;
            otPlayers = GameManager.gm.yellowOutPlayer;
        }

    }

    public bool playerCanMove()
    {

        if (GameManager.gm.totalPlayerCanPlay == 1)
        {
            if (GameManager.gm.rolingDice == GameManager.gm.rolingDiceList[2])
            {
                if (otPlayers > 0)
                {
                    for (int i = 0; i < playerPices.Count; i++)
                    {
                        if (playerPices[i].isReady)
                        {
                            if (playerPices[i].isPathPointAvailableToMove(GameManager.gm.numberOfStepsToMove, playerPices[i].numberOfStepsAlreadyMove, currentPathPoints))
                            {
                                currentPlayerPice = playerPices[i];
                                return true;
                            }
                        }
                    }
                }
            }
        }

        if (otPlayers == 1 && GameManager.gm.numberOfStepsToMove != 6)
        {
            for (int i = 0; i < playerPices.Count; i++)
            {
                if (playerPices[i].isReady)
                {
                    if (playerPices[i].isPathPointAvailableToMove(GameManager.gm.numberOfStepsToMove, playerPices[i].numberOfStepsAlreadyMove, currentPathPoints))
                    {
                        currentPlayerPice = playerPices[i];
                        return true;
                    }
                }
            }
        }
        else if (otPlayers == 0 && GameManager.gm.numberOfStepsToMove == 6)
        {
            return true;
        }
        return false;
    }

    void ReadyToMove()
    {
        if (GameManager.gm.rolingDice == GameManager.gm.rolingDiceList[0])
        {
            GameManager.gm.blueOutPlayer += 1;
        }
        else if (GameManager.gm.rolingDice == GameManager.gm.rolingDiceList[1])
        {
            GameManager.gm.redOutPlayer += 1;
        }
        else if (GameManager.gm.rolingDice == GameManager.gm.rolingDiceList[2])
        {
            GameManager.gm.greenOutPlayer += 1;
        }
        else
        {
            GameManager.gm.yellowOutPlayer += 1;
        }
        playerPices[0].MakePlayerReadyToMove(currentPathPoints);
    }
}
