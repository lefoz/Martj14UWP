using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class SerialModel
    {
        public string key { get; set; }
        public bool value { get; set; }

        public SerialModel(string listKey, bool listValue)
        {
            this.key = listKey;
            this.value = listValue;
        }
    }
}
