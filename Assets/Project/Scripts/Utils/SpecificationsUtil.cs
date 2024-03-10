using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Zenject;

namespace Syndicate.Utils
{
    public class SpecificationsUtil
    {
        [Inject] private readonly IComponentsService _componentsService;

        private List<SpecificationObject> _specifications = new();

        public void GetComponentSpecificationValues(ComponentObject componentObject, bool isSingle = true)
        {
            if (isSingle)
                ResetSpecifications();

            var specs = componentObject.Recipe.Specifications;
            foreach (var spec in specs)
            {
                var needSpec = _specifications.First(x => x.Type == spec.Type);
                needSpec.Value += spec.Value;
            }
        }

        public List<SpecificationObject> GetProductSpecificationValues(List<PartObject> partObjects)
        {
            ResetSpecifications();

            foreach (var part in partObjects)
            {
                if (part.ItemType != ItemTypeId.Component)
                    continue;

                var component = _componentsService.GetComponent((ComponentId)part.ItemId);
                GetComponentSpecificationValues(component, false);
            }

            return _specifications;
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