using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Data.Enums
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum OrderStatus
    {
        pending,
        processing,
        completed,
        cancelRequested,
        refundRequested,
        refunded,
        canceled,
    }
}