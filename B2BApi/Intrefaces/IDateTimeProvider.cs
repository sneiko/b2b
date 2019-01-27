using System;

namespace B2BApi.Intrefaces
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }        
    }
}