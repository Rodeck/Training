using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Training
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            List<ProgramForm> forms = new List<ProgramForm>() 
            { 
                new ProgramForm() { FormType = typeof(Form1), Name = "Maps"},
                new ProgramForm() { FormType = typeof(Drop), Name = "Drop symulator"},
                new ProgramForm() { FormType = typeof(Task3), Name = "Task 3"}
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProgramSwitcher switcher = new ProgramSwitcher(forms);

            Application.Run(switcher);
        }
    }

    public class ProgramForm
    {
        public Type FormType { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
