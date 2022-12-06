using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
    //Object References
    [SerializeField] public GameObject moveBox;
    public GameObject controller;

    //Position Variables
    private int boardX = -1, boardY = -1;
    
    //Player color var
    private string player;


    //Sprite References
    [SerializeField] public Sprite black_king, black_queen, black_knight, black_bishop, black_rook, black_pawn;
    [SerializeField] public Sprite white_king, white_queen, white_knight, white_bishop, white_rook, white_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Adjustment for instance transform
        SetTrans();

        switch(this.name)
        {
            //black
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;

            //white
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }

    public void SetTrans()
    {
        float x = boardX;
        float y = boardY;

        x = (x * 0.88f);
        y = (y * 0.88f);

        x += -3.066f;
        y += -3.066f;

        this.transform.position = new Vector3(x, y, -1);
    }

    public int GetBoardX() 
    { 
        return boardX; 
    }

    public int GetBoardY() 
    {
        return boardY;
    }

    public void SetBoardX(int x)
    {
        boardX = x;

    }

    public void SetBoardY(int y)
    {
        boardY = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovers();

            InitiateMovers();
        }
        
    }


    public void DestroyMovers()
    {
        GameObject[] movers = GameObject.FindGameObjectsWithTag("Mover");
        for (int i = 0; i < movers.Length; i++)
        {
            Destroy(movers[i]);
        }
    }
    
    private void InitiateMovers()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMover(1, 0);
                LineMover(0, 1);
                LineMover(1, 1);
                LineMover(-1, 0);
                LineMover(0, -1);
                LineMover(-1, -1);
                LineMover(-1, 1);
                LineMover(1, -1);
                break;

            case "black_knight":
            case "white_knight":
                LMover();
                break;

            case "black_bishop":
            case "white_bishop":
                LineMover(1, 1);
                LineMover(-1, 1);
                LineMover(1, -1);
                LineMover(-1, -1);
                break;

            case "black_king":
            case "white_king":
                SurroundMover();
                break;

            case "black_rook":
            case "white_rook":
                LineMover(1, 0);
                LineMover(0, 1);
                LineMover(-1, 0);
                LineMover(0, -1);
                break;

            case "black_pawn":
                PawnMover(boardX, boardY - 1);
                break;

            case "white_pawn":
                PawnMover(boardX, boardY + 1);  
                break;
        }
    }


    private void LineMover(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = boardX + xIncrement;
        int y = boardY + yIncrement;

        while (sc.PosOnBoard(x, y) && sc.GetPos(x, y) == null)
        {
            SpawnMover(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PosOnBoard(x, y) && sc.GetPos(x, y).GetComponent<Chess>().player != player)
        {
            SpawnKillMover(x, y);
        }
    }

    public void LMover()
    {
        PointMover(boardX + 1, boardY + 2);
        PointMover(boardX - 1, boardY + 2);
        PointMover(boardX + 2, boardY + 1);
        PointMover(boardX + 2, boardY - 1);
        PointMover(boardX + 1, boardY - 2);
        PointMover(boardX - 1, boardY - 2);
        PointMover(boardX - 2, boardY + 1);
        PointMover(boardX - 2, boardY - 1);
    }

    public void SurroundMover()
    {
        PointMover(boardX, boardY + 1);
        PointMover(boardX, boardY - 1);
        PointMover(boardX - 1, boardY - 1);
        PointMover(boardX - 1, boardY);
        PointMover(boardX - 1, boardY + 1);
        PointMover(boardX + 1, boardY - 1);
        PointMover(boardX + 1, boardY);
        PointMover(boardX + 1, boardY + 1);
    }

    public void PointMover(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PosOnBoard(x, y))
        {
            GameObject piece = sc.GetPos(x, y);

            if (piece == null)
            {
                SpawnMover(x, y);
            }

            else if(piece.GetComponent<Chess>().player != player)
            {
                SpawnKillMover(x, y);
            }
        }
    }

    public void PawnMover(int x, int y) 
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PosOnBoard(x, y))
        {
            if (sc.GetPos(x, y) == null)
            {
                SpawnMover(x, y);
            }

            if (sc.PosOnBoard(x + 1, y) && sc.GetPos(x + 1, y) != null && sc.GetPos(x + 1, y).GetComponent<Chess>().player != player)
            {
                SpawnKillMover(x + 1, y);
            }

            if (sc.PosOnBoard(x - 1, y) && sc.GetPos(x - 1, y) != null && sc.GetPos(x - 1, y).GetComponent<Chess>().player != player)
            {
                SpawnKillMover(x - 1, y);
            }
        }
    }

    public void SpawnMover(int gridX, int gridY)
    {
        float x = gridX;
        float y = gridY;

        x = (x * 0.88f);
        y = (y * 0.88f);

        x += -3.066f;
        y += -3.066f;

        GameObject mover = Instantiate(moveBox, new Vector3(x, y, -3), Quaternion.identity);

        Mover moverScript = mover.GetComponent<Mover>();
        moverScript.SetRef(gameObject);
        moverScript.SetCoords(gridX, gridY);
    }

    public void SpawnKillMover(int gridX, int gridY)
    {
        float x = gridX;
        float y = gridY;

        x = (x * 0.88f);
        y = (y * 0.88f);

        x += -3.066f;
        y += -3.066f;

        GameObject mover = Instantiate(moveBox, new Vector3(x, y, -3), Quaternion.identity);

        Mover moverScript = mover.GetComponent<Mover>();
        moverScript.kill = true;
        moverScript.SetRef(gameObject);
        moverScript.SetCoords(gridX, gridY);
    }
}
