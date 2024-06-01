using System;
using UnityEngine;

namespace Syndicate.Core.View
{
    public class AuthViewModel : ViewModelBase, IScreenViewModel
    {
        public Action ResetViews { get; set; }

        public GameObject SignInView { get; set; }
        public GameObject SignUpView { get; set; }
        public GameObject VerificationView { get; set; }
    }
}