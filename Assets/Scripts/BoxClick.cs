using UnityEngine;

public class BoxClick : MonoBehaviour
{
    [Range(0, 2)]
    public int[] position = new int[2];

    public void OnMouseDown()
    {
        if(GameManager.instance.gameOver) return;
        if(GameManager.instance.board[position[1], position[0]] != 0) return;
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.boxs[GameManager.instance.turn - 1];
        GameManager.instance.board[position[1], position[0]] = GameManager.instance.turn;
        GameManager.instance.turn = GameManager.instance.turn == 2 ? 1 : 2;
        GameManager.instance.round++;
        GameManager.instance.click.Play();
    }
}
