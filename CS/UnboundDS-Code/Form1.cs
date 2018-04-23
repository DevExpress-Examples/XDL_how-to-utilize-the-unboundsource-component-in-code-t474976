using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.Data;

namespace UnboundDS_Code {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm {
        public Form1() {
            InitializeComponent();
            gridView1.OptionsBehavior.AutoPopulateColumns = false;
            CreateDS();
            CreateGridColumns();
            gridControl1.UseEmbeddedNavigator = true;

        }

        public void CreateDS() {
            UnboundSource unboundDS = new UnboundSource();
            UnboundSourceProperty unboundSourceProperty1 = new UnboundSourceProperty() { DisplayName = "ID", Name = "Int", PropertyType = typeof(int) };
            UnboundSourceProperty unboundSourceProperty2 = new UnboundSourceProperty() { DisplayName = "Day of Week", Name = "String", PropertyType = typeof(string) };
            unboundDS.Properties.AddRange(new DevExpress.Data.UnboundSourceProperty[] {
            unboundSourceProperty1,
            unboundSourceProperty2});
            unboundDS.ValueNeeded += unboundDS_ValueNeeded;
            unboundDS.ValuePushed += unboundDS_ValuePushed;
            unboundDS.SetRowCount(100000);
            gridControl1.DataSource = unboundDS;
        }

        void unboundDS_ValuePushed(object sender, UnboundSourceValuePushedEventArgs e) {
            var defaultValue = GetDefaultData(e.RowIndex, e.PropertyName);
            var cellKey = new CellKey(e.RowIndex, e.PropertyName);
            if(object.Equals(defaultValue, e.Value))
                this.Differences.Remove(cellKey);
            else
                this.Differences[cellKey] = e.Value;
        }

        void unboundDS_ValueNeeded(object sender, UnboundSourceValueNeededEventArgs e) {
            if(this.Differences.Count > 0) {
                object rv;
                if(this.Differences.TryGetValue(new CellKey(e.RowIndex, e.PropertyName), out rv)) {
                    e.Value = rv;
                    return;
                }
            }
            e.Value = GetDefaultData(e.RowIndex, e.PropertyName);
        }


        public void CreateGridColumns() {
            gridView1.Columns.AddVisible("Int");
            gridView1.Columns.AddVisible("String");
        }

        private struct CellKey : IEquatable<CellKey> {
            int rowIndex;
            string propertyName;
            public int RowIndex { get { return rowIndex; } }
            public string PropertyName { get { return propertyName; } }
            public CellKey(int rowIndex, string propertyName) {
                this.rowIndex = rowIndex;
                this.propertyName = propertyName;
            }
            public bool Equals(CellKey other) {
                return this.RowIndex == other.RowIndex && this.PropertyName == other.PropertyName;
            }
            public override int GetHashCode() {
                return unchecked(RowIndex * 257 + (string.IsNullOrEmpty(this.PropertyName) ? 0 : this.PropertyName[0]));
            }
            public override bool Equals(object obj) {
                if(obj is CellKey)
                    return Equals((CellKey)obj);
                else
                    return false;
            }
        }

        readonly string[] DefaultStringData = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
        readonly Dictionary<CellKey, object> Differences = new Dictionary<CellKey, object>();
        object GetDefaultData(int rowIndex, string propertyName) {
            switch(propertyName) {
                case "String":
                    return DefaultStringData[rowIndex % DefaultStringData.Length];
                case "Int":
                    return rowIndex + 1;
                default:
                    return null;
            }
        }
    }
}
