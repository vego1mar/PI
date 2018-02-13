using PI.src.enumerators;
using PI.src.helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PI.src.curves
{
    public static class GridAssist
    {
        public static void SetDefaultSettings( DataGridView grid )
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                BackColor = Color.White
            };

            grid.RowHeadersDefaultCellStyle = new DataGridViewCellStyle {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                BackColor = SystemColors.Control,
                Font = new Font( "Consolas", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 238 ),
                ForeColor = SystemColors.WindowText,
                SelectionBackColor = SystemColors.Highlight,
                SelectionForeColor = SystemColors.HighlightText
            };

            grid.RowsDefaultCellStyle = new DataGridViewCellStyle {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                BackColor = Color.White
            };
        }

        public static void AlterColumnHeader( DataGridViewColumn column, string headerText, bool isReadOnly = true )
        {
            if ( column == null || headerText == null ) {
                return;
            }

            column.HeaderText = headerText;
            column.ReadOnly = isReadOnly;
        }

        public static void AlterRowHeader( DataGridViewRow row, object headerValue )
        {
            if ( row == null ) {
                return;
            }

            row.HeaderCell.Value = headerValue;
        }

        public static void PopulateColumn<T>( DataGridView grid, string columnName, IList<T> values )
        {
            if ( grid == null || columnName == null || values == null ) {
                return;
            }

            for ( int i = 0; i < values.Count; i++ ) {
                grid.Rows[i].Cells[columnName].ValueType = typeof( T );
                grid.Rows[i].Cells[columnName].Value = values[i];
            }
        }

        public static void PopulateColumn( DataGridView grid, string columnName, IList<double> values, int decimalPlacesNo )
        {
            if ( grid == null || columnName == null || values == null || decimalPlacesNo < 0 ) {
                return;
            }

            for ( int i = 0; i < values.Count; i++ ) {
                grid.Rows[i].Cells[columnName].ValueType = typeof( double );
                grid.Rows[i].Cells[columnName].Value = Strings.TryFormatAsNumeric( decimalPlacesNo, values[i], Thread.CurrentThread.CurrentCulture );
            }
        }

        public static void AddRows( DataGridView grid, int rowsNo )
        {
            if ( grid == null || rowsNo <= 0 ) {
                return;
            }

            for ( int i = 0; i < rowsNo; i++ ) {
                grid.Rows.Add();
            }
        }

        public static void SetAutoSizeColumnsMode( DataGridView grid, AutoSizeColumnsMode mode )
        {
            if ( grid == null ) {
                return;
            }

            switch ( mode ) {
            case AutoSizeColumnsMode.AllCells:
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                break;
            case AutoSizeColumnsMode.AllCellsExceptHeader:
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
                break;
            case AutoSizeColumnsMode.ColumnHeader:
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                break;
            case AutoSizeColumnsMode.DisplayedCells:
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                break;
            case AutoSizeColumnsMode.DisplayedCellsExceptHeader:
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
                break;
            case AutoSizeColumnsMode.Fill:
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                break;
            case AutoSizeColumnsMode.None:
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                break;
            }
        }

        public static void SetCellBackColor( DataGridView grid, int rowIndex, int cellIndex, Color color )
        {
            if ( grid == null || rowIndex < 0 || cellIndex < 0 ) {
                return;
            }

            grid.Rows[rowIndex].Cells[cellIndex].Style.BackColor = color;
        }

        public static IList<object> GetColumnValues( DataGridView grid, string headerText )
        {
            if ( grid == null || headerText == null ) {
                return new List<object>().AsReadOnly();
            }

            IList<object> values = new List<object>();

            for ( int i = 0; i < grid.Rows.Count; i++ ) {
                values.Add( grid.Rows[i].Cells[headerText].Value );
            }

            return values;
        }
    }
}
