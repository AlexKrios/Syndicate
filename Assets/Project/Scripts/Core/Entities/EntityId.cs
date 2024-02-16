﻿using System;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public abstract class EntityId<T>
    {
        [SerializeField] private T value;

        protected EntityId(T value)
        {
            Value = value;
        }

        protected T Value
        {
            get => value;
            set => this.value = value;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value?.ToString() ?? string.Empty;
    }
}