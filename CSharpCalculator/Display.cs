using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CSharpCalculator
{
    class Display
    {

        private TextBlock[] textBlocks;

        public Display(TextBlock[] newTextBlocks)
        {
            textBlocks = newTextBlocks;
        }

        public void UpdateDisplay(string newText)
        {
            textBlocks[0].Text += newText;
        }

        public void ClearDisplay()
        {
            textBlocks[0].Text = "";
        }

        public void ReplaceDisplay (string text)
        {
            this.ClearDisplay();
            this.UpdateDisplay(text);
        }

        public void BackspaceDisplay()
        {
            if (textBlocks[0].Text.Length > 0)
            {
                textBlocks[0].Text = textBlocks[0].Text.Remove(textBlocks[0].Text.Length - 1, 1);
            }
        }

        public void MoveUp()
        {

            for (int i = textBlocks.Length-1; i > 0; i--)
            {
                textBlocks[i].Text = textBlocks[i-1].Text;
            }
            textBlocks[0].Text = "";
        }

    }
}
