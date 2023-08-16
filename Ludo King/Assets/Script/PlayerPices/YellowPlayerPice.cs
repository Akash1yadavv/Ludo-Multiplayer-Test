using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayerPice : PlayerPice
{
    RolingDice yellowHomeRolingDice;

    // Start is called before the first frame update
    void Start()
    {
        yellowHomeRolingDice = GetComponentInParent<YellowHome>().rolingDice;
    }

    public void OnMouseDown()
    {
        if (GameManager.gm.rolingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rolingDice == yellowHomeRolingDice && GameManager.gm.numberOfStepsToMove == 6 && GameManager.gm.canPlayerMove)
                {
                    GameManager.gm.yellowOutPlayer +=1;
                    MakePlayerReadyToMove(pathParent.YellowPathPoint);
                    return;
                }
            }
            if (GameManager.gm.rolingDice == yellowHomeRolingDice && isReady && GameManager.gm.canPlayerMove)
            {
                MovePlayer(pathParent.YellowPathPoint);
            }
        }

    }
}
