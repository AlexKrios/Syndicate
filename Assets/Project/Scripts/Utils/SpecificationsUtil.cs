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
        [Inject] private readonly IComponentsService _componentsService;

        private List<SpecificationObject> _specifications = new();

        public void GetSpecificationValues(RecipeObject recipe, bool isSingle = true)
        {
            if (isSingle)
                ResetSpecifications();

            var specs = recipe.Specifications;
            foreach (var spec in specs)
            {
                var needSpec = _specifications.First(x => x.Type == spec.Type);
                needSpec.Value += spec.Value;
            }
        }

        public List<SpecificationObject> GetProductSpecificationValues(ICraftableItem productObject)
        {
            ResetSpecifications();

            GetSpecificationValues(productObject.Recipe, false);
            foreach (var part in productObject.Recipe.Parts)
            {
                if (part.ItemType != ItemType.Component)
                    continue;

                var component = _componentsService.GetComponent((ComponentId)part.Key);
                GetSpecificationValues(component.Recipe, false);
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