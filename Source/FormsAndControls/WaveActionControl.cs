using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CableGuardian
{
    partial class WaveActionControl : TriggeredActionControl
    {
        public event EventHandler<ChangeEventArgs> ChangeMade;
        public TriggeredAction TheWaveAction { get; private set; }
        ToolTip TTip = new ToolTip() { AutoPopDelay = 30000, ShowAlways = true };

        public WaveActionControl()
        {
            InitializeComponent();            
            AddEventHandlers();                        
        }
               

        public void LoadWaveAction(TriggeredAction waveAction)
        {
            TheWaveAction = waveAction;
            WaveEdit.LoadWaveAction(waveAction.TheAction as CGActionWave);
            CondEdit.LoadCondition(waveAction.TheTrigger.RotCondition);
            TrigEdit.LoadTrigger(waveAction.TheTrigger);
        }

        void AddEventHandlers()
        {               
            TrigEdit.TriggeringEventChanged += OnTriggeringEventChanged;
            WaveEdit.ChangeMade += OnSubControlChangeMade;
            TrigEdit.ChangeMade += OnSubControlChangeMade;
            CondEdit.ChangeMade += OnSubControlChangeMade;
        }

        void OnSubControlChangeMade(object sender, ChangeEventArgs e)
        {
            InvokeChangeMade(e);
        }


        void OnTriggeringEventChanged(object sender, EventArgs e)
        {            
            CondEdit.SetControlVisibility(); // available conditions are different for different rotation events            
        }

        void InvokeChangeMade(ChangeEventArgs e)
        {
            ChangeMade?.Invoke(this, e);
        }
    }
}
