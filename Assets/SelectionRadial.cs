using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using HoloToolkit.Unity;

namespace VRStandardAssets.Utils
{
   
    public class SelectionRadial : Singleton<SelectionRadial>
    {
        public event Action OnSelectionComplete;   
        
        [SerializeField] private float m_SelectionDuration = 2f;                               
        [SerializeField] private bool m_HideOnStart = true;                                    
        [SerializeField] private Image m_Selection;

        private Coroutine m_SelectionFillRoutine;                                              
        private bool m_IsSelectionRadialActive;                                                   
        private bool m_RadialFilled;                                                             

        public float SelectionDuration { get { return m_SelectionDuration; } }

        private void Start()
        {
            m_Selection.fillAmount = 0f;
            if (m_HideOnStart)
            {
                Hide();
            }
        }

        public void Show()
        {
            m_Selection.gameObject.SetActive(true);
            m_IsSelectionRadialActive = true;
        }


        public void Hide()
        {
            m_Selection.gameObject.SetActive(false);
            m_IsSelectionRadialActive = false;

            m_Selection.fillAmount = 0f;            
        }


        private IEnumerator FillSelectionRadial()
        {
            m_RadialFilled = false;
            
            float timer = 0f;
            m_Selection.fillAmount = 0f;
            
            while (timer < m_SelectionDuration)
            {
                m_Selection.fillAmount = timer / m_SelectionDuration;
                
                timer += Time.deltaTime;
                yield return null;
            }

            m_Selection.fillAmount = 1f;
           
            m_IsSelectionRadialActive = false;

            m_RadialFilled = true;

            if (OnSelectionComplete != null)
            {
                OnSelectionComplete();
            }
        }


        public IEnumerator WaitForSelectionRadialToFill ()
        {
            m_RadialFilled = false;
            Show ();
            while (!m_RadialFilled)
            {
                yield return null;
            }
            Hide ();
        }

        public void HandleUp()
        {
            if (m_IsSelectionRadialActive)
            {
                m_Selection.fillAmount = 0f;

                if (m_SelectionFillRoutine != null)
                {
                    StopCoroutine(m_SelectionFillRoutine);
                }
            }
        }

        public void HandleDown()
        {
            if (m_IsSelectionRadialActive)
            {
                m_SelectionFillRoutine = StartCoroutine(FillSelectionRadial());
            }
        }
    }
}