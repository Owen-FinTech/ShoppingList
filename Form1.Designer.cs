namespace ShoppingList
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void CreateScrollPanel()
        {
            // Create the scrollable panel
            scrollPanel = new Panel();
            scrollPanel.AutoScroll = true; // Enable automatic scrolling
            scrollPanel.Dock = DockStyle.Fill; // Fill the entire form

            // Add the scrollPanel to the form
            this.Controls.Add(scrollPanel);
        }

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CreateScrollPanel();
            addbutton = new Button();
            deleteButton = new Button();
            printButton = new Button();
            itemNameTextBox = new TextBox();
            SuspendLayout();
            // 
            // addbutton
            // 
            addbutton.Location = new Point(12, 12);
            addbutton.Name = "addbutton";
            addbutton.Size = new Size(112, 34);
            addbutton.TabIndex = 2;
            addbutton.Text = "Add";
            addbutton.UseVisualStyleBackColor = true;
            addbutton.Click += AddButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(662, 12);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(112, 34);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // printButton
            // 
            printButton.Location = new Point(1324, 12);
            printButton.Name = "printButton";
            printButton.Size = new Size(112, 34);
            printButton.TabIndex = 4;
            printButton.Text = "Print";
            printButton.UseVisualStyleBackColor = true;
            printButton.Click += PrintButton_Click;
            // 
            // itemNameTextBox
            // 
            itemNameTextBox.Location = new Point(130, 15);
            itemNameTextBox.Name = "itemNameTextBox";
            itemNameTextBox.Size = new Size(300, 31);
            itemNameTextBox.TabIndex = 5;
            itemNameTextBox.TextChanged += ItemNameTextBox_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1900, 900);
            scrollPanel.Controls.Add(itemNameTextBox);
            scrollPanel.Controls.Add(printButton);
            scrollPanel.Controls.Add(deleteButton);
            scrollPanel.Controls.Add(addbutton);
            Name = "Form1";
            Text = "Shopping List";
            ResumeLayout(false);
            PerformLayout();
        }

        private List<CheckBox> checkBoxes = new List<CheckBox>(); // Maintain a list of checkboxes
        private List<TextBox> textBoxes = new List<TextBox>(); // Maintain a list of textboxes

        private void CreateCheckBoxes()
        {
            int yPos = 100;
            int xPosCheckBox = 12;
            int xPosTextBox = 312;

            // Clear existing checkboxes and remove them from the collection
            foreach (CheckBox checkBox in checkBoxes)
            {
                scrollPanel.Controls.Remove(checkBox);
                checkBox.Dispose();
            }
            checkBoxes.Clear();

            foreach (TextBox textBox in textBoxes)
            {
                scrollPanel.Controls.Remove(textBox);
                textBox.Dispose();
            }
            textBoxes.Clear();

            for (int i = 0; i < groceryList.Count; i++)
            {
                CheckBox newItemCheckBox = new CheckBox();
                newItemCheckBox.Location = new Point(xPosCheckBox, yPos);
                newItemCheckBox.Size = new Size(300, 31);
                newItemCheckBox.Text = groceryList[i].Name; // Set the checkbox label to the item's name
                newItemCheckBox.Checked = groceryList[i].IsSelected;
                newItemCheckBox.CheckedChanged += CheckBox_CheckedChanged;

                TextBox annotationTextBox = new TextBox();
                annotationTextBox.Location = new Point(xPosTextBox, yPos);
                annotationTextBox.Name = groceryList[i].Name;
                annotationTextBox.Size = new Size(250, 31);
                annotationTextBox.TabIndex = 1;
                annotationTextBox.TextChanged += AnnotationTextBox_TextChanged;

                // Add the newly created CheckBox to the form
                scrollPanel.Controls.Add(newItemCheckBox);
                scrollPanel.Controls.Add(annotationTextBox);
                checkBoxes.Add(newItemCheckBox);
                textBoxes.Add(annotationTextBox);

                // Increase the yPos to position the next CheckBox below
                if (i == groceryList.Count / 3 || i == (groceryList.Count / 3) * 2)
                {
                    yPos = 100;
                    xPosCheckBox += 612;
                    xPosTextBox += 612;
                }
                else
                {
                    yPos += newItemCheckBox.Height + 10; // Adjust the spacing as needed
                }
            }
        }

        #endregion

        private Button addbutton;
        private Button deleteButton;
        private Button printButton;
        private TextBox itemNameTextBox;
    }
}
