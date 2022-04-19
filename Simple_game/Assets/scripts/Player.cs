using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    //  rotating varibales
    public float speed;
    public float angle;
    float first_angle;
    float second_angle;
    float rot_z;
    bool left = true;

    //  shooting
    public GameObject bullet_prefab;
    public Transform bullet_holder;
    public Transform shoot_point;
    public float bullet_speed;
    List<Bullet> bullet_pool = new();

    void Awake(){
        first_angle = 90f - angle/2f;
        second_angle = 180 - first_angle;
    }

    void Update(){
        if(!GameManager.Instance.data.is_play) return;
        Rotate();

        if(Input.GetMouseButtonDown(0)){
            Shoot();
        }

        if(Input.GetKeyDown("space")){
            Shoot();
        }
    }

    void Rotate(){
        var _speed = Time.deltaTime * speed;
        
        if(transform.eulerAngles.z  <= first_angle){
            left = true;
        }else if(transform.eulerAngles.z >= second_angle){
            left = false;
        }

        if(left){
            rot_z += _speed;
        }
        else{
            rot_z -= _speed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rot_z);
    }

    void Shoot(){
        var bullet = Get_bullet();
        bullet.transform.SetPositionAndRotation(shoot_point.position , shoot_point.rotation);
        bullet.Set_active(true);
        bullet.Set_bullet(bullet_speed);
    }

    Bullet Get_bullet(){
        var amount = bullet_pool.Count;
        for (int i = 0; i < amount; i++){
            if(!bullet_pool[i].Is_active){
                return bullet_pool[i];
            }
        }
        return Create_bullet();
    }

    Bullet Create_bullet(){
        var bullet = Instantiate(bullet_prefab , bullet_holder);
        var bullet_script = bullet.GetComponent<Bullet>();
        bullet_script.Set_active(false);
        bullet_pool.Add(bullet_script);
        return bullet_script;
    }
}
