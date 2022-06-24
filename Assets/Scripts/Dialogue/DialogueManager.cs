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
		if (dialogs.Count == 1){
            SaveManager.Instance.state.tutorialCompleted = true;
			FindObjectOfType<Clock>().clockSpeed = 1;

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

		FindObjectOfType<Clock>().AddTime(hoursToAdd);

		foreach(Button button in interactables){
			button.interactable = false;
		}

		Dialogue dialog = dialogs.Dequeue();
		string sentence = dialog.sentence;
		dialog.interactable.interactable = true;

		DateTime dialogTime = dialog.timeSet.dateTime;
		hoursToAdd = dialogTime.Hour - FindObjectOfType<Clock>().date.Hour;
		hoursToAdd += (dialogTime.Minute/60f) - (FindObjectOfType<Clock>().date.Minute/60f);
		Debug.Log(dialogTime.Hour + " - " + FindObjectOfType<Clock>().date.Hour + " = " + hoursToAdd);

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