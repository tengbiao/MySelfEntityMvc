using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MySelfEntityMvc.Models.EntityCustom
{
    [DataContract]
    public class DataPage<T>
    {
        [DataMember]
        public int RecordCount { get; set; }

        [DataMember]
        public List<T> Results { get; set; }

        [DataMember]
        public int PageCount { get; set; }

        [DataMember]
        public int Current { get; set; }

        [DataMember]
        public int Size { get; set; }
    }
}
