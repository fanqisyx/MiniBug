﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniBug
{
    public partial class ProjectForm : Form
    {
        /// <summary>
        /// Gets the current operation.
        /// </summary>
        public MiniBug.OperationType Operation { get; private set; } = OperationType.None;

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        public string ProjectName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the name of the project file.
        /// </summary>
        public string ProjectFilename { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the location of the project file.
        /// </summary>
        public string ProjectLocation { get; private set; } = string.Empty;

        public ProjectForm(OperationType operation, string projectName = "", string projectFilename = "", string projectLocation = "")
        {
            InitializeComponent();

            Operation = operation;

            if (Operation == OperationType.Edit)
            {
                // Edit an existing project
                ProjectName = projectName;
                ProjectFilename = projectFilename;
                ProjectLocation = projectLocation;
            }
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.AcceptButton = btOk;
            this.CancelButton = btCancel;

            lblFormTextBig.Width = this.ClientRectangle.Width;

            // Make initializations based on the type of operation
            if (Operation == OperationType.New)
            {
                this.Text = "New Project";
                lblFormTextBig.Text = "Create a new project";

                btOk.Enabled = false;
            }
            else if (Operation == OperationType.Edit)
            {
                this.Text = "Edit Project";
                lblFormTextBig.Text = "Edit the current project";

                // Populate the controls
                txtName.Text = ProjectName;
                txtFilename.Text = ProjectFilename;
                txtLocation.Text = ProjectLocation;
            }

            // Resume the layout logic
            this.ResumeLayout();
        }

        /// <summary>
        /// Browse for the location where the project will be saved.
        /// </summary>
        private void btBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = folderBrowserDialog1.SelectedPath;

                if (txtName.Text != string.Empty)
                {
                    btOk.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        private void btOk_Click(object sender, EventArgs e)
        {
            if ((txtName.Text != string.Empty) && (txtLocation.Text != string.Empty))
            {
                ProjectName = txtName.Text;
                ProjectLocation = txtLocation.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Cancel this operation and close the form.
        /// </summary>
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Handle the TextChanged event for the project name textbox control.
        /// </summary>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty)
            {
                txtFilename.Text = "minibug-" + txtName.Text + ".json";

                if (txtLocation.Text != string.Empty)
                {
                    btOk.Enabled = true;
                }                
            }
            else
            {
                txtFilename.Text = string.Empty;
                btOk.Enabled = false;
            }
        }
    }
}