using System;
using DG.Tweening;
using Syndicate.Core.Entities;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;

namespace Syndicate.Hub.View
{
    [RequireComponent(typeof(Button))]
    public class UnitTabView : ButtonWithActiveBorder
    {
        public Action<UnitTabView> OnClickEvent { get; set; }

        [SerializeField] private UnitTypeId type;

        public UnitTypeId Type => type;

        private Sequence _sequence;

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}