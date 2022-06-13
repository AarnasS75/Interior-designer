using HSVPicker;
using UnityEngine;

namespace HSVPickerExamples
{
    public class ColorPickerTester : MonoBehaviour
    {
        [HideInInspector]
        public Renderer renderer;

        public ColorPicker picker;
        public Color Color;
        bool SetColorOnStart = false;

        void Start()
        {
            picker.onValueChanged.AddListener(color =>
            {
                renderer.material.color = color;
                Color = color;
            });

            if (SetColorOnStart)
            {
                picker.CurrentColor = Color;
            }
        }
    }
}