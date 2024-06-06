﻿using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Button : Component, IComponentClick, IComponentDisabled, IComponentValue
{
    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;

    public new void Click() => base.Click();
}