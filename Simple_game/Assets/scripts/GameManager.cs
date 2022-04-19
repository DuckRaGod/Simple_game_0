using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public int p_y; 
    public int x;
    public int n_y;

    public Transform target;

    public Data data;
    public UiManager ui_manager;

    public GameObject ui_canves;
    public GameObject main_canves;

    public static GameManager Instance { get; private set; }

    void Awake(){
        data.is_play = false;
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
    }
    
    void Update(){
        if(data.is_play) return;
        if(Input.anyKeyDown){
            StartCoroutine(Set_play());   
            ui_canves.SetActive(true);
            main_canves.SetActive(false);
        }
    }

    IEnumerator Set_play(){
        yield return new WaitForSeconds(0.1f);
        data.is_play = true;
    }

    public void Place_target(){
        target.position = new Vector2(Random.Range(-x,x),Random.Range(n_y,p_y));
    }

    public void Get_point(int point){
        Place_target();
        data.score += point;
        ui_manager.Update_ui();
    }

    public void Lose_life(){
        data.life--;
        ui_manager.Update_ui();
    }
}
