using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Windows.Forms;
using Rabbit.BLL.Repository;

namespace Rabbit.Consumer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            logCustomer = new CustomerRepo().Queryable().Count();
            logMailLog = new MailLogRepo().Queryable().Count();
        }

        private static Consumer _consumer;
        public static long logCustomer = 0, logMailLog = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            _consumer = new Consumer("Customer");
            _consumer.Form = this;
            _consumer.ConsumerEvent.Received += ConsumerEvent_Received;
            _consumer = new Consumer("MailLog");
            _consumer.ConsumerEvent.Received += ConsumerEvent_Received;
            _consumer.Form = this;
            ConsumerEvent_Received(sender, new BasicDeliverEventArgs());
        }

        private void ConsumerEvent_Received(object sender, BasicDeliverEventArgs e)
        {
        }
    }
}
