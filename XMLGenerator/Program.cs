using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMLGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            int noOfFiles = 1;

            try
            {

                List<List<ContractClass>> objectsToSerialize = new List<List<ContractClass>>();
                for (int i = 1; i <= noOfFiles; ++i)
                {
                    List<ContractClass> contractData = new List<ContractClass>();
                    var contract1 = new ContractClass();
                    contract1.Prop1 = $"file{i}";
                    contract1.Prop2 = $"file{i}";

                    contractData.Add(contract1);
                    objectsToSerialize.Add(contractData);
                }


                int count = 1;
                foreach (List<ContractClass> contractData in objectsToSerialize)
                {
                    var stringwriter = new System.IO.StringWriter();
                    var serializer = new XmlSerializer(typeof(List<ContractClass>), new XmlRootAttribute("ContractClasses"));

                    //1. Using FileStream
                    //string fileName = String.Format(@"C:\TestBlobDownloadContent\UsingFileStream" + count + ".xml");
                    //var file = new FileStream(fileName, FileMode.OpenOrCreate);
                    //serializer.Serialize(file, contractData);
                    //file.Close();

                    //2. Using XMLDoc
                    serializer.Serialize(stringwriter, contractData);
                    var xmlString = stringwriter.ToString();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlString);
                    doc.PreserveWhitespace = true;
                    string fileName = String.Format(@"C:\TestBlobDownloadContent\UsingXMLDoc" + count + ".xml");
                    doc.Save(fileName);
                    count++;
                }

                Console.WriteLine("Done generating XMLs..");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
