using System.IO;
using System.Xml.Linq;

namespace AccesoXml
{
    public class AccesoDocumento
    {
        private XDocument _xml;

        public AccesoDocumento(XDocument xml)
        {
            _xml = xml;
        }

        public AccesoDocumento(Stream streamXml)
        {
            _xml = XDocument.Load(streamXml);
        }

        public AccesoDocumento(string stringXml)
        {
            _xml = XDocument.Parse(stringXml);
        }

        public XElement File()
        {
            return _xml.Root?.Element("File");
        }

        public XElement Key()
        {
            return _xml.Root?.Element("Key");
        }

        public XElement FileName()
        {
            return _xml.Root?.Element("FileName");
        }
    }
}
