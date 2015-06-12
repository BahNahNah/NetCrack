using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

public class AutoUpdater
{
    private Type Droid;
    private string Server;
    private string droid_location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BahNahNah");
    private string droid_file = "droid.updater";
    public AutoUpdater(string ser)
    {
        try
        {
            Server = ser;
            if (!Server.ToLower().StartsWith("http://"))
                Server = "http://" + Server;
            if (!Server.ToLower().EndsWith("/"))
                Server += "/";
            droid_file = Server.Replace("http://", "").Replace("/", "_") + ".droid";
            if (!File.Exists(Path.Combine(droid_location, droid_file)))
                DownloadDroid();
            Droid = Assembly.LoadFile(Path.Combine(droid_location, droid_file)).GetType("Droid");
            Droid.GetMethod("SetServer").Invoke(null, new object[] { Server });
            Droid.GetMethod("SetExecutingFile").Invoke(null, new object[] { Assembly.GetExecutingAssembly() });
            Droid.GetMethod("SetDroidLocation").Invoke(null, new object[] { droid_location });
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occured.\n" + ex.Message);
            Environment.Exit(0);
        }
    }
    public void Initilise(string ID, bool PromtForUpdate)
    {
        Droid.GetMethod("Initilise").Invoke(null, new object[] { ID, PromtForUpdate });
    }
    private void DownloadDroid()
    {
        try
        {
            if (!Directory.Exists(droid_location))
                Directory.CreateDirectory(droid_location);
            using (WebClient wc_droid_downloader = new WebClient())
            {
                XDocument xDoc = XDocument.Load(string.Format("{0}?q=downloaddroid&r=", Server, Guid.NewGuid().ToString().Replace("-", "")));
                var responceElement = xDoc.Element("response");
                if (responceElement.Attribute("Success").Value != "1")
                    throw new Exception(responceElement.Attribute("Message").Value);
                wc_droid_downloader.DownloadFile(string.Format("{0}{1}", Server, responceElement.Attribute("Location").Value), Path.Combine(droid_location, droid_file));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occured.\n" + ex.Message);
            Environment.Exit(0);
        }
    }
}
