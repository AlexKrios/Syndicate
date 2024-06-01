using Syndicate.Core.Entities;
using TMPro;
using UnityEngine;

namespace Syndicate.Core.View
{
    public class SpecificationView : MonoBehaviour
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