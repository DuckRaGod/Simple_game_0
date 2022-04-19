using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour{
    public float speed;
    public int points;

    void Awake(){
        transform.GetChild(0).GetComponent<TargetHit>().point = points;
    }

    void Update(){
        if(!GameManager.Instance.data.is_play) return;
        var pos_z = Time.deltaTime * speed;
        transform.rotation *= Quaternion.Euler(0,0,pos_z);
    }
}
