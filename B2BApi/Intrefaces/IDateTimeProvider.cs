using System;

namespace B2BApi.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }        
    }
}