using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InventoryMaintenance
{
    public static class InvItemDB
    {
        //private const string Path = @"C:\Users\nelso\Downloads\InventoryItems.xml";
        private const string Path = @"..\..\InventoryItems.xml";

        public static List<InvItem> GetItems()
        {
            // create the list
            List<InvItem> items = new List<InvItem>();

            // Add code here to read the XML file into the List<InvItem> object.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            XmlReader xmlIn = XmlReader.Create(Path, settings);

            if (xmlIn.ReadToDescendant("Item"))
            {

                do
                {
                    InvItem i = new InvItem();

                    xmlIn.ReadStartElement("Item");
                    i.ItemNo = xmlIn.ReadElementContentAsInt();
                    i.Description = xmlIn.ReadElementContentAsString();
                    i.Price = xmlIn.ReadElementContentAsDecimal();

                    items.Add(i);
                } while (xmlIn.ReadToNextSibling("Item")) ;

            }
                     
            xmlIn.Close();
            return items;
            
        }

        public static void SaveItems(List<InvItem> items)
        {
            // Add code here to write the List<InvItems> object to an XML file.
           
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");


            XmlWriter xmlOut = XmlWriter.Create(Path, settings);

            xmlOut.WriteStartDocument();
            xmlOut.WriteStartElement("Items");

            foreach (InvItem o in items)
            {
                xmlOut.WriteStartElement("Item");
                xmlOut.WriteElementString("ItemNo", Convert.ToString(o.ItemNo));
                xmlOut.WriteElementString("Description", o.Description);
                xmlOut.WriteElementString("Price", Convert.ToString(o.Price));
                xmlOut.WriteEndElement();
            }
            xmlOut.WriteEndElement();
            xmlOut.Close();
        }
    }
}
