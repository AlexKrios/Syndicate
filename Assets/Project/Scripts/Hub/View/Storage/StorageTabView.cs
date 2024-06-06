using System;
using DG.Tweening;
using Syndicate.Core.Entities;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;

namespace Syndicate.Hub.View
{
    [RequireComponent(typeof(Button))]
    public class StorageTabView : ButtonWithActiveBorder
    {
        public Action<StorageTabView> OnClickEvent { get; set; }

        [SerializeField] private UnitTypeId type;

        public UnitTypeId Type => type;

        private Sequence _sequence;

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}