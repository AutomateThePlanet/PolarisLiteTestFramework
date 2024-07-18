using System.Drawing;

namespace PolarisLite.Core.Layout;
public interface ILayoutComponent
{
    Point Location { get; }
    Size Size { get; }
}