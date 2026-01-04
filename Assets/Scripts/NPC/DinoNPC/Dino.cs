using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dino : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    private DialogueController dialogueUI;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private GameObject interactionIcon;

    private void Start()
    {
        dialogueUI = DialogueController.Instance;

        // 找到交互图标子对象
        interactionIcon = transform.Find("InteractionIcon")?.gameObject;
        
        // 初始状态下交互图标
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(true);
        }
    }
    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        // 如果游戏暂停且没有对话正在进行，或者根本没有对话内容，就不执行交互逻辑。
        // If no dialogue data or the game is paused and no dialogue is active
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        // 对话开始时隐藏交互图标
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(false);
        }

        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueUI.ShowDialogueUI(true);

        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            // Skip typing animation and show the full line
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            // If another line, type next line
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            
            if (NPCVoiceController.Instance != null)
            {
                NPCVoiceController.Instance.PlayVoice(dialogueData.voiceSound, dialogueData.voicePitch);
            }
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        // 判断当前这行对话是否应该自动播放，如果是，就等待一段时间再继续。
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        PauseController.SetPause(false);

        // 对话结束后重新显示交互图标
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(true);
        }
    }
}
