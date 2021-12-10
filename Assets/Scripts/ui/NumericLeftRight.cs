using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class NumericLeftRight : VisualElement
{


    public string LabelText
    {
        get
        {
            return this.controlLabel.text;
        }
        set
        {
            this.controlLabel.text = value;
        }
    }

    public int MaxValue
    {
        get
        {
            return this.maxValue;
        }
        set
        {
            this.maxValue = value;
            if (this.currentValue > value)
            {
                this.Value = value;
            }
        }
    }

    public int MinValue
    {
        get
        {
            return this.minValue;
        }
        set
        {
            this.minValue = value;
            if(this.currentValue < value)
            {
                this.Value = value;
                Debug.Log(this.currentValue);
            }
        }
    }

    public int ValueStepSize
    {
        get;
        set;
    }

    public Button subButton
    {
        get;
        set;
    }

    public Button addButton
    {
        get;
        set;
    }

    public int Value
    {
        get { return this.currentValue; }
        set 
        {
            this.currentValue = value;
            this.valueLabel.text = value.ToString();
        }
    }

    private Label controlLabel;
    private Label valueLabel;

    private int currentValue;
    private int minValue;
    private int maxValue;

    public NumericLeftRight()
    {
        this.style.flexDirection = FlexDirection.Row;

        this.controlLabel = new Label("Label");
        this.Add(this.controlLabel);

        this.subButton = new Button();
        this.subButton.text = string.Empty;
        this.subButton.AddToClassList("numeric-sub");
        this.Add(this.subButton);

        this.subButton.clicked += SubButton_clicked;

        this.valueLabel = new Label(this.currentValue.ToString());
        this.valueLabel.AddToClassList("numeric-value-label");
        this.valueLabel.text = this.currentValue.ToString();
        this.Add(this.valueLabel);

        this.addButton = new Button();
        this.addButton.text = string.Empty;
        this.addButton.AddToClassList("numeric-add");
        this.Add(this.addButton);

        this.addButton.clicked += AddButton_clicked;
    }

    private void AddButton_clicked()
    {
        if(this.currentValue < this.MaxValue)
        {
            this.currentValue+= this.ValueStepSize;
            this.valueLabel.text = this.currentValue.ToString();
            if(this.currentValue > this.MaxValue)
            {
                this.currentValue = this.MaxValue;
            }
        }
    }

    private void SubButton_clicked()
    {
        if (this.currentValue > this.MinValue)
        {
            this.currentValue -= this.ValueStepSize;
            this.valueLabel.text = this.currentValue.ToString();

            if (this.currentValue < this.MinValue)
            {
                this.currentValue = this.MinValue;
            }
        }
    }


    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<NumericLeftRight, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription control_label_string =
            new UxmlStringAttributeDescription { name = "Control Label", defaultValue = "default_text" };
        
        UxmlIntAttributeDescription min_Int = 
            new UxmlIntAttributeDescription { name = "Min value", defaultValue = 0 };

        UxmlIntAttributeDescription max_Int =
            new UxmlIntAttributeDescription { name = "Max Value", defaultValue = 100 };

        UxmlIntAttributeDescription step_Int =
            new UxmlIntAttributeDescription { name = "Step", defaultValue = 1 };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get {
                yield break; 
            }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as NumericLeftRight;

            ate.controlLabel.text = control_label_string.GetValueFromBag(bag, cc);
            ate.MinValue = min_Int.GetValueFromBag(bag, cc);
            ate.MaxValue = max_Int.GetValueFromBag(bag, cc);
            ate.ValueStepSize = step_Int.GetValueFromBag(bag, cc);
        }
    }


    #endregion
}
