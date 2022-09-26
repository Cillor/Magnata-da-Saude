using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public TMP_Text dialogueText;
    public GameObject dialogueGO;
	public Button[] interactables;


	private Queue<Dialogue> dialogs;

	void Start () {
		dialogs = new Queue<Dialogue>();
	}

	public void StartDialogue (Dialogue[] dialogue){
        dialogueGO.SetActive(true);
		dialogs.Clear();
			
		foreach (Dialogue dialog in dialogue){
			dialogs.Enqueue(dialog);
		}

		DisplayNextSentence();
	}

	float hoursToAdd = 0;
	public void DisplayNextSentence (){
		Timer.Clock clock = FindObjectOfType<Timer.Clock>();

		if (dialogs.Count == 1){
            SaveManager.Instance.state.tutorialCompleted = true;

			foreach(Button button in interactables){
				button.interactable = true;
			}
        }

		if (dialogs.Count == 0){
            dialogueGO.SetActive(false);
			foreach(Button button in interactables){
				button.interactable = true;
			}
			return;
        }


		foreach(Button button in interactables){
			button.interactable = false;
		}

		Dialogue dialog = dialogs.Dequeue();
		string sentence = dialog.sentence;
		dialog.interactable.interactable = true;

		clock.SetHours(hoursToAdd);
		DateTime dialogTime = dialog.timeSet.dateTime;
		hoursToAdd = dialogTime.Hour + (dialogTime.Minute/60f);
		//Debug.Log(dialogTime.Hour + " - " + clock.Date.Hour + " = " + hoursToAdd);

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence){
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()){
			dialogueText.text += letter;
			yield return null;
		}
	}

}