using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerPice : PlayerPice
{
    RolingDice redHomeRolingDice;

    // Start is called before the first frame update
    void Start()
    {
        redHomeRolingDice = GetComponentInParent<RedHome>().rolingDice;
    }

    public void OnMouseDown()
    {
        if (GameManager.gm.rolingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rolingDice == redHomeRolingDice && GameManager.gm.numberOfStepsToMove == 6 && GameManager.gm.canPlayerMove)
                {
                    GameManager.gm.redOutPlayer += 1;
                    MakePlayerReadyToMove(pathParent.RedPathPoint);
                    return;
                }
            }
            if (GameManager.gm.rolingDice == redHomeRolingDice && isReady && GameManager.gm.canPlayerMove)
            {
                MovePlayer(pathParent.RedPathPoint);
            }
        }

    }

}
