using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{ 
    [SerializeField] private ChatBubble.IconType iconType = ChatBubble.IconType.Happy;
    [SerializeField] [TextArea(2, 4)] private string message = "Hello there!";
    private GameObject currentChatBubble;

    // EffectIcon相关
    [SerializeField] private GameObject effectIcon;
    [SerializeField] private bool showEffectIcon = false;


    // 方向动画控制
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    private Animator animator;
    public Direction direction = Direction.Down;
    
    private void Start()
    {
        ShowChatBubble();
        // 方向动画
        animator = GetComponent<Animator>();
        PlayIdleAnimation();
        
    }
    void PlayIdleAnimation()
    {
        switch(direction)
        {
            case Direction.Left:
                animator.Play("IdleLeft");
                break;
            case Direction.Right:
                animator.Play("IdleRight");
                break;
            case Direction.Up:
                animator.Play("IdleUp");
                break;
            case Direction.Down:
                animator.Play("IdleDown");
                break;
        }
    }

    // 更新显示状态的核心方法
    private void UpdateDisplay()
    {
        if (showEffectIcon)
        {
            ShowEffectIcon();
            HideChatBubble();
        }
        else
        {
            ShowChatBubble();
            HideEffectIcon();
        }
    }

    public void ShowEffectIcon() { effectIcon?.SetActive(true); }
    public void HideEffectIcon() { effectIcon?.SetActive(false); }
    public void HideChatBubble()
    {
        if (currentChatBubble != null)
        {
            Destroy(currentChatBubble);
            currentChatBubble = null;
        }
    }
    public void SwitchToChatBubble()
    {
        showEffectIcon = false;
        UpdateDisplay();
    }
    public void ShowChatBubble()     
    {         
        ShowChatBubble(iconType, message);     
    }
    public void SwitchToEffectIcon()
    {
        showEffectIcon = true;
        UpdateDisplay();
    }
    public void ShowChatBubble(ChatBubble.IconType customIconType, string customMessage)
    {
        if (currentChatBubble != null)
        {
            Destroy(currentChatBubble);
        }
        currentChatBubble = ChatBubble.CreateAtNPC(transform, customIconType, customMessage);
        
        // 自动销毁
        if (currentChatBubble != null)
        {
             // Destroy(currentChatBubble, 3f);
        }
    }
    
    // 公共方法：让其他脚本可以改变NPC的消息和图标
    public void SetChatBubbleSettings(ChatBubble.IconType newIconType, string newMessage)
    {
        iconType = newIconType;
        message = newMessage;
    }
}
