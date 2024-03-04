#nullable enable

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace GameCore.Players.Inputs
{
    [RequireComponent(typeof(RectTransform))]
    public class TouchscreenInput : GarbageInputBase, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform touchInputZone = null!;
        [SerializeField] private Image knob = null!;
        [SerializeField] private float maxDragDistance = 3f;

        private Camera _camera = null!;
        private Vector2 _startPosition, _endPosition;
        private bool _isTouch;
        
        private void Awake()
        {
            if (touchInputZone == null)
                touchInputZone = GetComponent<RectTransform>()!;
            knob.EnsureNotNull("Knob not specified").enabled = false;
            _camera = Camera.main!;
        }

        public override bool IsTouch() => _isTouch;

        public override Vector2 StartTouchPosition() => _startPosition;
        
        public override Vector2 EndTouchPosition() => _endPosition;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isTouch = true;   
            knob.enabled = true;
            _startPosition = _camera.ScreenToWorldPoint(eventData.position);
            knob.transform.position = _startPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _endPosition = _camera.ScreenToWorldPoint(eventData.position);
            var raw = _endPosition - _startPosition;
            
            knob.transform.position = raw.magnitude > maxDragDistance 
                ? _startPosition + raw.normalized * maxDragDistance 
                : _endPosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isTouch = false;
            knob.enabled = false;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
        }
    }
}