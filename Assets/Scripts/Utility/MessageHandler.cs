using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageHandler : MonoBehaviour
{
    public TMP_Text endGameMessage;
    // Update is called once per frame
    void Update(){
        endGameMessage.text = GameController.Instance.finalMessage;
        
    }
}
