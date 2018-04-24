using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Done
//Pass
public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> AddWait = new List<SSAction>();
    private List<int> DelWait = new List<int>();

    // Use this for initialization
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        foreach (var ac in AddWait)
            actions[ac.GetInstanceID()] = ac;

        AddWait.Clear();
        foreach (KeyValuePair<int,SSAction> item in actions)
        {
            var ac = item.Value;

            if (ac.destroy)
                DelWait.Add(ac.GetInstanceID());
            else if (ac.enable)
                ac.Update();
        }
        foreach (var key in DelWait)
        {
            var ac = actions[key];
            actions.Remove(ac.GetInstanceID());
            DestroyObject(ac);
        }
        DelWait.Clear();
    }
    public void RunAction(GameObject gobj,SSAction ssa,ISSActionCallback callback)
    {
        ssa.GameObj = gobj;
        ssa.callback = callback;
        ssa.Trans = gobj.transform;
        AddWait.Add(ssa);
        ssa.Start();
    }
}
