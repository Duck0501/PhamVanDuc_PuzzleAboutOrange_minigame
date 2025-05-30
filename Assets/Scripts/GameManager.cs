using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] oranges;
    public GameObject[] blocks;
    public Text timeText;

    private int[,] grid = new int[4, 4];
    private Dictionary<int, GameObject> orangeDict = new Dictionary<int, GameObject>();
    private Dictionary<GameObject, Vector2Int> orangePositions = new Dictionary<GameObject, Vector2Int>();

    private const float cellSize = 1.555f;
    private const float gridOffset = 2.334f;
    private Vector2 mouseStartPos;
    private bool isMouseDown;
    private float timeRemaining = 45f; 
    private bool gameEnded = false;

    private void Start()
    {
        InitGrid();
    }

    private void Update()
    {
        if (gameEnded) return; 

        timeRemaining -= Time.deltaTime;
        timeText.text = "00 : " + Mathf.CeilToInt(timeRemaining).ToString();
        if (timeRemaining <= 0)
        {
            gameEnded = true;
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Lose");     
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
            isMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0) && isMouseDown)
        {
            Vector2 mouseEndPos = Input.mousePosition;
            Vector2 swipeDirection = mouseEndPos - mouseStartPos;

            if (swipeDirection.magnitude > 50f)
            {
                float absX = Mathf.Abs(swipeDirection.x);
                float absY = Mathf.Abs(swipeDirection.y);

                if (absX > absY)
                {
                    if (swipeDirection.x > 0)
                        MoveAll(Vector2Int.right);
                    else
                        MoveAll(Vector2Int.left);
                }
                else
                {
                    if (swipeDirection.y > 0)
                        MoveAll(Vector2Int.up);
                    else
                        MoveAll(Vector2Int.down);
                }
            }
            isMouseDown = false;
        }

        if (IsWin())
        {
            gameEnded = true;
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Win"); 
        }
    }

    void InitGrid()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                grid[x, y] = 0;

        foreach (var block in blocks)
        {
            Vector2Int pos = WorldToGrid(block.transform.position);
            if (IsInsideGrid(pos))
                grid[pos.x, pos.y] = -1;
            else
                Debug.LogWarning("Block object out of bounds: " + block.name);
        }

        for (int i = 0; i < oranges.Length; i++)
        {
            GameObject orange = oranges[i];
            Vector2Int pos = WorldToGrid(orange.transform.position);
            if (IsInsideGrid(pos))
            {
                orangePositions[orange] = pos;
                orangeDict[i + 1] = orange;
                grid[pos.x, pos.y] = i + 1;
            }
            else
            {
                Debug.LogWarning("Orange object out of bounds: " + orange.name);
            }
        }
    }

    void MoveAll(Vector2Int direction)
    {
        var sortedOranges = orangePositions.OrderBy(kvp 
            => direction == Vector2Int.up ? -kvp.Value.y :
                                                         
            direction == Vector2Int.down ? kvp.Value.y :
                                                        
            direction == Vector2Int.left ? kvp.Value.x :
                                                        
            -kvp.Value.x).ToList();

        foreach (var kvp in sortedOranges)
        {
            GameObject orange = kvp.Key;
            Vector2Int currentPos = kvp.Value;
            Vector2Int newPos = currentPos + direction;

            if (IsInsideGrid(newPos) && grid[newPos.x, newPos.y] == 0)
            {
                int id = GetOrangeId(orange);
                grid[currentPos.x, currentPos.y] = 0;
                grid[newPos.x, newPos.y] = id;
                orangePositions[orange] = newPos;
                orange.transform.position += new Vector3(direction.x * cellSize, direction.y * cellSize, 0);
            }
        }
    }

    int GetOrangeId(GameObject orange)
    {
        foreach (var kvp in orangeDict)
        {
            if (kvp.Value == orange) return kvp.Key;
        }
        return 0;
    }

    bool IsInsideGrid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < 4 && pos.y >= 0 && pos.y < 4;
    }

    bool IsWin()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (grid[x, y] == 1 &&
                    grid[x + 1, y] == 2 &&
                    grid[x, y + 1] == 3 &&
                    grid[x + 1, y + 1] == 4)
                    return true;
            }
        }
        return false;
    }

    Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt((worldPos.x + gridOffset) / cellSize);
        int y = Mathf.RoundToInt((worldPos.y + gridOffset) / cellSize);
        return new Vector2Int(x, y);
    }
}