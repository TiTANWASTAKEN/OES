using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Mgmt_System
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            // add image from your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            // add new student
            STUDENT student = new STUDENT();
            string fname = textBoxfname.Text;
            string lname = textBoxlname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string contact = textBoxContact.Text;
            string address = textBoxAddress.Text;
            string enroll = textBoxenroll.Text;
            string gender = "Male";
            if(radioButtonFemale.Checked)
            {
                gender = "Female";
            }
            MemoryStream picture = new MemoryStream();

            // we need to check the age of the student

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            if(((this_year - born_year) < 10) || ((this_year - born_year) > 100))
            {
                MessageBox.Show("Student's Age must be in between 10 and 100 years", "Invalid Date of birth", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(verif())
            {
                pictureBoxStudentImage.Image.Save(picture, pictureBoxStudentImage.Image.RawFormat);

                if (student.insertStudent(fname,lname,bdate,contact,address,enroll,gender,picture))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error!!", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Fields!", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        bool verif()
        {
            if((textBoxfname.Text.Trim() == "") ||
               (textBoxlname.Text.Trim() == "") ||
               (textBoxContact.Text.Trim() == "") ||
               (textBoxAddress.Text.Trim() == "") ||
                (textBoxenroll.Text.Trim() == "") ||
                (pictureBoxStudentImage.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
