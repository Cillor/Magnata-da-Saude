using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	private void Start() {
		if(!SaveManager.Instance.state.tutorialCompleted){
			TriggerDialogue();
        }
	}

	public void TriggerDialogue (){
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}