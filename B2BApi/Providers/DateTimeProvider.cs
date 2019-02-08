using B2BApi.Interfaces;

namespace B2BApi.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public System.DateTime Now => System.DateTime.Now;
    }
}