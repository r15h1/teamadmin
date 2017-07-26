using Newtonsoft.Json.Converters;

namespace TeamAdmin.Web.Formatters
{
    public class DateOnlyConverter: IsoDateTimeConverter
    {
        public DateOnlyConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
