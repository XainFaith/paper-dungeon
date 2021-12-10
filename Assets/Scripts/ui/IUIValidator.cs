using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IUIValidator
{
    public bool Validate(VisualElement caller);
}
