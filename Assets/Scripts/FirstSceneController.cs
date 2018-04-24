using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
//Almost
public class FirstSceneController : MonoBehaviour, ISceneController, IUserAction
{
    public AdapterActionManager actionManager { get; set; }
    public ScoreController scoreRecorder { get; set; }
    public ActionMode mode { get; set; }
    public Queue<GameObject> diskQueue = new Queue<GameObject>();
    private int diskNumber;
    private int currentRound = -1;
    public int round = 3;
    private float time = 0;
    private GameStatus gameState = GameStatus.Start;
    void Awake()
    {
        Director director = Director.GetDirector();
        director.CurrentSceneCtrl = this;
        diskNumber = 10;
        this.gameObject.AddComponent<ScoreController>();
        this.gameObject.AddComponent<DiskFactory>();
        scoreRecorder = Singleton<ScoreController>.Instance;
        director.CurrentSceneCtrl.LoadResource();
        mode = ActionMode.DEFAULT;
    }

    private void Update()
    {
        
        if (actionManager != null && mode != ActionMode.DEFAULT)
        {
            //Debug.Log("First Update");
            if (actionManager.NumOfDiskf() == 0 && gameState == GameStatus.Running)
                gameState = GameStatus.RoundEnd;
            if (actionManager.NumOfDiskf() == 0 && gameState == GameStatus.RoundBegin)
            {
                //Debug.Log("Start in fsc");
                currentRound = (currentRound + 1) % round;
                NextRound();
                actionManager.SetNumOfDiskf(10);
                gameState = GameStatus.Running;
            }
            if (time > 1)
            {
                ThrowDisk();
                time = 0;
            }
            else
                time += Time.deltaTime;
        }
    }

    private void NextRound()
    {
        DiskFactory df = Singleton<DiskFactory>.Instance;
        for (int i = 0; i < diskNumber; i++)
            diskQueue.Enqueue(df.GetDisk(currentRound,mode));
        actionManager.StartGame(diskQueue);
    }

    void ThrowDisk()
    {
        if (diskQueue.Count != 0)
        {
            GameObject disk = diskQueue.Dequeue();
            Vector3 position = new Vector3(0, 0, 0);
            float y = UnityEngine.Random.Range(0f, 4f);
            position = new Vector3(-disk.GetComponent<DiskProperties>().direction.x * 7, y, 0);
            disk.transform.position = position;
            disk.SetActive(true);
        }

    }
    public void LoadResource()
    {
        // = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/greenplane"));
        GameObject GreenPlane = GameObject.Instantiate(Resources.Load("Prefabs/greenplane", typeof(GameObject)),
            Vector3.zero, Quaternion.identity, null) as GameObject;
    }
    public int GetScore()
    {
        return scoreRecorder.score;
    }
    public GameStatus GetGameStatus()
    {
        return gameState;
    }
    public void SetGameStatus(GameStatus gs)
    {
        Debug.Log(gs.ToString());
        Debug.Log(mode.ToString());
        gameState = gs;
    }
    public void Hit(Vector3 dpos)
    {
        Ray ray = Camera.main.ScreenPointToRay(dpos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<DiskProperties>() != null)
            {
                scoreRecorder.AddScore(hit.collider.gameObject);
                hit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
            }
        }
    }
    public ActionMode GetMode()
    {
        return mode;
    }

    public void SetMode(ActionMode m)
    {

        if (m == ActionMode.SELF)
        {
            this.gameObject.AddComponent<CCActionManager>();
        }
        else
        {
            this.gameObject.AddComponent<RigidActionManager>();
        }
        mode = m;
    }
}

