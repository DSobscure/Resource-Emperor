namespace REStructure
{
    public interface IUpgradable
    {
        bool UpgradeCheck(object target);
        object Upgrade();
    }
}
