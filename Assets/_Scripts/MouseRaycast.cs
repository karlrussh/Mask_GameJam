using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRaycast : MonoBehaviour
{
    [SerializeField] GameObject characterScreen;
    [SerializeField] DialogueManager dialogueManager;

    bool inConversation;

    private void OnEnable()
    {
        DialogueManager.OnDialogueStarted += JoinConversation;
        DialogueManager.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStarted -= JoinConversation;
        DialogueManager.OnDialogueEnded -= LeaveConversation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();    
        }
    }

    private void Interact()
    {
        if (inConversation)
        {
            DialogueManager.instance.SkipLine();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit object: " + hit.collider.name);
            Debug.Log("Hit point: " + hit.point);
            
            if (hit.collider.CompareTag("Character"))
            {
                var npc = hit.collider.GetComponent<NPC>();                    
                dialogueManager.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName);
            }
        } 
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }
}