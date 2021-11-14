using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneNetwork.EF
{
    class EntEF
    {
        public static SubscribersTelephoneNetworkEntities Context { get; } = new SubscribersTelephoneNetworkEntities();
        public static int idEmployee;
        public static int idSubscriber;
        public static int idNumber;
        public static int idTariff;
    }
}
