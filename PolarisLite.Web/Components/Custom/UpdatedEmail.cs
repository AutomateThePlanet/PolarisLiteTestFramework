using PolarisLite.Web.Components;
using PolarisLite.Web.Contracts;

namespace PolarisLite.Web;

public partial class UpdatedEmail : Email
{
    public override void SetEmail(string email)
    {
        SetAttribute("value", email);
    }
}