using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace MonetDB.GUI.UI
{
    internal sealed class CustomDatagridView : DataGridView
    {
        public CustomDatagridView(string name)
        {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false; 
            Dock = DockStyle.Fill; 
            Name = name;
            ReadOnly = true;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ShowCellErrors = false;
            ShowCellToolTips = false; 
            ShowRowErrors = false;
        }

        #region Overrides of DataGridView

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowPostPaint"/> event. 
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowPostPaintEventArgs"/> that contains the event data. </param>
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            using (var b = new SolidBrush(RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(CultureInfo.InvariantCulture), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }

            base.OnRowPostPaint(e);
        }

        #endregion
    }
}
