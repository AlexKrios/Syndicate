using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Entities;
using Syndicate.Core.View;
using UnityEngine;

namespace Syndicate.Hub.View
{
    public class ProductionProductView : ComponentViewBase
    {
        [SerializeField] private List<ProductionItemView> items;

        public void SetData(List<ICraftableItem> products, Action<ProductionItemView> clickAction)
        {
            for (var i = 0; i < items.Count; i++)
            {
                items[i].SetData(products[i]);
                items[i].OnClickEvent += clickAction;
            }
        }

        public ProductionItemView GetFirstElement()
        {
            return items.First();
        }
    }
}