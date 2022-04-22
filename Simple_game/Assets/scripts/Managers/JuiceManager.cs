using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceManager : MonoBehaviour{
    public Data data;

    public AudioSource audioSourceEffect;
    public AudioSource pewAudioScoure;

    public ParticleSystem HitEffect;
    public Transform HitEffectTransform;

    public AudioClip PlayRetrayClip;

    public AudioClip HitTargetEffectClip;
    public AudioClip HitPowerupEffectClip;
    public AudioClip HitObstecleEffectClip;

    public Transform cam;

    IEnumerator timerPause;
    IEnumerator shake;


    public static JuiceManager Instance { get; private set; }

    void Awake(){
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
    }

    IEnumerator PauseForSeconds(float timeToPause){
        data.isPause = true;
        yield return new WaitForSeconds(timeToPause);
        data.isPause = false;
    }

    public void PlayerShoot(){

        var rnd = Random.Range(0.75f,1.85f);
        ShakeCam(.06f,.26f);
        pewAudioScoure.pitch = (float)System.Math.Round(rnd,3); 
        pewAudioScoure.Play();
    }

    public void PlayerDie(){
        StopCoroutine(timerPause);
    }

    public void HitObstacle(){
        PlaySound(HitObstecleEffectClip);
        ShakeCam(0.1f,1);
        timerPause = PauseForSeconds(.15f);
        StartCoroutine(timerPause);
    }

    public void HitTarget(Transform hit_point){
        HitEffectTransform.position = hit_point.position;
        PlaySound(HitTargetEffectClip);
        HitEffect.Play();
        ShakeCam(0.1f,0.7f);
        timerPause = PauseForSeconds(.05f);
        StartCoroutine(timerPause);
    }

    public void HitPowerup(){
        PlaySound(HitPowerupEffectClip);
    }

    public void SpawnedPowerup(){
        Debug.Log("Spawned Target!");
    }

    public void Enter(){
        PlaySound(PlayRetrayClip);
    }

    void PlaySound(AudioClip clip){
        var rnd = Random.Range(0.75f,1.85f);
        audioSourceEffect.pitch = (float)System.Math.Round(rnd,3); 
        audioSourceEffect.clip = clip;
        audioSourceEffect.Play();
    }

    void ShakeCam(float duration , float magnitude){
        if(shake != null)
            StopCoroutine(shake);
        shake = Shake(duration,magnitude,Random.Range(-999f,999f));
        StartCoroutine(shake);
    }

    IEnumerator Shake(float duration , float magnitude , float seed){
        var elapsed = 0f;
        while (elapsed < duration){
            var y = Mathf.PerlinNoise(0,elapsed * magnitude + seed);
            var x = Mathf.PerlinNoise(elapsed * magnitude + seed,0);

            cam.transform.localPosition = new Vector3(x,y,-10);

            elapsed += Time.deltaTime;
            yield return null;
        }
        while(cam.transform.position != Vector3.zero){
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition , new Vector3(0,0,-10) ,Time.deltaTime * 10);
            yield return null;
        }
    }
}