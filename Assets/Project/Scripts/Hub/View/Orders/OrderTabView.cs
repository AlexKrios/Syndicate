using System;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;

namespace Syndicate.Hub.View
{
    [RequireComponent(typeof(Button))]
    public class OrderTabView : ButtonWithActiveBorder
    {
        public Action<OrderTabView> OnClickEvent { get; set; }

        [SerializeField] private OrderGroupType type;

        public OrderGroupType Type => type;

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}