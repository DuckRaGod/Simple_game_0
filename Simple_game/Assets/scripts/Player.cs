using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    //  Rotating speed!
    public float speed;
    //  Angle of change diraction!
    public float angle;

    float first_angle;
    float second_angle;
    float rot_z;
    bool left = true;

    public GameObject bullet_prefab;
    //  Transform where to instantiate bullets!
    public Transform bullet_holder;
    public Transform shoot_point;
    public float bullet_speed;
    List<Bullet> bullet_pool = new();

    void Awake(){
        first_angle = angle ;
        second_angle = angle + 2* (90-angle);
    }
    
    void Rotate(){
        var pos = Time.deltaTime * speed;

        //  Change diraction if reached or over reached needed angle!
        if(transform.eulerAngles.z  <= first_angle){
            left = true;
        }else if(transform.eulerAngles.z >= second_angle){
            left = false;
        }

        //  Rotating in diraction
        if(left){
            rot_z += pos;
        }
        else{
            rot_z -= pos;
        }

        transform.rotation = Quaternion.Euler(0, 0, rot_z);
    }

    Bullet Get_bullet(){
        var amount = bullet_pool.Count;
        //  Check in list for inactive bullet if find return the bullet!
        for (int i = 0; i < amount; i++){
            if(!bullet_pool[i].Is_active){
                return bullet_pool[i];
            }
        }
        //  If dosent find any inactive bullet creates new bullet and return the new bullet!
        return Create_bullet();
    }

    void Shoot(){
        var bullet = Get_bullet();
        bullet.transform.SetPositionAndRotation(shoot_point.position , shoot_point.rotation);
        bullet.Set_active(true);
        bullet.Set_bullet(bullet_speed);
    }
    
    Bullet Create_bullet(){
        var bullet = Instantiate(bullet_prefab , bullet_holder);
        var bullet_script = bullet.GetComponent<Bullet>();
        bullet_script.Set_active(false);
        bullet_pool.Add(bullet_script);
        return bullet_script;
    }

    void Update(){
        if(!GameManager.Instance.data.is_play || GameManager.Instance.data.isPause) return;

        Rotate();
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")){
            Shoot();
            JuiceManager.Instance.PlayerShoot();
        }
    }
}
