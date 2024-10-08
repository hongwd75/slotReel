using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Reel
{
    public class Reel : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        private float scrollSpeed = 3000f; // 스크롤 속도 (픽셀/초)
        private float stopDuration = 0.5f; // 정지하는데 걸리는 시간
        
        private List<Slot> ReelSlots;
        private float TotalHeight = 0f;
        private float respawnPosition = 0f;
        
        // 릴 설정
        public void SetReel(List<ReelItemData> data)
        {
            ReelSlots = new List<Slot>();
            
            // 크기 계산
            float itemyposition = 0f + (_rectTransform.rect.height * _rectTransform.pivot.y);
            float itemsize = 0f;
            TotalHeight = _rectTransform.rect.height;
            
            var slotPrefab = Resources.Load<GameObject>("Reel/ReelSlot");

            for (int i = 0; i < data.Count; i++)
            {
                GameObject slotInstance = Instantiate(slotPrefab, _rectTransform);
                // Slot 컴포넌트에 접근할 수 있음
                Slot slotScript = slotInstance.GetComponent<Slot>();
                if (slotScript != null)
                {
                    slotScript.Init(data[i],i+1);
                    ReelSlots.Add(slotScript);
                    slotScript.SetYposition(itemyposition-slotScript.Height * 0.5f);
                    itemyposition -= slotScript.Height;
                    itemsize += slotScript.Height;
                }
            }

            respawnPosition = itemsize;// - TotalHeight;
        }


        // 릴 회전
        public void OnSpin()
        {
            StartCoroutine(SpinCoroutine());
        }

        private IEnumerator SpinCoroutine()
        {
            float spinDuration = UnityEngine.Random.Range(3f, 5f); // 2~5초 사이의 랜덤한 시간
            float elapsedTime = 0f;

            while (elapsedTime < spinDuration)
            {
                // 아래로 스크롤
                float deltaY = scrollSpeed * Time.deltaTime;
                MoveReelDown(deltaY);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 부드럽게 정지
            yield return StartCoroutine(SmoothStop());
        }

        private void MoveReelDown(float deltaY)
        {
            foreach (Slot slot in ReelSlots)
            {
                float halfheight = slot.Height * 0.5f;
                float newy = slot.GetYposition() - deltaY;
                
                if (newy < -(TotalHeight * 0.5f + halfheight))
                {
                    newy += respawnPosition;
                }

                slot.SetYposition(newy);
            }
        }

        private IEnumerator SmoothStop()
        {
            float elapsedTime = 0f;
            float initialSpeed = scrollSpeed;

            while (elapsedTime < stopDuration)
            {
                float t = elapsedTime / stopDuration;
                float currentSpeed = Mathf.Lerp(initialSpeed, 0, t);

                float deltaY = currentSpeed * Time.deltaTime;
                MoveReelDown(deltaY);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 최종 위치 조정 (가장 가까운 슬롯에 맞춤)
            AdjustFinalPosition();
        }

        private void AdjustFinalPosition()
        {
            float slotHeight = ReelSlots[0].Height;
            float halfTotalHeight = TotalHeight * 0.5f;

            // 가장 위에 있는 슬롯 찾기
            Slot topSlot = null;
            foreach (Slot slot in ReelSlots)
            {
                float pos = slot.GetYposition() - slot.Height * 0.5f;
                if (pos < halfTotalHeight && pos > -halfTotalHeight)
                {
                    if (topSlot == null)
                    {
                        topSlot = slot;
                    }
                    else if(pos > topSlot.GetYposition())
                    {
                        topSlot = slot;
                        slotHeight = slot.Height;
                    }
                }
            }

            // 조정값 계산
            float adjustment = halfTotalHeight - topSlot.GetYposition() - slotHeight * 0.5f;

            // 모든 슬롯 조정
            foreach (Slot slot in ReelSlots)
            {
                float newY = slot.GetYposition() + adjustment;
                slot.SetYposition(newY);
            }
        }
    }
}