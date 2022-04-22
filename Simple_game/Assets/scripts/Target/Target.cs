using UnityEngine;

public class Target : MonoBehaviour{
    public float speed;
    public float switch_diraction_time; // every x seconds switch diraction of rotation if x <= 0 then isnt working

    float time;

    void Awake(){
        var rnd = Random.Range(speed - 5f , speed + 5f);
        var pn = Random.Range(0,2)*2-1;     // -1 or 1
        speed =  rnd * pn;
    }

    void Update(){
        if(!GameManager.Instance.data.is_play || GameManager.Instance.data.isPause) return;
        
        if(switch_diraction_time > 0){
            time += Time.deltaTime;
            if(time >= switch_diraction_time){
                time = 0;
                speed = -speed;
            }
        }
        
        Rotate(speed);
    }

    void Rotate(float _speed){
        var pos_z = Time.deltaTime * _speed;
        transform.rotation *= Quaternion.Euler(0,0,pos_z);
    }
}