using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLife : MonoBehaviour,IHitable{
    public Data data;
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
        data.life ++;
        GameManager.Instance.ui_manager.Update_ui();
        gameObject.SetActive(false);
        JuiceManager.Instance.HitPowerup();
    }
}
