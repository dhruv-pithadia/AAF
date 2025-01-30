
using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Ui
{
    public class LineGraph : MonoBehaviour
    {
        [SerializeField] private RectTransform dotPrefab;
        [SerializeField] private RectTransform connectionPrefab;
        [SerializeField] private Transform dotParent;
        [SerializeField] private Transform connectionParent;
        [SerializeField] private TMP_Text maxVerticalValueText;
        [SerializeField] private TMP_Text maxHorizontalValueText;
        [SerializeField] private TMP_Text verticalLabelText;

        private const float Offset = 16f;
        private Queue<RectTransform> _dotsPool;
        private RectTransform _graphParent;
        private float _height;
        private float _width;

        #region Unity Methods

        private void Awake()
        {
            _dotsPool = new Queue<RectTransform>();
            _graphParent = GetComponent<RectTransform>();
            _width = _graphParent.sizeDelta.x - Offset * 2f;
            _height = _graphParent.sizeDelta.y - Offset * 0.5f;
        }

        private void OnDisable()
        {
            DisposeGraph();
            _dotsPool.Clear();
            _dotsPool = null;
        }

        #endregion

        #region Public Methods

        public void CreateGraph(List<float> points, string playDuration, string label)
        {
            var count = points.Count;
            if (count < 2) return;

            ReuseDots();
            verticalLabelText.text = label;
            maxHorizontalValueText.text = playDuration;
            PlotPointsOnGraph(points, count);
        }

        #endregion

        #region Private Methods

        private void PlotPointsOnGraph(List<float> points, int count)
        {
            var max = int.MinValue;
            for (var i = 0; i < count; i++)
            {
                if (points[i] <= max) continue;
                max = Mathf.FloorToInt(points[i]);
            }

            max++;
            maxVerticalValueText.text = max.ToString();
            var xSize = _width / count;
            RectTransform previousPoint = null;
            for (var i = 0; i < count; i++)
            {
                var point = PlacePoint(new Vector2(i * xSize + Offset, points[i] / max * _height));
                if (count > 20) continue;
                
                if (ReferenceEquals(previousPoint, null) == false)
                {
                    CreateConnection(point.anchoredPosition, previousPoint.anchoredPosition);
                }

                previousPoint = point;
            }
        }

        private RectTransform PlacePoint(Vector2 anchoredPosition)
        {
            var dot = GetDot();
            dot.gameObject.SetActive(true);
            dot.sizeDelta = new Vector2(10f, 10f);
            dot.anchorMin = new Vector2(0f, 0f);
            dot.anchorMax = new Vector2(0f, 0f);
            dot.anchoredPosition = anchoredPosition;
            return dot;
        }

        private void DisposeGraph()
        {
            ReuseDots(false);
        }

        private RectTransform GetDot()
        {
            return _dotsPool.Count == 0 ? Instantiate(dotPrefab, dotParent) : _dotsPool.Dequeue();
        }

        private void ReuseDots(bool reuse = true)
        {
            maxVerticalValueText.text = "N/A";
            maxHorizontalValueText.text = "N/A";
            var graphChildren = dotParent.GetComponentsInChildren<RectTransform>();
            for (var i = 0; i < graphChildren.Length; i++)
            {
                if (ReferenceEquals(graphChildren[i], dotParent)) continue;
                ReturnDot(graphChildren[i], reuse);
            }
        }

        private void ReturnDot(RectTransform dot, bool reuse)
        {
            dot.gameObject.SetActive(false);
            if (reuse == false || _dotsPool.Count > 100) return;
            if (_dotsPool.Contains(dot)) return;
            _dotsPool.Enqueue(dot);
        }

        private void CreateConnection(Vector2 positionA, Vector2 positionB)
        {
            var result = Instantiate(connectionPrefab, connectionParent);
            Vector2 direction = (positionB - positionA).normalized;
            var distance = Vector2.Distance(positionA, positionB);
            result.sizeDelta = new Vector2(distance, 2f);
            result.anchorMin = new Vector2(0f, 0f);
            result.anchorMax = new Vector2(0f, 0f);
            result.anchoredPosition = positionA + direction * distance * 0.5f;
            var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (rotation < 0) rotation += 360f;
            result.localEulerAngles = new Vector3(0, 0, rotation);
        }

        #endregion
    }
}
