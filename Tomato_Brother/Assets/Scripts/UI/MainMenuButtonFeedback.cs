using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TomatoBrother.UI
{
    /// <summary>
    /// 主菜单按钮 PC 交互：悬浮 1.05 提亮、按下 0.95（与策划文档 3.2.1 一致）。
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class MainMenuButtonFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Image targetGraphic;

        Color _normalColor;
        Color _hoverColor;
        Color _pressedColor;

        RectTransform _rt;
        Vector3 _baseScale;
        Button _button;

        void Awake()
        {
            _rt = (RectTransform)transform;
            _baseScale = _rt.localScale;
            _button = GetComponent<Button>();
            if (targetGraphic == null)
                targetGraphic = GetComponent<Image>();
            if (targetGraphic != null)
            {
                _normalColor = targetGraphic.color;
                _hoverColor = Brighten(_normalColor, 1.06f);
                _pressedColor = Brighten(_normalColor, 0.9f);
            }
        }

        static Color Brighten(Color c, float m)
        {
            return new Color(
                Mathf.Clamp01(c.r * m),
                Mathf.Clamp01(c.g * m),
                Mathf.Clamp01(c.b * m),
                c.a);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_button != null && !_button.interactable)
                return;
            _rt.localScale = _baseScale * 1.05f;
            if (targetGraphic != null)
                targetGraphic.color = _hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rt.localScale = _baseScale;
            if (targetGraphic != null)
                targetGraphic.color = _normalColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_button != null && !_button.interactable)
                return;
            _rt.localScale = _baseScale * 0.95f;
            if (targetGraphic != null)
                targetGraphic.color = _pressedColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_button != null && !_button.interactable)
            {
                _rt.localScale = _baseScale;
                if (targetGraphic != null)
                    targetGraphic.color = _normalColor;
                return;
            }

            _rt.localScale = _baseScale * 1.05f;
            if (targetGraphic != null)
                targetGraphic.color = _hoverColor;
        }
    }
}
