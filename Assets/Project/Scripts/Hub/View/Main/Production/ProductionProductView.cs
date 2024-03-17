using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Hub.View.Main
{
    public class ProductionProductView : MonoBehaviour
    {
        [SerializeField] private List<ProductionItemView> items;

        public void SetData(List<ICraftableItem> itemObjects, Action<ProductionItemView> clickAction)
        {
            for (var i = 0; i < items.Count; i++)
            {
                items[i].SetData(itemObjects[i]);
                items[i].OnClickEvent += clickAction;
            }
        }

        public ProductionItemView GetFirstElement()
        {
            return items.First();
        }
    }
}