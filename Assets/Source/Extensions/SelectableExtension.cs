using System;
using Lean.Gui;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source
{
    public static class SelectableExtension
    {
        public static UnityEvent OnClick(this Selectable selectable)
        {
            return selectable switch
            {
                LeanButton leanButton => leanButton.OnClick,
                Button button         => button.onClick,
                _ => throw new NotImplementedException()
            };
        }
    }
}