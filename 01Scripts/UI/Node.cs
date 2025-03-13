using System;
using UnityEngine;

namespace LJS.UI
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private Node _parent;
        [SerializeField] private UILineRenderer _lineRenderer;
        
        private void OnValidate()
        {
            if(_lineRenderer == null) return;
            
            if (_parent == null)
            {
                _lineRenderer.gameObject.SetActive(false);
                return;
            }
            
            _lineRenderer.gameObject.SetActive(true);

            RectTransform rectTrm = transform as RectTransform;
            Vector3 startPos = new Vector3(rectTrm.sizeDelta.x * 0.5f, rectTrm.sizeDelta.y);
            Vector3 relativePos = transform.InverseTransformPoint(_parent.transform.position);
            Vector3 delta = relativePos - new Vector3(0, rectTrm.sizeDelta.y);
            Vector3 endPos = startPos + delta;
            Vector3 middlePos = delta * 0.5f + startPos;
            
            _lineRenderer.points = new Vector2[4]
            {
                startPos,
                new Vector2(startPos.x, middlePos.y),
                new Vector2(endPos.x, middlePos.y),
                endPos
            };
        }
    }
}
