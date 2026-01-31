using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRaycast : MonoBehaviour
{
    [SerializeField] GameObject characterScreen;
    [SerializeField] DialogueManager dialogueManager;

    bool inConversation;

    private NPC storedNPC;

    private void OnEnable()
    {
        DialogueManager.OnDialogueStarted += JoinConversation;
        DialogueManager.OnDialogueEnded += LeaveConversation;

        TransitionManager.OnTransitionEnded += OnTransitionEnded;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStarted -= JoinConversation;
        DialogueManager.OnDialogueEnded -= LeaveConversation;

        TransitionManager.OnTransitionEnded -= OnTransitionEnded;
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
                if (GameManager.Instance.State == GameState.InConversation) return;
                if (SocialBatteryManager.instance.CurrentSocialBattery == 0) return;

                storedNPC = hit.collider.GetComponent<NPC>();
                dialogueManager.CharacterImage.texture = storedNPC.npcImage;
                TransitionManager.instance.StartTranstion(true, true, GameState.InConversation);
            }
        } 
    }

    private void OnTransitionEnded()
    {
        if (storedNPC == null) return;

        dialogueManager.StartDialogue(storedNPC.dialogueAssetTree, storedNPC.StartPosition, storedNPC.npcName);

        storedNPC = null;
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
        dialogueManager.CharacterImage = null;
    }
}