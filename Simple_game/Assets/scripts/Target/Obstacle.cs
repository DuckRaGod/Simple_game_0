using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour,IHitable{
    public void Hit(Bullet bullet){
        bullet.Set_active(false);
        GameManager.Instance.Hit_obstacle();
        Destroy(transform.parent.gameObject);
    }
}