using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Crud_Oprations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();


        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-ISK321A\\SQLEXPRESS;Initial Catalog=IT4SolutionsTest;Integrated Security=True");
        public int StudentId;
        private void GetStudentsRecord()
        {
           
            SqlCommand cmd = new SqlCommand("Select * from StudentsTb", con);
            DataTable dt = new DataTable();

            con.Open(); 
            
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr); 
            con.Close();

            StudentRecordDataGridView.DataSource= dt;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES(@Name,@FatherName,@RollNumber,@Address,@Mobile)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobileNo.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Student is Succesfully Saved in The Database", "Saved", MessageBoxButtons.OK , MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControl();
            }

        }

        private bool IsValid()
        {
            if(txtStudentName.Text == String.Empty)
            {
                MessageBox.Show("Student Name is Required", "Failed" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControl();

        }

        private void ResetFormControl()
        {
            StudentId = 0;
            txtStudentName.Clear();
            txtFatherName.Clear();
            txtRollNo.Clear();
            txtMobileNo.Clear();
            txtAddress.Clear();

            txtStudentName.Focus();
        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentId = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNo.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobileNo.Text = StudentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {

                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name=@Name,FatherName=@FatherName,RollNumber=@RollNumber,Address=@Address,Mobile=@Mobile WHERE StudentID =@Id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobileNo.Text);
                cmd.Parameters.AddWithValue("@Id",this.StudentId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is Updated Succesfully ", "Updated", MessageBoxButtons.OK,MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControl();

            }
            else
            {
                MessageBox.Show("Select Student To Update Record", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID =@Id", con);
               
                cmd.Parameters.AddWithValue("@Id", this.StudentId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is Deleted Succesfully ", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControl();
            }
            else
            {
                MessageBox.Show("Select Student To Delete Record", "Select", MessageBoxButtons. OK, MessageBoxIcon.Error);
            }
        }
    }
}
