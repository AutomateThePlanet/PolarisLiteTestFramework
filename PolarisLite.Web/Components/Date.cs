using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public class Date : ComponentAdapter, IComponentDisabled, IComponentValue
{
    public virtual string GetDate()
    {
        return GetAttribute("value");
    }

    public virtual void SetDate(int year, int month, int day)
    {
        if (year <= 0)
        {
            throw new ArgumentException($"The year should be a positive number but you specified: {year}");
        }

        if (month <= 0 || month > 12)
        {
            throw new ArgumentException($"The month should be between 0 and 12 but you specified: {month}");
        }

        if (day <= 0 || day > 31)
        {
            throw new ArgumentException($"The day should be between 0 and 31 but you specified: {day}");
        }

        string valueToBeSet = month < 10 ? $"{year}-0{month}" : $"{year}-{month}";
        valueToBeSet = day < 10 ? $"{valueToBeSet}-0{day}" : $"{valueToBeSet}-{day}";
        SetAttribute("value", valueToBeSet);
    }

    // TODO: add properties for step, min, max

    public virtual bool? IsAutoComplete => GetAttribute("autocomplete") == "on";

    public virtual bool IsReadonly => !string.IsNullOrEmpty(GetAttribute("readonly"));

    public virtual bool IsRequired => !string.IsNullOrEmpty(GetAttribute("required"));

    public virtual string Placeholder => string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");

    public new bool IsDisabled => base.IsDisabled;

    public new string Value => base.Value;
}