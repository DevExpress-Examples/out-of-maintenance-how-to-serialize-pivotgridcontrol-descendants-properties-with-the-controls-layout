using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using System.IO;
using DevExpress.Utils;
using System.Collections;
using DevExpress.XtraPivotGrid.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using System.Diagnostics;
using DevExpress.XtraPivotGrid.ViewInfo;
using DevExpress.Utils.Serializing;
using DevExpress.Utils.Serializing.Helpers;

namespace WindowsFormsSample {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            pivotGridControl1.Objects.Add(new Class1() { String = "test1" });
            pivotGridControl1.Objects.Add(new Class1() { String = "test2" });
            pivotGridControl1.Objects.Add(new Class1() { String = "test3" });
            pivotGridControl1.Object.String = "test4";
            pivotGridControl1.SaveLayoutToXml("test.xml");
            pivotGridControl1.Objects.Clear();
            pivotGridControl1.Object.String = null;
            pivotGridControl1.RestoreLayoutFromXml("test.xml");   
        }
    }
    public class MyPivotGrid : PivotGridControl {
        public List<Class1> Objects {
            get {
                return Data.Objects;
            }
        }
        public Class1 Object {
            get { return Data.Object; }
        }
        protected override PivotGridViewInfoData CreateData() {
            return new MyPivotGridViewInfoData(this);
        }
        public new MyPivotGridViewInfoData Data {
            get {
                return base.Data as MyPivotGridViewInfoData;
            }
        }
    }
    public class MyPivotGridViewInfoData : PivotGridViewInfoData, IXtraSupportDeserializeCollectionItem {
        public MyPivotGridViewInfoData(IViewInfoControl control) : base(control) {
            _Objects = new List<Class1>();
        }
        private List<Class1> _Objects;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true)]
        public List<Class1> Objects {
            get {
                return _Objects;
            }
        }
        private Class1 _Object = new Class1();
        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public Class1 Object {
            get { return _Object; }
        }
        
        #region IXtraSupportDeserializeCollectionItem Members

        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e) {
            if (propertyName == "Objects") {
                Class1 class1 = new Class1();
                Objects.Add(class1);
                return class1;
            } else return null;
        }

        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e) {
            
        }

        #endregion
    }
    public class Class1 {
        private string _String;
        [XtraSerializableProperty]
        public string String {
            get { return _String; }
            set {
                _String = value;
            }
        }
    }
}
