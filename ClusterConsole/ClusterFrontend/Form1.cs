using Akka.Actor;
using Akka.Cluster;
using Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClusterFrontend
{
    public partial class Form1 : Form
    {
        IActorRef formActor;
        public Form1()
        {
            InitializeComponent();

            Props props = Props.Create(() => new FormActor())
            .WithDispatcher("akka.actor.synchronized-dispatcher");
            formActor = Program.System.ActorOf(props);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            formActor.Tell(new Msg(rtbText.Text));
        }
    }
}
