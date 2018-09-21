namespace MyIp
{

    internal class IpInfo
    {

        public IpInfo()
        {
            Version = IpVersion.V4;
            Label = string.Empty;
            IpString = "127.0.0.1";
        }

        public IpVersion Version { get; set; }

        public string Label { get; set; }

        public string IpString { get; set; }

        public override string ToString()
        {
            return Label + " (" + Version + "): " + IpString;
        }

    }

}
