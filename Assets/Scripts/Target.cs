using UnityEngine;

public class Target : MonoBehaviour
{
    // NPC控制
    [SerializeField] private NPC targetNPC; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            if (targetNPC != null && targetNPC.gameObject != null)
            {
                targetNPC.SwitchToEffectIcon();
            }
        }

        if (collision.CompareTag("Food"))
        {
            if (targetNPC != null)
            {
                targetNPC.SetChatBubbleSettings(ChatBubble.IconType.Angry, "I need plates!");
                targetNPC.SwitchToChatBubble();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            if (targetNPC != null && targetNPC.gameObject != null)
            {
                targetNPC.SwitchToChatBubble();
            }
        }
    }

    public void SetTargetNPC(NPC npc)
    {
        targetNPC = npc;
    }

    public NPC GetTargetNPC()
    {
        return targetNPC;
    }
}