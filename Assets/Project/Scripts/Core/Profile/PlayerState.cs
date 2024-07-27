namespace Syndicate.Core.Profile
{
    public class PlayerState
    {
        public ProfileState Profile { get; } = new();
        public UnitsState Units { get; } = new();
        public InventoryState Inventory { get; } = new();
        public ProductionState Production { get; } = new();
        public ExpeditionState Expedition { get; } = new();
        public TradeState Trade { get; } = new();
    }
}