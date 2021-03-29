using Akka.Actor;
using Akka.Cluster;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClusterRouterExample
{

    public partial class Form1 : Form
    {
        private IActorRef formActor;
        private int numBackends;
        public Form1()
        {
            InitializeComponent();
            numBackends = 0;
            Props props = Props.Create(() => new FormActor())
                .WithDispatcher("akka.actor.synchronized-dispatcher");

            formActor = Program.System.ActorOf(props, "formActor");

            //Cluster.Get(Program.System).RegisterOnMemberUp(() =>
            //{
            //    numBackends = numBackends + 1;
            //    if(numBackends >= 2)
            //    {
            //        button1.Enabled = true;
            //    }
            //});
            //Cluster.Get(Program.System).RegisterOnMemberRemoved(() =>
            //{
            //    if(numBackends <= 2)
            //    {
            //        button1.Enabled = false;
            //    }
            //});
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNums.Text)) return;
            var nums = txtNums.Text.Split(',').Select(x => int.Parse(x));

            var msg = new Nums(nums);
            formActor.Tell(msg);
        }
    }
}
