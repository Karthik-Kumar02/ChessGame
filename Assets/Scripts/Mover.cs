using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //board positions only
    int gridX;
    int gridY;

    //false is a move and true is a kill
    public bool kill = false;

    public void Start()
    {
        if(kill)
        {
            //set to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if(kill)
        {
            GameObject piece = controller.GetComponent<Game>().GetPos(gridX, gridY);

            if(piece.name == "white_king")
            {
                controller.GetComponent<Game>().Winner("black");
            }

            if (piece.name == "black_king")
            {
                controller.GetComponent<Game>().Winner("white");
            }

            Destroy(piece);
        }

        controller.GetComponent<Game>().SetPosEmpty(reference.GetComponent<Chess>().GetBoardX(), reference.GetComponent<Chess>().GetBoardY());

        reference.GetComponent<Chess>().SetBoardX(gridX);
        reference.GetComponent<Chess>().SetBoardY(gridY);
        reference.GetComponent<Chess>().SetTrans();

        controller.GetComponent<Game>().SetPos(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<Chess>().DestroyMovers();
    }

    public void SetCoords(int x, int y)
    {
        gridX = x;
        gridY = y;
    }

    public void SetRef(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetRef() 
    { 
        return reference; 
    }
}
