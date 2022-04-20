using UnityEngine;

public class ChangeDiraction : MonoBehaviour,IHitable{
    public void Hit(Bullet bullet){
        bullet.Change_diraction(transform.rotation);
    }
}
