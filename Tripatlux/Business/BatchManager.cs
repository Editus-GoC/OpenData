using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Tripatlux.Business
{
    public class BatchManager
    {
        private Thread _thread { get; set; }

        public BatchManager()
        {

        }

        public void Start()
        {
            _thread = new Thread(new ThreadStart(DoWork));
            _thread.Start();
        }

        public void DoWork()
        {
            while(true)
            {
                Thread.Sleep(10000);

                //Send sms 10min before car comming
                //TODO
            }
        }
    }
}