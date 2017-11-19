# Training
This is training application.

For now project can have multiple forms as sub-programs.

Just create new form and then in Program.cs add new ProgramForm instance to forms list.

Like this:

new ProgramForm() { FormType = typeof(<YourFormClassNAme>), Name = "Description"}

Build project, and now you can choose your new sub-program from combo box and launch it!