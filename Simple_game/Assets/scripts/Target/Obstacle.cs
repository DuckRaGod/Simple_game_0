using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour,IHitable{
    public void Hit(Bullet bullet){
        bullet.Set_active(false);
        JuiceManager.Instance.HitObstacle();
        GameManager.Instance.Hit_obstacle(transform.parent.parent.gameObject);
    }
}
