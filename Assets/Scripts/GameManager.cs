using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource click;
    public AudioSource clear;
    public GameObject awning;
    public Text turnText;
    public Text resultDescription;
    [Range(1, 2)]
    public bool gameOver = false;
    public int turn = 1;
    public int round = 0;
    public Sprite[] boxs = new Sprite[2];
    public int[,] board;
    private int[,,] winLines;
    const int winCount = 8;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        board = new int[3, 3]{
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };
        winLines = new int[winCount, 3, 2]{
            {{0, 0}, {1, 0}, {2, 0}},
            {{0, 1}, {1, 1}, {2, 1}},
            {{0, 2}, {1, 2}, {2, 2}},
            {{0, 0}, {0, 1}, {0, 2}},
            {{1, 0}, {1, 1}, {1, 2}},
            {{2, 0}, {2, 1}, {2, 2}},
            {{0, 0}, {1, 1}, {2, 2}},
            {{2, 0}, {1, 1}, {0, 2}}
        };
        awning.SetActive(false);
    }

    public void Update()
    {
        if(gameOver) return;
        turnText.text = $"현재 턴 : <color={GetTurnColor(turn)}>{GetTurnName(turn)}</color>";
        int winner = CheckWinner();
        if(winner != -1)
        {
            gameOver = true;
            clear.Play();
            awning.SetActive(true);
            resultDescription.text = $"이겼어요! 승리자는 <color={GetTurnColor(winner)}>{GetTurnName(winner)}</color>에요.";
        }else if(round > 8)
        {
            gameOver = true;
            awning.SetActive(true);
            resultDescription.text = "비겼어요! 승리자는 없어요.";
        }
    }

    public void OnReplayClick()
    {
        awning.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    private int CheckWinner()
    {
        for(int i = 0; i < winCount; i++)
        {
            var (x1, y1) = (winLines[i, 0, 0], winLines[i, 0, 1]);
            var (x2, y2) = (winLines[i, 1, 0], winLines[i, 1, 1]);
            var (x3, y3) = (winLines[i, 2, 0], winLines[i, 2, 1]);
            if(board[y1, x1] != 0 && board[y1, x1] == board[y2, x2] && board[y1, x1] == board[y3, x3]) return board[y1, x1];
        }
        return -1;
    }

    private string GetTurnName(int turn)
    {
        return turn == 1 ? "O" : "X";
    }

    private string GetTurnColor(int turn)
    {
        return turn == 1 ? "#4641e3" : "#be362e";
    }
}
