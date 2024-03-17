namespace Syndicate.Core.Profile
{
    public class PlayerProfile
    {
        public InventoryState Inventory { get; } = new();
        public ProductionState Production { get; } = new();
    }
}