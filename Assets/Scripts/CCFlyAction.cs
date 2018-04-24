using UnityEngine;
using System.Collections;
//Done
//Pass
public class CCFlyAction : SSAction
{
    float speed;
    float gforce;
    float time;
    Vector3 direction;
    Rigidbody rigidbody;
    // Use this for initialization
    public override void Start()
    {
        time = 0;
        gforce = 10;
        enable = true;
        speed = GameObj.GetComponent<DiskProperties>().speed;
        direction = GameObj.GetComponent<DiskProperties>().direction;
        rigidbody = GameObj.GetComponent<Rigidbody>();
        if(rigidbody)
        {
            rigidbody.AddForce(Vector3.down * 9.8f);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        //Add G force;

        if(GameObj.activeSelf)
        {
            if (rigidbody)
            {
                rigidbody.AddForce(Vector3.down * 9.8f);
            }
            time += Time.deltaTime;
            Trans.Translate(Vector3.down * gforce * time * Time.deltaTime);
            Trans.Translate(direction * speed * Time.deltaTime);
            if (this.Trans.position.y<-4)
            {
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this);
            }
        }
    }
    public static CCFlyAction GetSSAction()
    {
        return ScriptableObject.CreateInstance<CCFlyAction>();
    }
}
