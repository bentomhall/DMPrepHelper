using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.DMGDungeonBuilder
{
    interface IDMGLocation
    {
        bool IsChamber { get; }
        bool IsPassage { get; }
        bool IsDoor { get; }
    }
}
