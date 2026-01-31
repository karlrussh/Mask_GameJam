using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject answerBox;
    [SerializeField] Button[] answerObjects;
    public RawImage CharacterImage; 

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    
    bool skipLineTriggered;
    bool answerTriggered;
    int answerIndex;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public void StartDialogue(DialogueTree dialogueTree, int startSection, string name)
    {
        ResetBox();
        nameText.text = name + "...";
        dialoguePanel.SetActive(true);
        CharacterImage.gameObject.SetActive(true);
        OnDialogueStarted?.Invoke();
        StartCoroutine(RunDialogue(dialogueTree, startSection));
    }

    IEnumerator RunDialogue(DialogueTree dialogueTree, int section)
    {
        for (int i = 0; i < dialogueTree.sections[section].dialogue.Length; i++)
        {
            dialogueText.text = dialogueTree.sections[section].dialogue[i];
            while (skipLineTriggered == false)
            {
                yield return null;
            }
            skipLineTriggered = false;
        }

        if (dialogueTree.sections[section].endAfterDialogue)
        {
            OnDialogueEnded?.Invoke();
            dialoguePanel.SetActive(false);
            CharacterImage.gameObject.SetActive(false);
            yield break;
        }

        dialogueText.text = dialogueTree.sections[section].branchPoint.question;
        ShowAnswers(dialogueTree.sections[section].branchPoint);

        while (answerTriggered == false)
        {
            yield return null;
        }
        //answerBox.SetActive(false);
        answerTriggered = false;

        StartCoroutine(RunDialogue(dialogueTree, dialogueTree.sections[section].branchPoint.answers[answerIndex].nextElement));
    }

    void ResetBox()
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(false);
        CharacterImage.gameObject.SetActive(false);
        //answerBox.SetActive(false);
        skipLineTriggered = false;
        answerTriggered = false;
    }

    void ShowAnswers(BranchPoint branchPoint)
    {
        // Reveals the aselectable answers and sets their text values
        //answerBox.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            if (i < branchPoint.answers.Length)
            {
                answerObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = branchPoint.answers[i].answerLabel;
                answerObjects[i].gameObject.SetActive(true);
            }
            else {
                answerObjects[i].gameObject.SetActive(false);
            }
        }
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void AnswerQuestion(int answer)
    {
        answerIndex = answer;
        answerTriggered = true;
    }
}
