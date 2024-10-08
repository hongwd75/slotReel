using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameUISystem
{
    public class CanvasOption : MonoBehaviour
    {
        #region === Component 설정 ===
#if UNITY_EDITOR
        [ContextMenu("캔버스 객체 적용")]
        public void attachObjects()
        {
            canvas = transform.GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = transform.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                
            }
            canvasScaler = transform.GetComponent<CanvasScaler>();
            if(canvasScaler == null)
            {
                canvasScaler = transform.AddComponent<CanvasScaler>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = new Vector2(1080, 1920);
            }
            rectTransform = transform as RectTransform;
            graphicRaycaster = transform.GetComponent<GraphicRaycaster>();
            if (graphicRaycaster == null)
            {
                graphicRaycaster = transform.AddComponent<GraphicRaycaster>();
            }
            canvasGroup = transform.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = transform.AddComponent<CanvasGroup>();
            }
        }
#endif
        #endregion
        
        [SerializeField] private CanvasType uitype = CanvasType.none;
        [SerializeField] private Canvas canvas = null;
        [SerializeField] private CanvasScaler canvasScaler = null;
        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private RectTransform rectTransform = null;
        [SerializeField] private GraphicRaycaster graphicRaycaster = null;

        private void Start()
        {
            if (canvas != null)
            {
                canvas.sortingOrder = uitype.ToOrderBaseValue();
            }
#if UNITY_EDITOR            
            else
            {
                Debug.LogError("[오류] CanvasOption 값 설정이 되어 있지 않습니다. 오른쪽 클릭 후,[캔버스 객체 적용] 항목을 눌러 자동 설정해주세요!");          
            }
#endif            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}