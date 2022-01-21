using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class StatDistributor : VisualElement
{

    #region Public Props

    /// <summary>
    /// The number of avalible points to spend on Stats
    /// </summary>
    public int StatPoints
    {
        get
        {
            return this.currentStatePoints;
        }
        set
        {
            this.currentStatePoints = value;
            this.pointsCounterLabel.text = value.ToString();
        }
    }

    /// <summary>
    /// Gets the strength stat
    /// </summary>
    public int Str
    {
        get
        {
            return this.strNumeric.Value;
        }
    }

    /// <summary>
    /// Gets the dexterty stat
    /// </summary>
    public int Dex
    {
        get
        {
            return this.dexNumeric.Value;
        }
    }

    /// <summary>
    /// Gets the constituion Stat
    /// </summary>
    public int Con
    {
        get
        {
            return this.conNumeric.Value;
        }
    }

    /// <summary>
    /// Gets the intelligence Stat
    /// </summary>
    public int Int
    {
        get
        {
            return this.intNumeric.Value;
        }
    }

    /// <summary>
    /// Gets the Wisdom Stat
    /// </summary>
    public int Wis
    {
        get
        {
            return this.wisNumeric.Value;
        }
    }

    /// <summary>
    /// Gets the charisma Stat
    /// </summary>
    public int Cha
    {
        get
        {
            return this.chaNumeric.Value;
        }
    }

    #endregion

    #region Private Fields

    private Label pointsLabel;
    private Label pointsCounterLabel;

    //Numeric controls for stats
    private NumericLeftRight strNumeric;
    private NumericLeftRight dexNumeric;
    private NumericLeftRight conNumeric;
    private NumericLeftRight intNumeric;
    private NumericLeftRight wisNumeric;
    private NumericLeftRight chaNumeric;

    private int currentStatePoints;

    #endregion

    public StatDistributor()
    {
        GroupBox labelContainer = new GroupBox();
        labelContainer.AddToClassList("stats-label-group");
        labelContainer.style.flexDirection = FlexDirection.Row;

        this.pointsLabel = new Label("Points:");
        this.pointsLabel.style.alignContent = Align.FlexStart;
        labelContainer.Add(this.pointsLabel);

        this.pointsCounterLabel = new Label("0");
        this.pointsCounterLabel.style.alignContent = Align.FlexEnd;
        labelContainer.Add(this.pointsCounterLabel);

        this.Add(labelContainer);

        this.strNumeric = this.instanceNumeric();
        this.strNumeric.LabelText = "Str:";
        this.dexNumeric = this.instanceNumeric();
        this.dexNumeric.LabelText = "Dex:";
        this.conNumeric = this.instanceNumeric();
        this.conNumeric.LabelText = "Con:";
        this.intNumeric = this.instanceNumeric();
        this.intNumeric.LabelText = "Int:";
        this.wisNumeric = this.instanceNumeric();
        this.wisNumeric.LabelText = "Wis:";
        this.chaNumeric = this.instanceNumeric();
        this.chaNumeric.LabelText = "Cha:";
    }

    public void RollStatePoints()
    {
        this.Reset();
        this.StatPoints = Random.Range(10, 16);
    }

    private void Reset()
    {
        this.strNumeric.Value = 12;
        this.dexNumeric.Value = 12;
        this.conNumeric.Value = 12;
        this.wisNumeric.Value = 12;
        this.intNumeric.Value = 12;
        this.chaNumeric.Value = 12;
    }

    /// <summary>
    /// Helper function for instancing Numeric controls with preset values
    /// </summary>
    /// <returns></returns>
    private NumericLeftRight instanceNumeric()
    {
        NumericLeftRight con = new NumericLeftRight();
        con.MinValue = 12;
        con.MaxValue = 20;
        con.ValueStepSize = 1;
        con.Increment = this.canIncrementStat;
        con.Decrement = this.canDecrementStat;
        this.Add(con);
        return con;
    }

    //Called by a numeric control to validate its change, used to track point usage and cancel a numeric change if no points are avaliable
    private bool canIncrementStat()
    {
        if(this.StatPoints > 0)
        {
            this.StatPoints--;
            return true;
        }

        return false;
    }

    //Called by a numeric control to validate its change, used to track point usage, reutrns points to the pool
    private bool canDecrementStat()
    {
        this.StatPoints++;
        return true;
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<StatDistributor, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription stat_points =
            new UxmlIntAttributeDescription { name = "Points", defaultValue = 10 };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get
            {
                yield break;
            }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as StatDistributor;


            ate.StatPoints = stat_points.GetValueFromBag(bag, cc);
        }
    }

    #endregion
}
