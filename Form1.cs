using System.Drawing.Printing;


namespace ShoppingList
{
    public partial class Form1 : Form
    {
        private List<GroceryItem> groceryList = new List<GroceryItem>();
        private string? filePath = "../../../Items.txt"; // Relative path to the file
        private Panel? scrollPanel;

        public Form1()
        {
            InitializeComponent();
            LoadFromFile(filePath); // Load items from the file
            CreateCheckBoxes();
        }

        private void LoadFromFile(string filePath)
        {
            groceryList.Clear();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    AddItemToList(line);                    
                }
            }
        }

        private async Task SaveToFileAsync(string filePath)
        {
            // Sort the groceryList alphabetically before saving
            groceryList.Sort((x, y) => string.Compare(x.Name, y.Name));

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var item in groceryList)
                {
                    await writer.WriteLineAsync(item.Name);
                }
            }
        }

        private async void DeleteSelectedItems()
        {
            if (scrollPanel != null)
            {
                // Collect items to remove from the list
                List<GroceryItem> itemsToRemove = new List<GroceryItem>();

                // Collect checkboxes to remove from the form
                List<CheckBox> checkboxesToRemove = new List<CheckBox>();

                foreach (Control control in scrollPanel.Controls)
                {
                    if (control is CheckBox checkBox && checkBox.Checked)
                    {
                        string itemName = checkBox.Text;

                        // Find the corresponding grocery item by matching the checkbox label (item name)
                        GroceryItem? item = groceryList.Find(g => g.Name == itemName);

                        if (item != null)
                        {
                            itemsToRemove.Add(item); // Collect items to remove
                            checkboxesToRemove.Add(checkBox); // Collect checkboxes to remove
                        }
                    }
                }

                // Remove items from the list
                foreach (GroceryItem itemToRemove in itemsToRemove)
                {
                    groceryList.Remove(itemToRemove);
                }

                // Remove checkboxes from the form
                foreach (CheckBox checkboxToRemove in checkboxesToRemove)
                {
                    scrollPanel.Controls.Remove(checkboxToRemove);
                    checkboxToRemove.Dispose();
                }

                if (filePath != null)
                {
                    await SaveToFileAsync(filePath);
                    LoadFromFile(filePath);
                }
                CreateCheckBoxes();
            }
        }

        private void AddItemToList(string itemName)
        {
            GroceryItem item = new GroceryItem
            {
                Name = itemName,
            };
            groceryList.Add(item);
        }

        private void CheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                // Find the corresponding grocery item by matching the checkbox label (item name)
                GroceryItem? item = groceryList.Find(g => g.Name == checkBox.Text);

                if (item != null)
                {
                    item.IsSelected = checkBox.Checked;
                }
            }
        }

        private async void AddButton_Click(object sender, EventArgs e)
        {
            string itemName = itemNameTextBox.Text;

            if (!string.IsNullOrWhiteSpace(itemName))
            {
                AddItemToList(itemName);
                itemNameTextBox.Clear();
            }

            if (filePath != null)
            {
                await SaveToFileAsync(filePath);
                LoadFromFile(filePath);
            }
             // Load items from the file
            CreateCheckBoxes();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int yPos = 100;

            foreach (var item in groceryList.Where(i => i.IsSelected))
            {
                e.Graphics?.DrawString(item.Name + " - " + item.Annotation, Font, Brushes.Black, 100, yPos);
                yPos += 20;
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void ItemNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AnnotationTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Find the corresponding grocery item by matching the item name
            GroceryItem? item = groceryList.Find(g => g.Name == textBox.Name);

            if (item != null)
            {
                item.Annotation = textBox.Text;
            }
        }

        private bool ShowConfirmationDialog()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete these items?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (ShowConfirmationDialog())
            {
                DeleteSelectedItems();
            }
        }
    }
}
