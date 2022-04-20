using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    //  If gameObject active!
    public bool Is_active;
    //  Timer length to de-active gameObject!
    public float death_time;
    //  Visual of bullet!
    public GameObject visual;
    Rigidbody2D rigidbody2d;
    Collider2D collider2d;
    //  Timer time of alive if reach deathTime de-activate gameObject!
    float alive_timer;
    //  Speed of bullet!
    float speed;

    void Awake(){
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    //  When Int
    public void Set_bullet(float _speed){
        speed = _speed;
        rigidbody2d.velocity = transform.right * speed;
    }

    void Update(){
        if(!Is_active) return;

        alive_timer += Time.deltaTime;
        if(alive_timer < death_time) return;
        Set_active(false);
        alive_timer = 0;
    }

    public void Set_active(bool set){
        if(!set) Set_bullet(0);
        alive_timer = 0;
        collider2d.enabled = set;
        visual.SetActive(set);
        Is_active = set;
    }

    public void Change_diraction(Quaternion rotation){
        transform.localRotation = rotation;
        rigidbody2d.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D coll){
        var hit = coll.GetComponent<IHitable>();
        if(hit == null) return;
        hit.Hit(this);
    }
}
