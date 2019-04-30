using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAndMultiThdInWF
{
    public class Puber
    {
        public event EventHandler<string> OnEvent;

        public void Trigger(string msg)
        {
            if (OnEvent!=null)
            {
                OnEvent(this, msg);
            }
        }
    }
}
