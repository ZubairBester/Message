using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDS.Client;

namespace Application1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create MDSClient object to connect to DotNetMQ
            //Name of this application: Application1
            var mdsClient = new MDSClient("Application1");

            //Connect to DotNetMQ server
            mdsClient.Connect();

            Console.WriteLine("Write your firstname and press enter to send " +
               "to Application2. Write 'exit' to stop application.");

            while (true)
            {
                //Get a message from user
                var firstNameText = Console.ReadLine();
                if (string.IsNullOrEmpty(firstNameText) || firstNameText == "exit")
                {
                   break;

                   
                }

                //Create a DotNetMQ Message to send to Application2
                var message = mdsClient.CreateMessage();
                //Set destination application name
                message.DestinationApplicationName = "Application2";
                //Set message data
                message.MessageData = Encoding.UTF8.GetBytes(firstNameText);

                //Send message
                message.Send();
                //Disconnect from DotNetMQ server
                mdsClient.Disconnect();
            }
           
        }
    }
}
