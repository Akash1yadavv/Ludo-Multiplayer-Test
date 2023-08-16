using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerPice : PlayerPice
{
    //identify the player with roling dice
    RolingDice blueHomeRolingDice;

    // Start is called before the first frame update
    void Start()
    {
        blueHomeRolingDice = GetComponentInParent<BlueHome>().rolingDice;
    }

    public void OnMouseDown()
    {
        //first check dice is rolled or not 
        if (GameManager.gm.rolingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rolingDice == blueHomeRolingDice && GameManager.gm.numberOfStepsToMove == 6 && GameManager.gm.canPlayerMove)
                {
                    GameManager.gm.blueOutPlayer+=1;
                    MakePlayerReadyToMove(pathParent.BluePathPoint);
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.rolingDice == blueHomeRolingDice && isReady && GameManager.gm.canPlayerMove)
            {
                GameManager.gm.canPlayerMove=false;
                MovePlayer(pathParent.BluePathPoint);
            }
        }
    }
}
