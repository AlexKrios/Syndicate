using UnityEngine;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace DefaultUtility.CanvasHelpers
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        [SerializeField]
        private Vector2 safeAreaAxis = new(1f, 0f);

        private RectTransform _safeAreaTransform;
        private ScreenOrientation _lastOrientation = ScreenOrientation.LandscapeLeft;
        private Vector2 _lastResolution = Vector2.zero;
        private Canvas _canvas;
        private Vector2 _inverseSafeAreaAxis;

        private void Awake()
        {
            _safeAreaTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();

            _lastOrientation = Screen.orientation;
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;

            _inverseSafeAreaAxis = new Vector2(safeAreaAxis.y, safeAreaAxis.x);

            ApplySafeArea();
        }

        private void Update()
        {
            if (Screen.orientation != _lastOrientation)
            {
                OrientationChanged();
            }

            if (Screen.width != _lastResolution.x || Screen.height != _lastResolution.y)
            {
                ResolutionChanged();
            }
        }

        private void ApplySafeArea()
        {
            if (_safeAreaTransform == null)
                return;

            var safeArea = Screen.safeArea;
            var pixelRect = _canvas.pixelRect;
            var anchorMin = Vector2.Scale(safeArea.position, safeAreaAxis);
            var anchorMax = Vector2.Scale(safeArea.size, safeAreaAxis) + Vector2.Scale(pixelRect.size, _inverseSafeAreaAxis);
            anchorMin.x /= pixelRect.width;
            anchorMin.y /= pixelRect.height;
            anchorMax.x /= pixelRect.width;
            anchorMax.y /= pixelRect.height;

            _safeAreaTransform.anchorMin = anchorMin;
            _safeAreaTransform.anchorMax = anchorMax;
        }

        private void OrientationChanged()
        {
            _lastOrientation = Screen.orientation;
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;

            ApplySafeArea();
        }

        private void ResolutionChanged()
        {
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;
        }
    }
}