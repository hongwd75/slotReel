using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 릴에 붙은 슬롯 개념
namespace GameSystem.Reel
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image slot;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Text textIndex;
        private const float maxBounceHeight = 50f;
        private ReelItemData itemInfo;

        public float Height => _rectTransform.rect.height;
        
        // 초기화 함수
        public void Init(ReelItemData item,int index)
        {
            itemInfo = item;
            textIndex.text = index.ToString();
            SetRealImage(0);
        }
        
        // 이미지 설정
        public void SetRealImage(int t)
        {
            if (t == 0)
            {
                slot.sprite = itemInfo.NormalSprite;
            }
            else
            {
                slot.sprite = itemInfo.activieSprite;
            }
        }

        public void SetYposition(float y)
        {
            _rectTransform.anchoredPosition = new Vector2(0f, y);
        }

        public float GetYposition()
        {
            return _rectTransform.anchoredPosition.y;
        }
    }
}