using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sons : MonoBehaviour
{
    public AudioSource[] musicas;
    public AudioSource[] sons;
    private int iAu, iAb;
    private bool changeSong = true;
    //private int evento = "";
    //private AudioSource musicaAtual;

    void Start()
    {
        iAu = 3; 
        //musicaAtual = musicas[0];
    }

    void Update()
    {              
        musicas[0].volume = (musicas[0].volume >= 0.3f) ? 0.3f : musicas[0].volume;     
        musicas[1].volume = (musicas[1].volume >= 0.5f) ? 0.5f : musicas[1].volume;      
        musicas[3].volume = (musicas[3].volume >= 0.5f) ? 0.5f : musicas[3].volume; 
        musicas[4].volume = (musicas[4].volume >= 0.5f) ? 0.5f : musicas[4].volume;       
        
        if( changeSong && musicas[iAu].isPlaying && musicas[iAu].clip.length - musicas[iAu].time <= 2){   
            iAb = iAu; 
            iAu = 0;
            StartCoroutine("AbaixarVolume");
            StartCoroutine("AumentarVolume");
            changeSong = false;
        }
        if(musicas[2].isPlaying && musicas[2].clip.length - musicas[2].time <= 1){
            musicas[3].Play();
            iAu = 3;
            StartCoroutine("AumentarVolume");
        }
        //print(Random.Range(1, 4));
        //print(musicaAtual.clip.length - musicaAtual.time);
        if (musicas[0].clip.length - musicas[0].time <= 0.2f){
            musicas[0].Play();
        }
    }
    public void PlaySound(string som){
        switch (som){
            case "hit":
                sons[0].Play();
                break;
            case "pulo":
                sons[Random.Range(1, 4)].Play();
                break;
            case "broto":
                sons[5].Play();
                break;
            case "pá":
                NextMusic(1);
                break;  
            case "rosa":
                NextMusic(4);
                break;
        }
    }
    IEnumerator AbaixarVolume(){
        for(int i = 0; i < 500; i++){
            yield return new WaitForSeconds(0.01f);
            musicas[iAb].volume -= 0.002f;
        }
    }
    IEnumerator AumentarVolume(){
        print(iAu);
        for(int i = 0; i < 500; i++){
            yield return new WaitForSeconds(0.01f);
            musicas[iAu].volume += 0.002f;
        }
        changeSong = true;
    }
    void NextMusic(int i){
        StopCoroutine("AumentarVolume");                
        StopCoroutine("AbaixarVolume");
        if(musicas[1].isPlaying){iAb = 1;}
        else if(musicas[3].isPlaying){iAb = 3;}
        else if(musicas[4].isPlaying){iAb = 4;}        
        musicas[i].Play();
        musicas[i].volume = 0.1f;
        iAu = i;
        StartCoroutine("AumentarVolume");
        StartCoroutine("AbaixarVolume");
    }
    
}
