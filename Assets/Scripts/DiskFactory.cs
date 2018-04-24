using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Done
//Pass
public class DiskFactory : MonoBehaviour {

    public GameObject DiskPrefab;
    public List<DiskProperties> NewDisk = new List<DiskProperties>();
    public List<DiskProperties> UsedDisk = new List<DiskProperties>();

    public void Awake()
    {
        Debug.Log("Factory start");
        DiskPrefab = GameObject.Instantiate(Resources.Load("Prefabs/disk", typeof(GameObject)),
            Vector3.zero, Quaternion.identity,null) as GameObject;
        DiskPrefab.SetActive(false);
    }
    public GameObject GetDisk(int round,ActionMode actionMode)
    {
        GameObject DiskObj = null;
        if(NewDisk.Count>0)
        {
            DiskObj = NewDisk[0].gameObject;
            NewDisk.Remove(NewDisk[0]);
        }
        else
        {
            DiskObj = GameObject.Instantiate<GameObject>(DiskPrefab, Vector3.zero, Quaternion.identity);
            DiskObj.AddComponent<DiskProperties>();
        }
        //Add properties
        float colorpicker= UnityEngine.Random.Range(0f, 3f);
        //Debug.Log(colorpicker);
        if(colorpicker<3)
        {
            //Debug.Log("yello");
            DiskObj.GetComponent<DiskProperties>().color = Color.yellow;
            DiskObj.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (colorpicker<2)
        {
            //Debug.Log("red");
            DiskObj.GetComponent<DiskProperties>().color = Color.red;
            DiskObj.GetComponent<Renderer>().material.color = Color.red;
        }
        if (colorpicker<1)
        {
            //Debug.Log("white");
            DiskObj.GetComponent<DiskProperties>().color = Color.white;
            DiskObj.GetComponent<Renderer>().material.color = Color.white;
        }
        //Debug.Log("------------");
        DiskObj.GetComponent<DiskProperties>().speed = 3.0f+round;
        DiskObj.GetComponent<DiskProperties>().direction = new Vector3(UnityEngine.Random.Range(-2f, 2f), 1, 0);
        //--
        UsedDisk.Add(DiskObj.GetComponent<DiskProperties>());
        if(actionMode==ActionMode.RIGIDBODY)
        {
            Debug.Log("Use Rigid body");
            DiskObj.AddComponent<Rigidbody>();
        }
        DiskObj.name = DiskObj.GetInstanceID().ToString();
        return DiskObj;
    }
    public void FreeDisk(GameObject DiskObj)
    {
        DiskProperties tmp = null;
        foreach (DiskProperties item in UsedDisk)
        {
            if (DiskObj.GetInstanceID() == item.gameObject.GetInstanceID())
                tmp = item;
        }
        if(tmp!=null)
        {
            tmp.gameObject.SetActive(false);
            NewDisk.Add(tmp);
            UsedDisk.Remove(tmp);
        }
    }
}
