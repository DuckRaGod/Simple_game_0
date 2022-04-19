using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    Rigidbody2D rigidbody2d;
    public bool Is_active;
    public GameObject visual;
    float alive_timer;
    public float death_time;
    Collider2D collider2d;

    void Awake(){
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    public void Set_bullet(float speed){
        rigidbody2d.velocity = transform.right * speed;
    }

    void Update(){
        if(!Is_active) return;
        alive_timer += Time.deltaTime;
        if(alive_timer >= death_time){
            Set_active(false);
            alive_timer = 0;
        }
    }

    public void Set_active(bool set){
        if(!set) Set_bullet(0);
        alive_timer = 0;
        collider2d.enabled = set;
        visual.SetActive(set);
        Is_active = set;
    }

    void OnTriggerEnter2D(Collider2D coll){
        var hit = coll.GetComponent<IHitable>();
        if(hit == null) return;

        hit.Hit(this);
    }
}
