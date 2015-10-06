using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REStructure
{
    public interface IUpgradable
    {
        bool UpgradeCheck(object target);
        object Upgrade();
    }
}
