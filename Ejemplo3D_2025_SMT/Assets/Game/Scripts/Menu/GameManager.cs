using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public static GameManage Instance;

    private float globalTime = 0f;
    private int score = 0;
    private int itemsCount = 0;

    public float GlobalTime { get => globalTime; set => globalTime = value; }
    public int Score { get => score; set => score = value; }
    public int ItemsCount { get => itemsCount; set => itemsCount = value; }



    public void AddTime(float timeScene)
    {
        globalTime += timeScene;
    }

    public void AddScore(int scoreItem)
    {
        score += scoreItem;
    }

}