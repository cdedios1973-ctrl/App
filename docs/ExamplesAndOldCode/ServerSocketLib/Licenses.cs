using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

using DAOLib;

namespace ServerSocketLib 
{
    public class Licenses 
    {
        private List<License> licenseList;
        private Timer timer = new Timer(900000);

        public Licenses() 
        {
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            loadLicenses();
            timer.Start();
        }

        public bool CheckLicense(String hdSerial)
        {
            License license = licenseList.Find(ByHdSerial(hdSerial));

            return license != null && license.Active == 1;
        }

        public License GetLicenseDetails(String hdSerial)
        {
            return licenseList.Find(ByHdSerial(hdSerial));
        }

        private void loadLicenses() 
        {
            this.licenseList = new DAO<License>().FindAll();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            loadLicenses();
            timer.Start();
        }

        private Predicate<License> ByHdSerial(String hdSerial) 
        {
            return delegate(License license) 
            {
                return license.HDSerial == hdSerial;
            };
        }
    }
}
