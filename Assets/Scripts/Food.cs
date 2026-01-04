using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Food Settings")]
    public GameObject merged; 
    
    [Header("Food Type")]
    public FoodType foodType = FoodType.Raw; 
    
    [Header("Spice Settings - Only for Raw Food")]
    public Sprite spicedSprite; 

    [Header("UI References")]
    [SerializeField] private List<Vector2> spicePositions = new List<Vector2>(); 
    [System.Serializable]
    public class SpiceSliderMapping
    {
        public UnityEngine.UI.Slider progressSlider;
        public List<int> spicePositionIndices = new List<int>();
    }
    [SerializeField] private List<SpiceSliderMapping> sliderMappings = new List<SpiceSliderMapping>();

    [Header("Detection Settings")]
    public LayerMask detectLayer;
    

    private bool hasSpice = false;
    private bool isBeingSpiced = false;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    
 
    public enum FoodType
    {
        Raw,        // 生食 - 需要调料才能装盘
        Fresh      // 新鲜食物 - 直接可以装盘（如水果、沙拉）
    }
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    private void Update()
    {
        if (foodType == FoodType.Raw && !hasSpice)
        {
            for (int i = 0; i < spicePositions.Count; i++)
            {
                Vector2 spicePos = spicePositions[i];
                if (Vector2.Distance(transform.position, spicePos) < 0.1f)
                {
                    UnityEngine.UI.Slider targetSlider = GetSliderForSpicePosition(i);
                    StartSpicing(targetSlider);
                    break;
                }
            }
        }
    }

    private UnityEngine.UI.Slider GetSliderForSpicePosition(int spicePositionIndex)
    {
        foreach (SpiceSliderMapping mapping in sliderMappings)
        {
            if (mapping.spicePositionIndices.Contains(spicePositionIndex))
            {
                return mapping.progressSlider;
            }
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plate"))
        {
            if (CanBePlated())
            {
                GameObject obj = Instantiate(merged, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
    
    private bool CanBePlated()
    {
        switch (foodType)
        {
            case FoodType.Raw:
                return hasSpice; // 生食必须加调料
            case FoodType.Fresh:
                return true; 
            default: return false;
        }
    }

    private void StartSpicing(UnityEngine.UI.Slider slider)
    {
        if (foodType == FoodType.Raw && !hasSpice && !isBeingSpiced)
        {
            isBeingSpiced = true;
            SoundEffectManager.Play("SauceSquirt", false);
            StartCoroutine(CompleteSpicing(slider));
        }
    }

    private System.Collections.IEnumerator CompleteSpicing(UnityEngine.UI.Slider slider)
    {
        float elapsedTime = 0f;
        float duration = 0.8f; 
        if (slider != null)
        {
            slider.gameObject.SetActive(true); 
            slider.value = 0f; 
        }
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            if (slider != null)
            {
                slider.value = progress;
            }
            yield return null; 
        }
        if (slider != null)
        {
            slider.value = 1f;
        }
        yield return new WaitForSeconds(0.2f);
        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }
        if (spicedSprite != null)
        {
            spriteRenderer.sprite = spicedSprite;
        }
        hasSpice = true;
        isBeingSpiced = false;
    }

    // private void AddSpice()
    // {
    //     if (foodType == FoodType.Raw && !hasSpice)
    //     {
    //         hasSpice = true;
    //         if (spicedSprite != null)
    //         {
    //             spriteRenderer.sprite = spicedSprite;
    //         }
    //         // 这里可以添加调料效果：粒子、音效等
    //     }
    // }
    public bool CanMoveToDir(Vector2 dir)
    {
        if (isBeingSpiced) return false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 0.5f, detectLayer);
        if (!hit || (hit.collider != null && hit.collider.CompareTag("Plate")) || (hit.collider != null && hit.collider.CompareTag("Target")))
        {
            transform.Translate(dir);
            return true;
        }
        return false;
    }
}
