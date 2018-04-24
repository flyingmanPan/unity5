using UnityEngine;
using System.Collections;
//Done
public enum SSActionEvents : int { Start, End }
public interface ISSActionCallback
{
    void SSActionEvent(
        SSAction source,
        SSActionEvents e = SSActionEvents.End,
        int n_param = 0,
        string s_param = null,
        Object obj_param = null);
}
