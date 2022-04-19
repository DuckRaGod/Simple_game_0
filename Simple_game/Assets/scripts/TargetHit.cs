using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour,IHitable{
    public int point;

    public void Hit(Bullet bullet){
        bullet.Set_active(false);
        GameManager.Instance.Get_point(point);
    }
}
