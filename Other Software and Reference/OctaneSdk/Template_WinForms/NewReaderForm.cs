using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Octane_ViewModel;

namespace Template_WinForms
{
    public partial class NewReaderForm : Form
    {
        public NewReaderForm()
        {
            InitializeComponent();
        }

        public NewReaderModel NewReaderModel
        {
            get
            {
                var model = new NewReaderModel();
                model.ReaderName = _nameTextBox.Text;
                model.ReaderIdentity = _identityTextBox.Text;
                
                return model;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
