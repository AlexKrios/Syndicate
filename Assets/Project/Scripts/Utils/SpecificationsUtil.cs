using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Zenject;

namespace Syndicate.Utils
{
    [UsedImplicitly]
    public class SpecificationsUtil
    {
        [Inject] private readonly IProductsService _productsService;

        private List<SpecificationObject> _specifications = new();

        public List<SpecificationObject> GetUnitSpecificationValues(UnitObject data)
        {
            var specList = new List<SpecificationObject>();
            foreach (var specification in data.Specifications)
            {
                var specCopy = new SpecificationObject(specification);
                foreach (var (_, value) in data.Outfit)
                {
                    var product = _productsService.GetProduct(new ProductId(value));
                    specCopy.Value += product.Specifications.First(y => y.Type == specification.Type).Value;
                }

                specList.Add(specCopy);
            }

            return specList;
        }

        public List<SpecificationObject> GetProductSpecificationValues(ICraftableItem productObject)
        {
            ResetSpecifications();

            GetSpecificationValues(productObject.Specifications, false);

            return _specifications;
        }

        private void GetSpecificationValues(List<SpecificationObject> specifications, bool isSingle = true)
        {
            if (isSingle)
                ResetSpecifications();

            foreach (var spec in specifications)
            {
                var needSpec = _specifications.First(x => x.Type == spec.Type);
                needSpec.Value += spec.Value;
            }
        }

        private void ResetSpecifications()
        {
            _specifications = new List<SpecificationObject>
            {
                new() { Type = SpecificationId.Attack, Value = 0 },
                new() { Type = SpecificationId.Health, Value = 0 },
                new() { Type = SpecificationId.Defense, Value = 0 },
                new() { Type = SpecificationId.Initiative, Value = 0 }
            };
        }
    }
}