using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace SendReceiveMSMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            const string queueName = @".\private$\TestQueue";

            FireCommand(queueName);
        }

        private static void FireCommand(string queueName)
        {
            Console.WriteLine("\n\nWrite 1 to send message and 2 to receive message.");
            var command = Console.ReadLine();
            if (command.Equals("1"))
            {
                SendMessageToQueue(queueName);
                FireCommand(queueName);
            }
            else if (command.Equals("2"))
            {
                ReceiveMessageFromQueue(queueName);
                FireCommand(queueName);
            }
        }

        private static void ReceiveMessageFromQueue(string queueName)
        {

            MessageQueue msMq = msMq = new MessageQueue(queueName);

            try
            {

                // msMq.Formatter = new XmlMessageFormatter(new Type[] {typeof(string)});
                msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Person) });
                var message = (Person)msMq.Receive().Body;

                Console.WriteLine("FirstName: " + message.FirstName + ",  LastName: " + message.LastName);
                // Console.WriteLine(message.Body.ToString());

            }
            catch (MessageQueueException ee)
            {
                Console.Write(ee.ToString());
            }
            catch (Exception eee)
            {
                Console.Write(eee.ToString());
            }
            finally
            {
                msMq.Close();
            }
            Console.WriteLine("Message received ......");
        }

        private static void SendMessageToQueue(string queueName)
        {
            // check if queue exists, if not create it
            MessageQueue msMq = null;
            if (!MessageQueue.Exists(queueName))
            {
                msMq = MessageQueue.Create(queueName);
            }
            else
            {
                msMq = new MessageQueue(queueName);
            }

            try
            {
                // msMq.Send("Sending data to MSMQ at " + DateTime.Now.ToString());
                Person p = new Person()
                {
                    FirstName = "Zubair",
                    LastName = "Bester"
                };
                msMq.Send(p);

            }
            catch (MessageQueueException ee)
            {
                Console.Write(ee.ToString());
            }
            catch (Exception eee)
            {
                Console.Write(eee.ToString());
            }
            finally
            {
                msMq.Close();
            }
            Console.WriteLine("Message sent ......");
        }
    }

    // create a class
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
