﻿using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Label : WebComponent, IComponentInnerText, IComponentInnerHtml
{
    public new string Text => base.Text;

    public new string InnerHtml => base.InnerHtml;
}