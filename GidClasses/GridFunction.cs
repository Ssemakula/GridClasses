using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridClasses
{
    public class Grids
    {
        public static int GetIntRef(DataGridView dgv, int _columnIndex) //Get integer value using column index
        {
            object? cellValue;
            cellValue = GetValue(dgv, _columnIndex);
            if (cellValue == null)
            {
                return 0;
            }
            else
            {
                return (int)cellValue;
            }
        }

        public static int GetIntRef(DataGridView dgv, string _columnIndex) //Get integer value using column name
        {
            object? cellValue;
            cellValue = GetValue(dgv, _columnIndex);
            if (cellValue == null)
            {
                return 0;
            }
            else
            {
                return (int)cellValue;
            }
        }

        public static string? GetStringRef(DataGridView dgv, int _columnIndex) //Get string value using column index
        {
            object? cellValue;
            cellValue = GetValue(dgv, _columnIndex);
            if (cellValue == null)
            {
                return "";
            }
            else
            {
                return cellValue.ToString();
            }
        }

        public static string? GetStringRef(DataGridView dgv, string _columnIndex) //Get string value using column name
        {
            object? cellValue;
            cellValue = GetValue(dgv, _columnIndex);
            if (cellValue == null)
            {
                return "";
            }
            else
            {
                return cellValue.ToString();

            }

        }

        public static double GetDoubleRef(DataGridView dgv, int _columnIndex) //Get double value using column index
        {
            object? cellValue;
            cellValue = GetValue(dgv, _columnIndex);
            if (cellValue == null)
            {
                return 0D;
            }
            else
            {
                return (double)cellValue;
            }
        }

        public static double GetDoubleRef(DataGridView dgv, string _columnIndex) //Get double value using colum name
        {
            object? cellValue;
            cellValue = GetValue(dgv, _columnIndex);
            if (cellValue == null)
            {
                return 0D;
            }
            else
            {
                return (double)cellValue;
            }
        }

        public static decimal GetDecimalRef(DataGridView dgv, string _columnIndex) //Get decimal value using column name
        {
            object? cellValue;
            cellValue = GetValue(dgv, _columnIndex);
            if (cellValue == null)
            {
                return 0M;
            }
            else
            {
                return (decimal)cellValue;
            }
        }

        public static decimal GetDecimalRef(DataGridView dgv, int _columnIndex) //Get decimal value using column index
        {
            object? cellValue;
            cellValue = GetValue(dgv, _columnIndex);
            if (cellValue == null)
            {
                return 0M;
            }
            else
            {
                return (decimal)cellValue;
            }
        }


        public static object? GetValue(DataGridView dgv, string _columnIndex) //Get value using column name
        {
            //int rowIndex = 0; //int columnIndex = 0;
            int countMess = dgv.RowCount; //Check whether there are any records in grid
            if (countMess < 1)  //if none return null (alsways check if GetInRef() returns 0)
            {
                return null;
            }
            //DataGridViewRow row = _dataGridView.Rows[rowIndex];
            //DataGridViewCell cell = row.Cells[_columnIndex];
            object? cellValue;
            if (dgv.SelectedRows.Count > 0) //have to select a full row...
            {
                int selectedRow = dgv.SelectedRows[0].Index; //Get the index of the first selected row
                object rowValue = GetCellValue(dgv, _columnIndex, selectedRow);
                cellValue = rowValue;
            }
            else if (dgv.SelectedCells.Count > 0) //Selected a cell
            {
                int selectedRow = dgv.SelectedCells[0].RowIndex; //Get the row index of the first selected cell
                object rowValue = GetCellValue(dgv, _columnIndex, selectedRow);
                cellValue = rowValue;
            }
            else
            {
                cellValue = null;
            }
            return cellValue;
        }

        public static object? GetValue(DataGridView dgv, int _columnIndex) //Get value using column index
        {
            //
            //.
            //.int rowIndex = 0; //int columnIndex = 0;
            int countMess = dgv.RowCount; //Check whether there are any records in grid
            if (countMess < 1)  //if none return 0 (alsways check if GetInRef() returns 0)
            {
                return null;
            }
            DataGridViewRow row;
            DataGridViewCell cell;
            object? cellValue;
            if (dgv.SelectedRows.Count > 0) //have to select a full row...
            {
                DataGridViewRow selectedRow = dgv.SelectedRows[0];
                object rowValue = selectedRow.Cells[_columnIndex].Value;
                cellValue = rowValue;
            }
            else if (dgv.SelectedCells.Count > 0) //Selected a cell
            {
                cell = dgv.SelectedCells[0];
                row = dgv.Rows[cell.RowIndex];
                cellValue = row.Cells[_columnIndex].Value;
            }
            else
            {
                cellValue = null;
            }
            return cellValue;
        }

        public static object GetCellValue(DataGridView dgv, string _columnName, int _rowIndex)
        {
            object cellValue = dgv.Rows[_rowIndex].Cells[_columnName].Value;
            return cellValue;
        }

        //------------------------Clear selection---------------------------------------------------------//
        public static void ClearGridSelection(DataGridView dgv) //Clear selection
        {
            DataGridViewSelectionMode oldMode = dgv.SelectionMode;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ClearSelection();
            dgv.SelectionMode = oldMode;
        }

        //----------------------------------Positioning--------------------------------------------------//
        public static int NextGridRow(DataGridView dgv) //returns next selected row
        {
            if (dgv.RowCount < 1) return 0; //if no rows return 0
            int rowIndex = dgv.CurrentRow.Index; //get current selected row
            if(rowIndex < dgv.Rows.Count - 1) 
            {
                rowIndex++;
                ClearGridSelection(dgv); 
                for (int i = 0; i < dgv.Columns.Count; i++) //Get first visible column
                {
                    if (dgv[i, rowIndex].Visible == true)
                    {
                        dgv.CurrentCell = dgv[i, rowIndex];
                        break;
                    }
                }
                dgv.Rows[rowIndex].Selected = true;
            }
            return rowIndex + 1; //Return position starting from 1
        }

        public static int PrevGridRow(DataGridView dgv) //returns prev selected row
        {
            if (dgv.RowCount < 1) return 0; //if no rows return 0
            int rowIndex = dgv.CurrentRow.Index; //get current selected row
            if (rowIndex > 0) //Move only if not at first column
            {
                rowIndex--;
                ClearGridSelection(dgv); 
                dgv.Rows[rowIndex].Selected = true;
                for (int i = 0; i < dgv.Columns.Count; i++) //Get first visible column
                {
                    if (dgv[i, rowIndex].Visible == true)
                    {
                        dgv.CurrentCell = dgv[i, rowIndex];
                        break;
                    }
                }
            }
            return rowIndex + 1; //Return position starting from 1
        }

        public static int FirstGridRow(DataGridView dgv) //returns first selected row
        {
            if (dgv.RowCount < 1) return 0; //if no rows return 0
            ClearGridSelection(dgv);
            dgv.Rows[0].Selected = true;
            for (int i = 0; i < dgv.Columns.Count; i++) //Get first visible column
            {
                if (dgv[i, 0].Visible == true)
                {
                    dgv.CurrentCell = dgv[i, 0];
                    break;
                }
            }
            return 1; //Return position starting from 1
        }

        public static int LastGridRow(DataGridView dgv) //returns last selected row
        {
            if (dgv.RowCount < 1) return 0; //if no rows return 0
            int rowCount = dgv.RowCount;
            ClearGridSelection(dgv);
            dgv.Rows[rowCount -1].Selected = true;
            for (int i = 0; i < dgv.Columns.Count; i++) //Get first visible column
            {
                if (dgv[i, rowCount - 1].Visible == true)
                {
                    dgv.CurrentCell = dgv[i, rowCount - 1];
                    break;
                }
            }
            return rowCount; //Return position starting from 1
        }

        //-----------------Edit on/off-----------------------------------------------------------------//
        // Return true is at least one column was modified, false otherwise

        public static bool EnableEditColumns(DataGridView dgv, List<string> writeColumns)
        {
            bool result = false;
            foreach (string columnName in writeColumns)
            {
                //Processing here
                DataGridViewColumn column = dgv.Columns[columnName];
                if (column != null)
                {
                    column.ReadOnly = false;
                    result = true;
                }
            }
            return result;
        }

        public static bool EnableEditColumns(DataGridView dgv, List<int> writeColumns)
        {
            bool result = false;
            foreach (int columnIndex in writeColumns)
            {
                //Processing here
                if (columnIndex >= 0 && columnIndex < dgv.Columns.Count)
                {

                    DataGridViewColumn column = dgv.Columns[columnIndex];
                    column.ReadOnly = false;
                    result = true;
                }
            }
            return result;
        }

        public static bool EnableEditColumns(DataGridView dgv, string writeColumn)
        {
            bool result = false;
            //Processing here
            DataGridViewColumn column = dgv.Columns[writeColumn];
            if (column != null)
            {
                column.ReadOnly = false;
                result = true;
            }
            return result;
        }

        public static bool EnableEditColumns(DataGridView dgv, int writeColumn)
        {
            bool result = false;
            //Processing here
            if (writeColumn >= 0 && writeColumn < dgv.Columns.Count)
            {
                DataGridViewColumn column = dgv.Columns[writeColumn];
                column.ReadOnly = false;
                result = true;
            }
            return result;
        }


        public static bool DisableEditColumns(DataGridView dgv, List<string> readOnlyColumns)
        {
            bool result = false;
            foreach (string columnName in readOnlyColumns)
            {
                //Processing here
                DataGridViewColumn column = dgv.Columns[columnName];
                if (column != null)
                {
                    column.ReadOnly = true;
                    result = true;
                }
            }
            return result;
        }

        public static bool DisableEditColumns(DataGridView dgv, List<int> readOnlyColumns)
        {
            bool result = false;
            foreach (int columnIndex in readOnlyColumns)
            {
                //Processing here
                if (columnIndex >= 0 && columnIndex < dgv.Columns.Count)
                {
                    DataGridViewColumn column = dgv.Columns[columnIndex];
                    column.ReadOnly = true;
                    result = true;
                }
            }
            return result;
        }

        public static bool DisableEditColumns(DataGridView dgv, string readOnlyColumn)
        {
            bool result = false;
            //Processing here
            DataGridViewColumn column = dgv.Columns[readOnlyColumn];
            if (column != null)
            {
                column.ReadOnly = true;
                result = true;
            }
            return result;
        }

        public static bool DisableEditColumns(DataGridView dgv, int readOnlyColumn)
        {
            bool result = false;
            //Processing here
            if (readOnlyColumn >= 0 && readOnlyColumn < dgv.Columns.Count)
            {
                DataGridViewColumn column = dgv.Columns[readOnlyColumn];
                column.ReadOnly = true;
                result = true;
            }
            return result;
        }
    }
}
