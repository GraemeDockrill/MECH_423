namespace Lab1_Visual_C__Tutorial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //button1.Click += form_click;

            foreach (Button target in this.Controls)
            {
                target.Click += form_click;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void form_click(object sender, EventArgs e)
        {
            //Do something useful
            MessageBox.Show("Test");
            button1.Click -= form_click; //Removes event handler, won't execute in subsequent events
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}