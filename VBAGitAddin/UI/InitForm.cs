using System;
using System.Windows.Forms;

namespace VBAGitAddin.UI
{
    public partial class InitForm : Form
    {        
        public InitForm()
        {
            InitializeComponent();
            MakeBare.Text = VBAGitUI.InitForm_MakeBare;
            LabelInfo.Text = VBAGitUI.InitFoem_LabelInfo;
        }      
        
        public bool Bare
        {
            get { return this.MakeBare.Checked; }
        } 

        public DialogResult ShowDialog(string initPath)
        {
            Text = string.Format(VBAGitUI.InitForm_Text, initPath);
            return ShowDialog();
        }
    }
}
