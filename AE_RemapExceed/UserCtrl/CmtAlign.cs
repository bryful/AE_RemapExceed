using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AE_RemapExceed
{
    public partial class CmtAlign : UserControl
    {
        private RadioButton[] rb = new RadioButton[9];

        private int m_SelectIndex = -1;
        public CmtAlign()
        {
            InitializeComponent();
            rb[0] = rb0;
            rb[1] = rb1;
            rb[2] = rb2;
            rb[3] = rb3;
            rb[4] = rb4;
            rb[5] = rb5;
            rb[6] = rb6;
            rb[7] = rb7;
            rb[8] = rb8;
            for (int i = 0; i < 9; i++) rb[i].Tag = i;
            rb0.Checked = true;
            m_SelectIndex = 0;
        }

        private void rb0_Click(object sender, EventArgs e)
        {
            int idx = (int)((RadioButton)sender).Tag;
            m_SelectIndex = idx;
        }
        public CmtAligns SelectedIndex
        {
            get { return (CmtAligns)m_SelectIndex; }
            set 
            {
                int v = (int)value;
                if ((v >= 0) && (v < (int)CmtAligns.Count))
                {
                    rb[v].Checked = true;
                    m_SelectIndex = v;
                }
            }
        }
    }
}
