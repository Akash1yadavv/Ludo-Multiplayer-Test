using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{

    public PathObjectParent pathObjectParent;
    public List<PlayerPice> playerPiceList = new List<PlayerPice>();

    //for tracking the reverse direction of the player
    PathPoint[] pathPointToMoveOn_;




    // Start is called before the first frame update
    void Start()
    {
        pathObjectParent = GetComponentInParent<PathObjectParent>();
    }


    //code for kill the player
    public bool AddPlayerPice(PlayerPice playerPice_)
    {
        if (this.name == "ComonPathPoint")
        {
            addPlayer(playerPice_);
            complete(playerPice_);
            return false;
        }
        if (!pathObjectParent.savePoint.Contains(this))
        {
            if (playerPiceList.Count == 1)
            {
                string preePlayerPiceName = playerPiceList[0].name;
                string currentPlayerPiceName = playerPice_.name;
                currentPlayerPiceName = currentPlayerPiceName.Substring(0, currentPlayerPiceName.Length - 4);

                if (!preePlayerPiceName.Contains(currentPlayerPiceName))
                {
                    playerPiceList[0].isReady = false;
                    StartCoroutine(reverseOnStart(playerPiceList[0]));
                    playerPiceList[0].numberOfStepsAlreadyMove = 0;
                    RemovePlayerPice(playerPiceList[0]);
                    playerPiceList.Add(playerPice_);
                    return false;
                }
            }
        }
        addPlayer(playerPice_);
        return true;
    }

    IEnumerator reverseOnStart(PlayerPice playerPice_)
    {
        if (playerPice_.name.Contains("Blue"))
        {
            GameManager.gm.blueOutPlayer -= 1;
            pathPointToMoveOn_ = pathObjectParent.BluePathPoint;
        }
        else if (playerPice_.name.Contains("Red"))
        {
            GameManager.gm.redOutPlayer -= 1;
            pathPointToMoveOn_ = pathObjectParent.RedPathPoint;
        }
        else if (playerPice_.name.Contains("Green"))
        {
            GameManager.gm.greenOutPlayer -= 1;
            pathPointToMoveOn_ = pathObjectParent.GreenPathPoint;
        }
        else
        {
            GameManager.gm.yellowOutPlayer -= 1;
            pathPointToMoveOn_ = pathObjectParent.YellowPathPoint;
        }

        //for reverse movement of player
        for (int i = playerPice_.numberOfStepsAlreadyMove - 1; i >= 0; i--)
        {
            playerPice_.transform.position = pathPointToMoveOn_[i].transform.position;
            yield return new WaitForSeconds(0.02f);
        }

        playerPice_.transform.position = pathObjectParent.BasePoint[BasePointPosition(playerPice_.name)].transform.position;


    }
    int BasePointPosition(string name)
    {
        for (int i = 0; i < pathObjectParent.BasePoint.Length; i++)
        {
            if (pathObjectParent.BasePoint[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }

    void addPlayer(PlayerPice playerPice_)
    {
        playerPiceList.Add(playerPice_);
        RescaleAndRepositionAllPlayerPice();
    }

    public void RemovePlayerPice(PlayerPice playerPice_)
    {
        if (playerPiceList.Contains(playerPice_))
        {
            playerPiceList.Remove(playerPice_);
            RescaleAndRepositionAllPlayerPice();
        }
    }

    void complete(PlayerPice playerPice_)
    {
        int totalCOmpleteCompletePlayer;

        if (playerPice_.name.Contains("Blue"))
        {
            GameManager.gm.blueOutPlayer -= 1;
            totalCOmpleteCompletePlayer = GameManager.gm.blueCompletePlayer += 1;

        }
        else if (playerPice_.name.Contains("Red"))
        {
            GameManager.gm.redOutPlayer -= 1;
            totalCOmpleteCompletePlayer = GameManager.gm.redCompletePlayer += 1;

        }
        else if (playerPice_.name.Contains("Green"))
        {
            GameManager.gm.greenOutPlayer -= 1;
            totalCOmpleteCompletePlayer = GameManager.gm.greenCompletePlayer += 1;

        }
        else
        {
            GameManager.gm.yellowOutPlayer -= 1;
            totalCOmpleteCompletePlayer = GameManager.gm.yellowCompletePlayer += 1;

        }

        if (totalCOmpleteCompletePlayer == 4)
        {
            //celebrate
        }
    }

    public void RescaleAndRepositionAllPlayerPice()
    {
        int plsCount = playerPiceList.Count;
        bool isOdd = (plsCount % 2) == 0 ? false : true;

        int extent = plsCount / 2;
        int counter = 0;
        int SpriteLayer = 0;

        if (isOdd)
        {
            for (int i = -extent; i <= extent; i++)
            {
                playerPiceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount - 1], 1f);
                playerPiceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }
        }
        else
        {
            for (int i = -extent; i < extent; i++)
            {
                playerPiceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount - 1], 1f);
                playerPiceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }
        }
        for (int i = 0; i < playerPiceList.Count; i++)
        {
            playerPiceList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = SpriteLayer;
            SpriteLayer++;
        }
    }
}
