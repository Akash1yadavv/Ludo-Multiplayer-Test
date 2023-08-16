using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPice : MonoBehaviour
{

    public bool moveNow;
    //it will check that our components are ready ot move or not
    public bool isReady;
    public int numberOfStepsToMove;
    public int numberOfStepsAlreadyMove;

    Coroutine playerMovement;

    public PathPoint previousPathPoint;
    public PathPoint currentPathPoint;


    //connect pathpoint and player
    public PathObjectParent pathParent;
    //awake function will execute first
    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    //player out of home
    public void MakePlayerReadyToMove(PathPoint[] pathParent_)
    {
        isReady = true;
        //move player to outside oh home at zero position
        transform.position = pathParent_[0].transform.position;
        numberOfStepsAlreadyMove = 1;
        GameManager.gm.numberOfStepsToMove = 0;

        previousPathPoint = pathParent_[0];
        currentPathPoint = pathParent_[0];
        currentPathPoint.AddPlayerPice(this);
        GameManager.gm.AddPathPoint(currentPathPoint);

        GameManager.gm.rolingDiceTransfer();
    }

    public void MovePlayer(PathPoint[] pathParent_)
    {
        StartCoroutine(MoveStep_enum(pathParent_));
    }

    IEnumerator MoveStep_enum(PathPoint[] pathParent_)
    {
        yield return new WaitForSeconds(0.25f);
        numberOfStepsToMove = GameManager.gm.numberOfStepsToMove;
        for (int i = numberOfStepsAlreadyMove; i < (numberOfStepsAlreadyMove + numberOfStepsToMove); i++)
        {
            if (isPathPointAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMove, pathParent_))
            {
                transform.position = pathParent_[i].transform.position;
                yield return new WaitForSeconds(0.35f);
            }
        }

        if (isPathPointAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMove, pathParent_))
        {
            numberOfStepsAlreadyMove += numberOfStepsToMove;

            //if any player present on the path then it will remove and place new player
            GameManager.gm.RemovePathPoint(previousPathPoint);
            previousPathPoint.RemovePlayerPice(this);

            currentPathPoint = pathParent_[numberOfStepsAlreadyMove - 1];
            bool trnsfr = currentPathPoint.AddPlayerPice(this);
            currentPathPoint.RescaleAndRepositionAllPlayerPice();
            GameManager.gm.AddPathPoint(currentPathPoint);

            previousPathPoint = currentPathPoint;

            if (trnsfr && GameManager.gm.numberOfStepsToMove != 6)
            {

                GameManager.gm.transferDice = true;
            }

            GameManager.gm.numberOfStepsToMove = 0;

        }
        GameManager.gm.canPlayerMove = true;
        GameManager.gm.rolingDiceTransfer();

        if (playerMovement != null)
        {
            StopCoroutine("MoveStep_enum");
        }
    }

    //it will check that the path is available or not for movement
    public bool isPathPointAvailableToMove(int numberOfStepsToMove, int numberOfStepsAlreadyMove, PathPoint[] pathParent_)
    {
        if (numberOfStepsToMove == 0)
        {
            return false;
        }
        int leftNumberOfPath = pathParent_.Length - numberOfStepsAlreadyMove;
        if (leftNumberOfPath >= numberOfStepsToMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
