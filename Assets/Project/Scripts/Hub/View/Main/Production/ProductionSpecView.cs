using Syndicate.Core.Entities;
using TMPro;
using UnityEngine;

namespace Syndicate.Hub.View.Main
{
    public class ProductionSpecView : MonoBehaviour
    {
        private const string DefaultSpecificationValue = "0";

        [SerializeField] private SpecificationId id;
        [SerializeField] private TMP_Text count;

        public SpecificationId Id => id;

        public void SetData(SpecificationObject specificationObject)
        {
            count.text = specificationObject.Value.ToString();
        }

        public void ResetData()
        {
            count.text = DefaultSpecificationValue;
        }
    }
}