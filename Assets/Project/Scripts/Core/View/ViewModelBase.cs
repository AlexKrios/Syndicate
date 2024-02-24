using System;
using UnityEngine;

namespace Syndicate.Core.View
{
    public class ViewModelBase
    {
        public GameObject GameObject { get; set; }

        public Action Show { get; set; }
        public Action Hide { get; set; }
    }
}