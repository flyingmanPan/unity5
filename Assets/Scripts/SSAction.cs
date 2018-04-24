using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Done
public class SSAction : ScriptableObject
{
    public bool enable = false;
    public bool destroy = false;
    public GameObject GameObj { get; set; }
    public Transform Trans { get; set; }
    public ISSActionCallback callback { get; set; }
    protected SSAction() { }

    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
    public void Reset()
    {
        enable = false;
        destroy = false;
        GameObj = null;
        Trans = null;
        callback = null;
    }
}
