using B2BApi.Intrefaces;

namespace B2BApi.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public System.DateTime Now => System.DateTime.Now;
    }
}