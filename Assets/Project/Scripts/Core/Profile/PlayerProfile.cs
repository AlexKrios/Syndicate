namespace Syndicate.Core.Profile
{
    public class PlayerProfile
    {
        public ProfileState Profile { get; } = new();
        public InventoryState Inventory { get; } = new();
        public UnitsState Units { get; } = new();
        public ProductionState Production { get; } = new();
    }
}