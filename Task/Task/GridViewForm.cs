using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SampleSupport;
using Task.Data;

namespace Task
{
    public partial class GridViewForm : Form
    {
        DataSource _dataSource = new DataSource();
        public GridViewForm()
        {
            InitializeComponent();
            FillData();
        
        }

        private void FillData()
        {
            var customers = _dataSource.Customers
                .Where(c => c.Orders.Sum(o => o.Total) > 75000)
                .Select(c => new
                {
                    customerId = c.CustomerID,
                    customerAddress = c.Address,
                    total = c.Orders.Sum(o => o.Total)
                });

            dataGridView1.DataSource = ToDataTable(customers);
        }

        private DataTable ToDataTable<T>(IEnumerable<T> Linqlist)
        {
            var dt = new DataTable();

            PropertyInfo[] columns = null;
            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist) {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>))) {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns) {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
