using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationConsoleClient.CustomAtrributes
{
    public class DisplayAttribute : Attribute
    {
        public string Text { get;  private set; }

        public DisplayAttribute(string text)
        {
            this.Text = text;
        }
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IsRequriedAttribute : Attribute
    {

    }
}
