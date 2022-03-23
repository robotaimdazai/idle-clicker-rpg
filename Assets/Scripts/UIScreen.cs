using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;


namespace GIKIT
{
    public class UIScreen : MonoBehaviour
    {

        [SerializeField] protected UIScreenType type;
        [SerializeField] TransitionPosition transitionFrom = TransitionPosition.Custom;
        [SerializeField] TransitionPosition transitionTo = TransitionPosition.Custom;
        [SerializeField] protected float inTransitionTime = 0.5f;
        [SerializeField] protected float outTransitionTime = 0.5f;

        [SerializeField] protected Ease inEase = Ease.Linear;
        [SerializeField] protected bool useOutEase = false;
        public bool UseOutEase { get { return useOutEase; } }
        [SerializeField] protected Ease outEase = Ease.OutExpo;

        public float InTransitionDuration { get { return inTransitionTime; } }
        public float OutTransitionTime { get { return outTransitionTime; } }

        public Ease InEase { get { return inEase; } }
        public Ease OutEase { get { return outEase; } }

        public UIScreenType Type { get { return type; } }
        protected RectTransform rectTransform;

        [SerializeField] protected UnityEvent OnShow;
        [SerializeField] protected UnityEvent OnClose;

        public Vector3 StartPosition { get { return startPosition; } }
        protected Vector3 startPosition = Vector3.zero;
        protected Vector3 endPosition = Vector3.zero;
        public Vector3 EndPosition { get { return endPosition; } }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        private void Start()
        {
            startPosition = UIManager.Instance.GetPosition(transitionFrom);
            endPosition = UIManager.Instance.GetPosition(transitionTo);

            if (transitionFrom == TransitionPosition.Custom)
            {
                startPosition = transform.localPosition;
            }
            if (transitionTo == TransitionPosition.Custom)
            {
                endPosition = transform.localPosition;
            }

            rectTransform.localPosition = startPosition;

            
        }

        public void Show()
        {
            
            if (OnShow!=null) { OnShow.Invoke(); }
            gameObject.SetActive(true);
            
        }


        public void Close()
        {
           
            if (OnClose != null) { OnClose.Invoke(); }
            
            
        }

     
    }

}
