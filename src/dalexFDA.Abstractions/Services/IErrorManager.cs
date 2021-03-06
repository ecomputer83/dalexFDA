﻿using System;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface IErrorManager
    {
        Task DisplayErrorMessageAsync(Exception ex, string errorMessage = null);
        void LogException(Exception ex, bool rethrow = false, [System.Runtime.CompilerServices.CallerMemberName] string caller = "");
    }
}
