using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Done
public class ScoreController : MonoBehaviour
{
    public int score;
    private Dictionary<Color, int> scoreCounter = new Dictionary<Color, int>();

    // Use this for initialization
    void Start()
    {
        Reset();
        scoreCounter.Add(Color.yellow, 3);
        scoreCounter.Add(Color.white, 1);
        scoreCounter.Add(Color.red, 5);
    }
    public void AddScore(GameObject DiskShooted)
    {
        score += scoreCounter[DiskShooted.GetComponent<DiskProperties>().color];
    }
    private void Reset()
    {
        score = 0;
    }
}
