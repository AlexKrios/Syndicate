using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    public class ProductionService : IProductionService
    {
        [Inject] private readonly IGameService _gameService;

        private readonly Dictionary<Guid, ProductionObject> _productionData = new();

        public void AddProduction(ProductionObject productionObject)
        {
            _productionData.Add(productionObject.Id, productionObject);
        }

        public List<ProductionObject> GetAllProduction() => _productionData.Values.ToList();

        public bool IsHaveFreeCell()
        {
            var queueSize = _gameService.GetPlayerProfile().production.queueSize;
            return queueSize < _productionData.Count;
        }
    }
}