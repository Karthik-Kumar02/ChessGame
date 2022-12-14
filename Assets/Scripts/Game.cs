using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] public GameObject piece;
    
    //postions and team for chess piece
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] blackPlayer= new GameObject[16];
    private GameObject[] whitePlayer = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        whitePlayer = new GameObject[]
        {
           Create("white_rook", 0, 0), Create("white_knight", 1, 0), Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
           Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
           Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1), Create("white_pawn", 3, 1), Create("white_pawn", 4, 1),
           Create("white_pawn", 5, 1), Create("white_pawn", 6, 1), Create("white_pawn", 7, 1)
        };

        blackPlayer = new GameObject[]
        {
            Create("black_rook", 0, 7), Create("black_knight", 1, 7), Create("black_bishop", 2, 7), Create("black_queen", 3, 7), Create("black_king", 4, 7),
            Create("black_bishop", 5, 7), Create("black_knight", 6, 7), Create("black_rook", 7, 7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6), Create("black_pawn", 3, 6), Create("black_pawn", 4, 6),
            Create("black_pawn", 5, 6), Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)
        };

        //set all positions of pieces on board
        for (int i = 0; i < blackPlayer.Length; i++)
        {
            SetPos(blackPlayer[i]);
            SetPos(whitePlayer[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(piece, new Vector3(0, 0, -1), Quaternion.identity);
        Chess chess = obj.GetComponent<Chess>();
        chess.name = name;
        chess.SetBoardX(x);
        chess.SetBoardY(y);
        chess.Activate();
        return obj;
    }

    public void SetPos(GameObject obj) 
    {
        Chess chess = obj.GetComponent<Chess>();
        positions[chess.GetBoardX(), chess.GetBoardY()] = obj;
    }

    public void SetPosEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPos(int x, int y)
    {
        return positions[x, y];
    }

    public bool PosOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        else return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Multiplayer");
        }

        else if(gameOver == true && Input.GetKey(KeyCode.Escape))
        {
            gameOver = false;

            SceneManager.LoadScene(1);
        }
    }

    public void Winner(string winner)
    {
        gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = winner.ToUpper() + " WINS!";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<TextMeshProUGUI>().enabled = true;
    }
}


