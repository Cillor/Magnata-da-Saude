using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    SaveState state;

    public string finalMessage;
    public static GameController Instance { set; get; }

    private void Awake(){

        if (Instance == null)
            Instance = this;
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        state = SaveManager.Instance.state;
    }

    // Update is called once per frame
    void Update(){
        if(state.energy <= 0){
            finalMessage  = "NÃO CUIDAR DA SUA ENERGIA MENTAL É UM CAMINHO PERIGOSO DE SE TRILHAR EM UMA JORNADA COMO A SUA";
            SceneManager.LoadScene("EndGameStats");
        }
                
        if(state.hoursSinceLastSlept > (24*5)){
            finalMessage  = "A FALTA DE SONO REGULAR É UM SÉRIO PROBLEMA DE SAÚDE QUE PODE CAUSAR A MORTE EM CASOS EXTREMOS";
            SceneManager.LoadScene("EndGameStats");
        }

        if(state.bmi <= 25){
            finalMessage  = "FOI UMA JORNADA MUITO LONGA, MAS VOCÊ SE MOSTROU PERSEVERANTE E ALCANÇOU SEU OBJETIVO DE ESTABILIZAR SUA SAÚDE";
            SceneManager.LoadScene("EndGameStats");
            
            //show all stats in this screen
        }
    }
}
