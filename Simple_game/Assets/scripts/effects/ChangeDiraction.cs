using UnityEngine;

public class ChangeDiraction : MonoBehaviour,IHitable{
    public float timer_alive;
    float time_alive;

    void Update(){
        time_alive += Time.deltaTime;
        if(time_alive>=timer_alive){
            time_alive = 0;
            gameObject.SetActive(false);
        }
    }

    public void Hit(Bullet bullet){
        time_alive = 0;
        bullet.Change_diraction(transform.rotation , transform.position);
        gameObject.SetActive(false);
        JuiceManager.Instance.HitPowerup();
    }
}
