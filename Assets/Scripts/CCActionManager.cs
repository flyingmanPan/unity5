using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
//Done
public class CCActionManager : MonoBehaviour, AdapterActionManager, ISSActionCallback
{
    public FirstSceneController sceneCtrl;
    public List<CCFlyAction> fly;
    public int NumOfDisk = 0;
    private List<SSAction> UsedDisk = new List<SSAction>();
    private List<SSAction> NewDisk = new List<SSAction>();
    // Use this for initialization
    public SSAction GetSSAction()
    {
        Debug.Log("cc get ss");
        SSAction act = null;
        if(NewDisk.Count>0)
        {
            act = NewDisk[0];
            NewDisk.Remove(act);
        }
        else
            act = ScriptableObject.Instantiate<CCFlyAction>(fly[0]);
        UsedDisk.Add(act);
        return act;
    }
    public void FreeSSAction(SSAction act)
    {
        SSAction tmp = null;
        foreach (var item in UsedDisk)
            if (act.GetInstanceID() == item.GetInstanceID())
                tmp = item;
        if(tmp!=null)
        {
            tmp.Reset();
            NewDisk.Add(tmp);
            UsedDisk.Remove(tmp);
        }
    }
    protected void Start()
    {
        sceneCtrl = (FirstSceneController)Director.GetDirector().CurrentSceneCtrl;
        sceneCtrl.actionManager = this;
        fly.Add(CCFlyAction.GetSSAction());
    }

    // Update is called once per frame
    public void StartGame(Queue<GameObject> DiskQueue)
    {
        Debug.Log("start game");
        foreach (var item in DiskQueue)
        {
            RunAction(item, GetSSAction(), this);
        }
    }
    
    public void SSActionEvent(SSAction source, 
        SSActionEvents e = global::SSActionEvents.End, 
        int n_param = 0, 
        string s_param = null, 
        UnityEngine.Object obj_param = null)
    {
        if(source is CCFlyAction)
        {
            NumOfDisk--;
            DiskFactory diskFactory = Singleton<DiskFactory>.Instance;
            diskFactory.FreeDisk(source.GameObj);
            FreeSSAction(source);
        }
        throw new NotImplementedException();
    }
    public int NumOfDiskf()
    {
        return NumOfDisk;
    }
    public void SetNumOfDiskf(int num)
    {
        NumOfDisk = num;
    }
    public void RunAction(GameObject gobj, SSAction ssa, ISSActionCallback callback)
    {
        ssa.GameObj = gobj;
        ssa.callback = callback;
        ssa.Trans = gobj.transform;
        UsedDisk.Add(ssa);
        ssa.Start();
    }
}
