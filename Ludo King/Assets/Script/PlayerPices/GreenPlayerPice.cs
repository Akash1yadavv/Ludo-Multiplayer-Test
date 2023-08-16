using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerPice : PlayerPice
{
    RolingDice greenHomeRolingDice;

    // Start is called before the first frame update
    void Start()
    {
        greenHomeRolingDice = GetComponentInParent<GreenHome>().rolingDice;
    }


    public void OnMouseDown()
    {
        if (GameManager.gm.rolingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rolingDice == greenHomeRolingDice && GameManager.gm.numberOfStepsToMove == 6 && GameManager.gm.canPlayerMove)
                {
                    GameManager.gm.greenOutPlayer +=1;
                    MakePlayerReadyToMove(pathParent.GreenPathPoint);
                    return;
                }
            }
            if (GameManager.gm.rolingDice == greenHomeRolingDice && isReady && GameManager.gm.canPlayerMove)
            {
                MovePlayer(pathParent.GreenPathPoint);
            }
        }

    }
}
