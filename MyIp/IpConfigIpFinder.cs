using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MyIp
{

    internal class IpConfigIpFinder : IIpFinder
    {

        [DllImport("kernel32.dll")]
        private static extern int GetSystemDefaultLCID();

        private ProcessStartInfo _procStartInfo;

        public IpConfigIpFinder()
        {
            _procStartInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "ipconfig",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.GetCultureInfo(GetSystemDefaultLCID()).TextInfo.OEMCodePage)
            };
        }

        public List<IpInfo> FindAll()
        {
            return ParseOutput(GetOutput());
        }

        private List<IpInfo> ParseOutput(string str)
        {

            string[] linesRaw = str.Split(new char[] { '\n' });
            var linesNr = new List<string>();

            foreach (var ln in linesRaw)
            {
                if (!string.IsNullOrWhiteSpace(ln))
                {
                    if (ln.Contains(":"))
                    {
                        linesNr.Add(ln.Trim());
                    }
                }
            }

            var found = new List<IpInfo>();

            IpVersion? pIpVer = null;
            string pName = null;
            string[] pBits = null;

            foreach (var ln in linesNr)
            {

                if (ln.ToLower().Contains("adapter") && ln.Trim().EndsWith(":"))
                {

                    pName = ln.Substring(
                            ln.IndexOf("adapter", StringComparison.InvariantCultureIgnoreCase) + 7).Trim();

                    pName = pName.Substring(0, pName.Length - 1);

                    if (pName.Equals("Ethernet", StringComparison.InvariantCultureIgnoreCase))
                    {
                        pName = "Eth";
                    }
                    else if (pName.StartsWith("vEthernet", StringComparison.InvariantCultureIgnoreCase))
                    {
                        pName = "vEth";
                    }

                }

                if (pName != null)
                {

                    if (ln.ToLower().Contains("ipv4 address"))
                    {
                        pIpVer = IpVersion.V4;
                    }
                    else if (ln.ToLower().Contains("ipv6 address"))
                    {
                        pIpVer = IpVersion.V6;
                    }
                    else
                    {
                        pIpVer = null;
                    }

                    if (pIpVer.HasValue)
                    {

                        pBits = ln.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);

                        found.Add(new IpInfo()
                        {
                            Version = pIpVer.Value,
                            Label = pName,
                            IpString = pBits[1].Trim().ToLower()
                        });

                    }

                }

            }

            var ethV4 = new List<IpInfo>();
            var ethV6 = new List<IpInfo>();

            var wifiV4 = new List<IpInfo>();
            var wifiV6 = new List<IpInfo>();

            var othersV4 = new List<IpInfo>();
            var othersV6 = new List<IpInfo>();

            foreach (var ip in found)
            {

                if (ip.Label.Equals("Eth", StringComparison.InvariantCultureIgnoreCase))
                {

                    if (ip.Version == IpVersion.V6)
                    {
                        ethV6.Add(ip);
                    }
                    else
                    {
                        ethV4.Add(ip);
                    }

                }
                else if (ip.Label.Equals("Wi-Fi", StringComparison.InvariantCultureIgnoreCase))
                {

                    if (ip.Version == IpVersion.V6)
                    {
                        wifiV6.Add(ip);
                    }
                    else
                    {
                        wifiV4.Add(ip);
                    }

                }
                else
                {

                    if (ip.Version == IpVersion.V6)
                    {
                        othersV6.Add(ip);
                    }
                    else
                    {
                        othersV4.Add(ip);
                    }

                }

            }

            found.Clear();

            found.AddRange(ethV4);
            found.AddRange(wifiV4);
            found.AddRange(othersV4);

            found.AddRange(ethV6);
            found.AddRange(wifiV6);
            found.AddRange(othersV6);

            return found;

        }

        private string GetOutput()
        {

            var proc = Process.Start(_procStartInfo);
            string output = proc.StandardOutput.ReadToEnd();

            return output;

        }

    }

}
