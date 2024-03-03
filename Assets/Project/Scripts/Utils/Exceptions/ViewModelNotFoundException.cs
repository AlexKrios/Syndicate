using System;

namespace Syndicate.Utils.Exceptions
{
    [Serializable]
    public class ViewModelNotFoundException : Exception
    {
        public ViewModelNotFoundException(Type type) : base($"ViewModel of type ({type.Name}) not found") { }
    }
}