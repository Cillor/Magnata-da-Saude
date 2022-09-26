using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue[] dialogue;

	private void Start() {
		if(PlayerPrefs.GetInt("playTutorial") == 0){
			SaveManager.Instance.state.tutorialCompleted = true;
		}

		if(!SaveManager.Instance.state.tutorialCompleted){
			TriggerDialogue();
        }
	}

	public void TriggerDialogue (){
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}