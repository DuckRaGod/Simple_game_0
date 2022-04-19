using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour,IHitable{
    public void Hit(Bullet bullet){
        bullet.Set_active(false);
        GameManager.Instance.Lose_life();
    }
}
