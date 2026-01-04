using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public static GameObject Create(Transform parent, IconType iconType, string text)
    {

        // 从Resources文件夹加载ChatBubble prefab
        GameObject chatBubblePrefab = Resources.Load<GameObject>("Prefabs/ChatBubble");
        
        if (chatBubblePrefab == null)
        {
            Debug.LogError("ChatBubble prefab not found! Please place your ChatBubble prefab in Assets/Resources/ folder and name it 'ChatBubblePrefab'");
            return null;
        }

        GameObject chatBubbleObject = Instantiate(chatBubblePrefab, parent);
        chatBubbleObject.transform.localPosition = new Vector3(0.25f, 1.5f, 0);
        
        ChatBubble chatBubble = chatBubbleObject.GetComponent<ChatBubble>();
        if (chatBubble != null)
        {
            chatBubble.Setup(iconType, text);
        }
        
        return chatBubbleObject;
    }

    public static GameObject CreateAtNPC(Transform npcTransform, IconType iconType, string text)
    {

        return Create(npcTransform, iconType, text);
    }

    public enum IconType
    {
        Happy,
        Neutral,
        Angry
    }

    [SerializeField] private Sprite happyIconSprite;
    [SerializeField] private Sprite neutralIconSprite;
    [SerializeField] private Sprite angryIconSprite;

    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;
    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Setup(IconType iconType, string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(1f, 0.5f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector2 offset = new Vector2(-1.65f, 0f);
        backgroundSpriteRenderer.transform.localPosition = new Vector2(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;
    
        iconSpriteRenderer.sprite = GetIconSprite(iconType);
    }

    private Sprite GetIconSprite(IconType iconType)
    {
        switch(iconType)
        {
            default:
            case IconType.Happy:    return happyIconSprite;
            case IconType.Neutral:  return neutralIconSprite;
            case IconType.Angry:    return angryIconSprite;
        }
    }
}
