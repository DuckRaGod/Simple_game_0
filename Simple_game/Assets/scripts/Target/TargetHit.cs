using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour,IHitable{
    public void Hit(Bullet bullet){
        bullet.Set_active(false);
        JuiceManager.Instance.HitTarget(transform);
        GameManager.Instance.Hit_target(transform.parent.parent.gameObject);
    }
}
