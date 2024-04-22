using System;
using UnityEngine;

namespace Syndicate.Core.View
{
    public class AuthViewModel : ViewModelBase
    {
        public Action ResetViews { get; set; }

        public GameObject SignInView { get; set; }
        public GameObject SignUpView { get; set; }
        public GameObject VerificationView { get; set; }
    }
}