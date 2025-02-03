
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
        private const int MaxConnections = 64;
        private Queue<RectTransform> _dotPool;
        private Queue<RectTransform> _dotConnectionPool;
        private RectTransform _graphParent;
        private float _height;
        private float _width;

        #region Unity Methods

        private void Awake()
        {
            _dotPool = new Queue<RectTransform>();
            _dotConnectionPool = new Queue<RectTransform>();
            _graphParent = GetComponent<RectTransform>();
            _width = _graphParent.sizeDelta.x - Offset * 2f;
            _height = _graphParent.sizeDelta.y - Offset * 0.5f;
        }

        private void OnDisable()
        {
            DisposeGraph();
            _dotConnectionPool.Clear();
            _dotConnectionPool = null;
            _dotPool.Clear();
            _dotPool = null;
        }

        #endregion

        #region Public Methods

        public void CreateGraph(List<float> points, string playDuration, string label)
        {
            ReuseDots();
            var count = points.Count;
            if (count < 2)
            {
                ReuseConnections();
                return;
            }
            
            verticalLabelText.text = label;
            maxHorizontalValueText.text = playDuration;
            PlotPointsOnGraph(points, count);
        }

        #endregion

        #region Private Methods

        private void PlotPointsOnGraph(List<float> points, int count)
        {
            if (count <= MaxConnections) ReuseConnections();
            
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
                if (count > MaxConnections) continue;
                
                if (ReferenceEquals(previousPoint, null) == false)
                {
                    PlaceConnection(point.anchoredPosition, previousPoint.anchoredPosition);
                }

                previousPoint = point;
            }
        }

        private void DisposeGraph()
        {
            ReuseConnections();
            ReuseDots();
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
        
        private void PlaceConnection(Vector2 positionA, Vector2 positionB)
        {
            var connection = GetDotConnection();
            
            Vector2 direction = (positionB - positionA).normalized;
            var distance = Vector2.Distance(positionA, positionB);
            connection.sizeDelta = new Vector2(distance, 2f);
            connection.anchorMin = new Vector2(0f, 0f);
            connection.anchorMax = new Vector2(0f, 0f);
            connection.anchoredPosition = positionA + direction * distance * 0.5f;
            var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (rotation < 0) rotation += 360f;
            connection.localEulerAngles = new Vector3(0, 0, rotation);
            connection.gameObject.SetActive(true);
        }

        private RectTransform GetDot()
        {
            return _dotPool.Count == 0 ? Instantiate(dotPrefab, dotParent) : _dotPool.Dequeue();
        }

        private RectTransform GetDotConnection()
        {
            return _dotConnectionPool.Count == 0
                ? Instantiate(connectionPrefab, connectionParent)
                : _dotConnectionPool.Dequeue();
        }

        private void ReuseDots()
        {
            maxVerticalValueText.text = "N/A";
            maxHorizontalValueText.text = "N/A";
            var graphChildren = dotParent.GetComponentsInChildren<RectTransform>();
            for (var i = 0; i < graphChildren.Length; i++)
            {
                if (ReferenceEquals(graphChildren[i], dotParent)) continue;
                ReturnDot(graphChildren[i]);
            }
        }

        public void ReuseConnections()
        {
            var graphChildren = connectionParent.GetComponentsInChildren<RectTransform>();
            for (var i = 0; i < graphChildren.Length; i++)
            {
                if (ReferenceEquals(graphChildren[i], connectionParent)) continue;
                ReturnDotConnection(graphChildren[i]);
            }
        }

        private void ReturnDot(RectTransform dot)
        {
            dot.gameObject.SetActive(false);
            if (_dotPool.Count > 100) return;
            if (_dotPool.Contains(dot)) return;
            _dotPool.Enqueue(dot);
        }

        private void ReturnDotConnection(RectTransform connection)
        {
            connection.gameObject.SetActive(false);
            if (_dotConnectionPool.Count > 100) return;
            if (_dotConnectionPool.Contains(connection)) return;
            _dotConnectionPool.Enqueue(connection);
        }
        
        #endregion
    }
}
