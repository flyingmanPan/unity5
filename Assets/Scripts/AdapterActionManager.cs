using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
//Done
public interface AdapterActionManager
{
    SSAction GetSSAction();
    void FreeSSAction(SSAction act);
    void StartGame(Queue<GameObject> DiskQueue);
    int NumOfDiskf();
    void SetNumOfDiskf(int num);
}
